namespace Game.Implementation
{
    using System;
    using Game.Definitions;

    class Program
    {
        static void Main()
        {
            IGameEngine game = new GameEngine();
            Console.WriteLine($"{game.ToString()}\n");

            while (!game.IsGameOver)
            {
                Console.WriteLine($"Player {game.ActivePlayer}, please submit a move: ({string.Join(", ", game.ValidMoves)})");

                while (!int.TryParse(Console.ReadLine(), out int move) || !game.SubmitMove(move))
                {
                    Console.WriteLine($"Invalid move, please try again: ({string.Join(", ", game.ValidMoves)})");
                }

                Console.Clear();
                Console.WriteLine($"{game.ToString()}\n");
            }

            Console.WriteLine(game.HasWinner
                ? $"\t The Winner is: Player { game.ActivePlayer }!"
                : $"\t The game is a draw!");
        }
    }
}