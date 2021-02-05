namespace Game.Definitions
{
    using System.Collections.Generic;

    public interface IGameEngine
    {
        /// <summary>
        /// Returns the valid positional moves as integers
        /// </summary>
        IEnumerable<int> ValidMoves { get; }

        /// <summary>
        /// Returns the active player Id
        /// </summary>
        int ActivePlayer { get; }

        /// <summary>
        /// 
        /// </summary>
        int[] GameState { get; }

        /// <summary>
        /// Returns whether the game is in a "completed" state as a boolean
        /// </summary>
        bool IsGameOver { get; }

        /// <summary>
        /// Returns whether the game has a winner
        /// </summary>
        bool HasWinner { get; }

        /// <summary>
        /// Submit an integer positional move to be processed by the game engine
        /// </summary>
        /// <param name="move">The integer position of the move</param>
        /// <returns>Whether the move was submitted successfully</returns>
        bool SubmitMove(int move);
    }
}