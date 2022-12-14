using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public abstract class BaseTeam : MonoBehaviour {
        [SerializeField] protected List<Pawn> _pawns;
        
        public abstract TeamType TeamType { get; }

        public List<Pawn> Pawns => _pawns;

        void Awake() => gameObject.SetActive(true);

        public void Init() {
            foreach (var pawn in _pawns) {
                pawn.Init(OnPawnUsed);
            }
        }

        public virtual void Refresh() {
            gameObject.SetActive(true);
            foreach (var pawn in _pawns) {
                pawn.Refresh();
            }
        }

        void OnEnable() {
            EventManager.onPawnDied.AddListener(OnPawnDied);
        }

        void OnDisable() {
            EventManager.onPawnDied.RemoveListener(OnPawnDied);
        }

        public virtual void StartTeamTurn() {
            foreach (var pawn in _pawns) {
                pawn.StartTeamTurn();
            }
        }

        public void EndTeamTurn() {
            EventManager.onTeamTurnEnded.Invoke(TeamType);
        }

        void OnPawnDied(Pawn pawn) {
            if (_pawns.All(pawn => pawn.IsDead)) {
                Debug.Log("The entire team is ded!");
                gameObject.SetActive(false);
                EventManager.onTeamDied.Invoke(TeamType);
            }
        }

        void OnPawnUsed() {
            if (_pawns.All(a => a.Action.WasUsed)) {
                EndTeamTurn();
            }
        }
    }
}
