namespace Game.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Game.Definitions;

    /// <summary>
    /// Basic implementation of the tic-tac-toe game
    /// </summary>
    public class GameEngine
        : IGameEngine
        , IGameEngineTestRig
    {
        /// <summary>
        /// Creates an instance of the GameState object
        /// </summary>
        public GameEngine() 
        {
            const int numberOfPositions = 9;

            GameState = Enumerable.Range(0, numberOfPositions).Select(i => -1).ToArray();
            ActivePlayer = 0;
        }

        ///<inheritdoc/>    
        public int ActivePlayer { get; set; }

        ///<inheritdoc/>   
        public int[] GameState { get; set; }

        ///<inheritdoc/>
        public bool IsGameOver
            => HasWinner || !ValidMoves.Any();

        ///<inheritdoc/>
        public bool HasWinner
            // Horizontals
            => (GameState[0] != -1 && GameState[0] == GameState[1] && GameState[0] == GameState[2])
            || (GameState[3] != -1 && GameState[3] == GameState[4] && GameState[3] == GameState[5])
            || (GameState[6] != -1 && GameState[6] == GameState[7] && GameState[6] == GameState[8])

            // Verticals
            || (GameState[0] != -1 && GameState[0] == GameState[3] && GameState[0] == GameState[6])
            || (GameState[1] != -1 && GameState[1] == GameState[4] && GameState[1] == GameState[7])
            || (GameState[2] != -1 && GameState[2] == GameState[5] && GameState[2] == GameState[8])

            // Diagonals
            || (GameState[0] != -1 && GameState[0] == GameState[4] && GameState[0] == GameState[8])
            || (GameState[2] != -1 && GameState[2] == GameState[4] && GameState[2] == GameState[6]);
        
        ///<inheritdoc/>
        public IEnumerable<int> ValidMoves
            => this.GameState
                .Select((i,j) => new { index = j, state = i})
                .Where(i => i.state == -1)
                .Select(i => i.index);

        ///<inheritdoc/>
        public bool SubmitMove(int move)
        {
            // Sanity check
            if (!ValidMoves.Contains(move))
                return false;

            if (IsGameOver)
                return false;

            // Set the new move
                this.GameState[move] = ActivePlayer;

            if (!IsGameOver)
            {
                // Set the next active player
                ActivePlayer = ++ActivePlayer % 2;
            }

            return true;
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return string.Join(" ", GameState
                .Select(i => i < 0 ? "-" : i == 0 ? "O" : "X")
                .Select((i, j) => j % 3 == 0 ? $"\n{i}" : i));
        }
    }
}