using UnityEngine;

namespace TestGwentGame.Gameplay {
    public sealed class GameManager : Singleton<GameManager> {
        [SerializeField] BaseTeam _playerTeam;
        [SerializeField] BaseTeam _aiTeam;

        bool     _isGameEnded;
        int      _currentTurn;
        BaseTeam _currentTeam;

        public BaseTeam PlayerTeam => _playerTeam;
        public BaseTeam AiTeam     => _aiTeam;

        protected override void Awake() {
            base.Awake();
            Setup();
        }

        void OnEnable() {
            EventManager.onTeamTurnEnded.AddListener(SwitchSides);
            EventManager.onTeamDied.AddListener(OnTeamDied);
        }

        void OnDisable() {
            EventManager.onTeamTurnEnded.RemoveListener(SwitchSides);
            EventManager.onTeamDied.RemoveListener(OnTeamDied);
        }

        void Start() => Init();

        void Setup() {
            _currentTeam = _playerTeam;
            _currentTurn = 0;
            EndTurn();
        }

        void Init() {
            _playerTeam.Init(_aiTeam);
            _aiTeam.Init(_playerTeam);

            _currentTeam.StartTeamTurn();
        }

        void SwitchSides() {
            if (_isGameEnded) {
                return;
            }

            if (_currentTeam == _aiTeam) {
                EndTurn();
            }

            _currentTeam = _currentTeam.EnemyTeam;
            _currentTeam.StartTeamTurn();
        }

        void EndTurn() {
            _currentTurn += 1;
            Debug.Log($"Turn {_currentTurn}");
            EventManager.onTurnStarted.Invoke();
        }

        void OnTeamDied(BaseTeam team) {
            var teamName = team == _aiTeam ? "<color=green>Player</color>" : "<color=red>AI</color>";
            Debug.Log($"{teamName} team wins!");
            _isGameEnded = true;
        }
    }
}


