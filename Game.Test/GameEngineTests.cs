namespace Game.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Game.Definitions;
    using Game.Implementation;
    using NUnit.Framework;

    [TestFixture(typeof(GameEngine))]
    public class Tests<T> where T : IGameEngineTestRig, new()
    {
        public IGameEngineTestRig MakeEngine() 
        {
            return new T();
        }
                
        [Test]
        public void ValidMovesTests()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ActivePlayerTests()
        {
            var game = MakeEngine();
            var turn = 0;

            Assert.IsTrue(game.ActivePlayer == 0, "Starting player should be 0");

            while(!game.IsGameOver)
            {
                Assert.IsTrue(game.ActivePlayer == turn % 2, $"Active player should be {turn % 2} on turn {turn}");
                game.SubmitMove(game.ValidMoves.First());
                turn++;
            }
        }

        [TestCase(new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 }, false)]
        [TestCase(new int[] { 0, 0, 0, -1, -1, -1, -1, -1, -1 }, true)]
        [TestCase(new int[] { -1, -1, -1, 0, 0, 0, -1, -1, -1 }, true)]
        [TestCase(new int[] { -1, -1, -1, -1, -1, -1, 0, 0, 0 }, true)]
        [TestCase(new int[] { 0, -1, -1, 0, -1, -1, 0, -1, -1 }, true)]
        [TestCase(new int[] { -1, 0, -1, -1, 0, -1, -1, 0, -1 }, true)]
        [TestCase(new int[] { -1, -1, 0, -1, -1, 0, -1, -1, 0 }, true)]
        [TestCase(new int[] { 0, -1, -1, -1, 0, -1, -1, -1, 0 }, true)]
        [TestCase(new int[] { -1, -1, 0, -1, 0, -1, 0, -1, -1 }, true)]
        [TestCase(new int[] { 1, 1, 1, -1, -1, -1, -1, -1, -1 }, true)]
        [TestCase(new int[] { -1, -1, -1, 1, 1, 1, -1, -1, -1 }, true)]
        [TestCase(new int[] { -1, -1, -1, -1, -1, -1, 1, 1, 1 }, true)]
        [TestCase(new int[] { 1, -1, -1, 1, -1, -1, 1, -1, -1 }, true)]
        [TestCase(new int[] { -1, 1, -1, -1, 1, -1, -1, 1, -1 }, true)]
        [TestCase(new int[] { -1, -1, 1, -1, -1, 1, -1, -1, 1 }, true)]
        [TestCase(new int[] { 1, -1, -1, -1, 1, -1, -1, -1, 1 }, true)]
        [TestCase(new int[] { -1, -1, 1, -1, 1, -1, 1, -1, -1 }, true)]
        public void IsGameOverTests(int[] gameState, bool expectedResult)
        {
            var game = MakeEngine();
            game.GameState = gameState;
            Assert.IsTrue(game.IsGameOver == expectedResult);
        }

        [Test]
        public void SubmitMoveTests()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Randomized playthrough which allows us to capture odd situations,
        /// when brute forcing al inputs is not an option.
        /// </summary>
        [Test]        
        public void RandomizedGame()
        {
            var seed = Guid.NewGuid().GetHashCode();
            var rng = new Random(seed);
            var game = MakeEngine();
            var moves = new List<int>();

            try
            {
                while (!game.IsGameOver)
                {
                    Assert.True(game.ActivePlayer == moves.Count % 2, "Player Order");

                    var validMoves = game.ValidMoves.ToArray();
                    Assert.True(validMoves.Length > 0, "Valid moves exist!");

                    moves.Add(validMoves[rng.Next(0, validMoves.Length)]);
                    Assert.True(game.SubmitMove(moves.Last()), "");
                }
            }
            catch (Exception)
            {
                File.WriteAllText($"..\\..\\..\\Data\\{nameof(RandomizedGame)}.{seed}.csv", string.Join("\n", moves));
                throw;
            }
        }
    }
}