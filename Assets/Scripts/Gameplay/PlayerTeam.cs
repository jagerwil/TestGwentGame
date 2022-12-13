using UnityEngine;

namespace TestGwentGame {
    public class PlayerTeam : BaseTeam {
        public override void StartTeamTurn() {
            base.StartTeamTurn();
            Debug.Log("<color=green>It's player turn!</color>");
            //Allow player to use his cards or something

            //Code for testing
            foreach (var pawn in _pawns) {
                var targets = GetPawnTargets(pawn);
                var targetIndex = Random.Range(0, targets.Count);

                var target = targets[targetIndex];

                Debug.Log($"Use effect of pawn {pawn.DebugName} on target {target.DebugName}");
                UsePawn(pawn, target);
            }
        }
    }
}
