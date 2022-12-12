using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGwentGame {
    public abstract class BaseTeam : MonoBehaviour {
        [SerializeField] protected List<Pawn> _pawns;

        public static event Action           onTurnEnded;
        public static event Action<BaseTeam> onTeamDied;

        public BaseTeam EnemyTeam { get; private set; }
        
        public void Setup(BaseTeam oppositeTeam) {
            EnemyTeam = oppositeTeam;
        }

        void OnEnable() {
            foreach (var pawn in _pawns) {
                pawn.onDied += OnPawnDied;
            }
        }

        void OnDisable() {
            foreach (var pawn in _pawns) {
                pawn.onDied -= OnPawnDied;
            }
        }

        public virtual void StartTeamTurn() {
            foreach (var pawn in _pawns) {
                pawn.StartTurn();
            }
        }

        public void UsePawn(Pawn pawn, Pawn target) {
            if ( !pawn.TryUse(target) ) {
                return;
            }

            if (_pawns.All(a => a.Action.WasUsed)) {
                onTurnEnded?.Invoke();
            }
        }

        public List<Pawn> GetPawnTargets(Pawn pawn) {
            var effect     = pawn.Action;
            var enemyPawns = EnemyTeam._pawns;
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

        void OnPawnDied() {
            if (_pawns.All(pawn => pawn.IsDead)) {
                Debug.Log("The entire team is ded!");
                onTeamDied?.Invoke(this);
            }
        }
    }
}
