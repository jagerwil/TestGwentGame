using TestGwentGame.Gameplay;

namespace TestGwentGame {
    public static class EventManager {
        public static readonly Evt onTurnStarted = new();
        
        public static readonly Evt onTeamTurnEnded = new();
        public static readonly Evt<BaseTeam> onTeamDied = new();

        public static readonly Evt<Pawn> onPawnHealthChanged = new();
        public static readonly Evt<Pawn> onPawnDied = new();
    }
}

