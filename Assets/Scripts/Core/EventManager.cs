using TestGwentGame.Gameplay;

namespace TestGwentGame {
    public static class EventManager {
        public static readonly Evt<int> onTurnStarted = new();
        
        public static readonly Evt<TeamType> onTeamTurnEnded = new();
        public static readonly Evt<TeamType> onTeamDied = new();

        public static readonly Evt<Pawn> onPawnHealthChanged = new();
        public static readonly Evt<Pawn> onPawnDied = new();

        public static class Input {
            public static readonly Evt onRefreshKeyPressed = new();
            public static readonly Evt onEndTurnKeyPressed = new();
        }
    }

}
