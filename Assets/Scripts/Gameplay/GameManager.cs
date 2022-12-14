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
            EventManager.onTeamTurnEnded.AddListener(OnTeamTurnEnded);

            EventManager.Input.onRefreshKeyPressed.AddListener(Refresh);
            EventManager.Input.onEndTurnKeyPressed.AddListener(EndPlayerTurn);
        }

        void OnDisable() {
            EventManager.onTeamTurnEnded.RemoveListener(OnTeamTurnEnded);

            EventManager.Input.onRefreshKeyPressed.RemoveListener(Refresh);
            EventManager.Input.onEndTurnKeyPressed.RemoveListener(EndPlayerTurn);
        }

        void Start() => Init();

        public void EndPlayerTurn() {
            if (_currentTeam == _playerTeam) {
                SwitchSides();
            }
        }

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

        void Refresh() {
            Setup();
            _playerTeam.Refresh();
            _aiTeam.Refresh();
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

        void OnTeamTurnEnded(TeamType teamType) => SwitchSides();
    }
}


