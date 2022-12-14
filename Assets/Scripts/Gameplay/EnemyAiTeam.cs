using System.Collections;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public sealed class EnemyAiTeam : BaseTeam {
        public override TeamType TeamType => TeamType.Ai;

        Coroutine _startTurnCoro;
        
        public override void StartTeamTurn() {
            base.StartTeamTurn();
            Debug.Log("<color=orange>It's enemy turn!</color>");
            //Randomly apply effects to player cards
            _startTurnCoro = StartCoroutine(StartTurnCoro());
        }

        public override void Refresh() {
            base.Refresh();
            if (_startTurnCoro != null) {
                StopCoroutine(_startTurnCoro);
            }
        }

        IEnumerator StartTurnCoro() {
            //Code for testing
            foreach (var pawn in _pawns) {
                var targets = GameManager.Instance.GetPawnTargets(pawn);
                var targetIndex = Random.Range(0, targets.Count);

                var target = targets[targetIndex];

                yield return new WaitForSeconds(1f);
                Debug.Log($"Use effect of pawn {pawn.DebugName} on target {target.DebugName}");

                var targetType = GameManager.Instance.GetTargetType(pawn, target);
                pawn.Action.TryUse(target, targetType);
            }
        }
    }
}