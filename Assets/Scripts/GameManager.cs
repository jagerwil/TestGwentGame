using UnityEngine;

namespace TestGwentGame {
    public sealed class GameManager : MonoBehaviour {
        [SerializeField] BaseTeam _playerTeam;
        [SerializeField] BaseTeam _aiTeam;

        bool     _isGameEnded;
        int      _currentTurn;
        BaseTeam _currentTeam;

        void Awake() {
            _currentTeam = _playerTeam;

            _playerTeam.Setup(_aiTeam);
            _aiTeam.Setup(_playerTeam);

            _currentTurn = 0;
            EndTurn();
        }

        void OnEnable() {
            BaseTeam.onTurnEnded += SwitchSides;
            BaseTeam.onTeamDied  += OnTeamDied;
        }

        void OnDisable() {
            BaseTeam.onTurnEnded -= SwitchSides;
            BaseTeam.onTeamDied  -= OnTeamDied;
        }

        void Start() {
            _currentTeam.StartTeamTurn();
        }

        void SwitchSides() {
            if (_isGameEnded) {
                return;
            }

            _currentTeam = _currentTeam.EnemyTeam;
            if (_currentTeam == _aiTeam) {
                EndTurn();
            }

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


