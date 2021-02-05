namespace Game.Definitions
{
    public interface IGameEngineTestRig : IGameEngine
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

        /// <summary>
        /// 
        /// </summary>
        int[] GameState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ActivePlayer { get; set; }
        
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    }
}