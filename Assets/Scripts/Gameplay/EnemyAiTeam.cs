using System.Collections;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public sealed class EnemyAiTeam : BaseTeam {
        protected override TeamType TeamType => TeamType.Ai;

        Coroutine _startTurnCoro;
        
        public override void StartTeamTurn() {
            base.StartTeamTurn();
            _startTurnCoro = StartCoroutine(StartTurnCoro());
        }

        public override void Refresh() {
            base.Refresh();
            if (_startTurnCoro != null) {
                StopCoroutine(_startTurnCoro);
            }
        }

        IEnumerator StartTurnCoro() {
            foreach (var pawn in _pawns) {
                if ( pawn.IsDead ) {
                    continue;
                }

                var targets = GameManager.Instance.GetPawnTargets(pawn);
                var targetIndex = Random.Range(0, targets.Count);

                var target = targets[targetIndex];
                yield return new WaitForSeconds(1f);

                var targetType = GameManager.Instance.GetTargetType(pawn, target);
                pawn.Action.TryUse(target, targetType);
            }
        }
    }
}