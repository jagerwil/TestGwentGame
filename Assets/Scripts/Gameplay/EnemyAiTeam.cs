using System.Collections;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public sealed class EnemyAiTeam : BaseTeam {
        Coroutine _startTurnCoro;
        
        public override void StartTeamTurn() {
            base.StartTeamTurn();
            Debug.Log("<color=orange>It's enemy turn!</color>");
            //Randomly apply effects to player cards
            _startTurnCoro = StartCoroutine(StartTurnCoro());
        }

        public override void Refresh() {
            base.Refresh();
            StopCoroutine(_startTurnCoro);
        }

        IEnumerator StartTurnCoro() {
            //Code for testing
            foreach (var pawn in _pawns) {
                var targets = GetPawnTargets(pawn);
                var targetIndex = Random.Range(0, targets.Count);

                var target = targets[targetIndex];

                yield return new WaitForSeconds(1f);
                Debug.Log($"Use effect of pawn {pawn.DebugName} on target {target.DebugName}");

                var targetType = GetTargetType(pawn, target);
                pawn.Action.TryUse(target, targetType);
            }
        }
    }
}