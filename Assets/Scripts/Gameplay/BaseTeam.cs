using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public abstract class BaseTeam : MonoBehaviour {
        [SerializeField] protected List<Pawn> _pawns;

        public BaseTeam EnemyTeam { get; private set; }
        public List<Pawn> Pawns => _pawns;

        void Awake() => gameObject.SetActive(true);

        public void Init(BaseTeam oppositeTeam) {
            EnemyTeam = oppositeTeam;
            foreach (var pawn in _pawns) {
                pawn.Init(this, OnPawnUsed);
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

        public List<Pawn> GetPawnTargets(Pawn pawn) {
            var effect     = pawn.Action;
            var enemyPawns = EnemyTeam.Pawns;
            var targets    = new List<Pawn>(_pawns.Count + enemyPawns.Count);

            if (effect.CanUseOn(TargetType.Enemy)) {
                targets.AddRange(enemyPawns);
            }

            if (effect.CanUseOn(TargetType.Ally)) {
                targets.AddRange(_pawns.Where(allyPawn => allyPawn != pawn));
            }

            if (effect.CanUseOn(TargetType.Self)) {
                targets.Add(pawn);
            }

            return targets;
        }

        public TargetType GetTargetType(Pawn pawn, Pawn target) {
            if (pawn == target) {
                return TargetType.Self;
            }
            
            var isSameTeam = _pawns.Contains(target);
            if (isSameTeam) {
                return TargetType.Ally;
            }

            return TargetType.Enemy;
        }

        void OnPawnDied(Pawn pawn) {
            if (_pawns.All(pawn => pawn.IsDead)) {
                Debug.Log("The entire team is ded!");
                gameObject.SetActive(false);
                EventManager.onTeamDied.Invoke(this);
            }
        }

        void OnPawnUsed() {
            if (_pawns.All(a => a.Action.WasUsed)) {
                EventManager.onTeamTurnEnded.Invoke();
            }
        }
    }
}
