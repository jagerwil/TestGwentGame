using UnityEngine;

namespace TestGwentGame {
    public class EnemyAiTeam : BaseTeam {
        public override void StartTeamTurn() {
            base.StartTeamTurn();
            Debug.Log("<color=orange>It's enemy turn!</color>");
            //Randomly apply effects to player cards

            foreach (var pawn in _pawns) {
                var targets = GetPawnTargets(pawn);
                var targetIndex = Random.Range(0, targets.Count);

                var target = targets[targetIndex];
                UsePawn(pawn, target);

                Debug.Log($"Use effect of pawn {pawn.DebugName} on target {target.DebugName}");
            }
        }
    }
}