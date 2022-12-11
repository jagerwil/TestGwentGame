using UnityEngine;

namespace TestGwentGame {
    public sealed class GameManager : MonoBehaviour {
        [SerializeField] BaseTeam _playerTeam;
        [SerializeField] BaseTeam _enemyTeam;

        int _curTurn;
        BaseTeam _curTeam;

        void Awake() {
            _curTeam = _playerTeam;
        }

        void Start() {
            _curTurn = 0;
            EndTurn();

            _curTeam.StartTurn();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SwitchSides();
            }
        }

        void SwitchSides() {
            if (_curTeam == _playerTeam) {
                _curTeam = _enemyTeam;
            } else {
                _curTeam = _playerTeam;
                EndTurn();
            }
            
            _curTeam.StartTurn();
        }

        void EndTurn() {
            _curTurn += 1;
            Debug.Log($"Turn {_curTurn}");
        }
    }
}


