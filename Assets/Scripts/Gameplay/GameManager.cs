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
            _currentTeam = _playerTeam;

            _currentTurn = 0;
            EndTurn();
        }

        void OnEnable() {
            EventManager.onTeamTurnEnded.AddListener(SwitchSides);
            EventManager.onTeamDied.AddListener(OnTeamDied);
        }

        void OnDisable() {
            EventManager.onTeamTurnEnded.RemoveListener(SwitchSides);
            EventManager.onTeamDied.RemoveListener(OnTeamDied);
        }

        void Start() {
            _playerTeam.Setup(_aiTeam);
            _aiTeam.Setup(_playerTeam);

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
        }

        void OnTeamDied(BaseTeam team) {
            var teamName = team == _aiTeam ? "<color=green>Player</color>" : "<color=red>AI</color>";
            Debug.Log($"{teamName} team wins!");
            _isGameEnded = true;
        }
    }
}


