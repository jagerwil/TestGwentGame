using UnityEngine;

namespace TestGwentGame {
    public class EnemyTeam : BaseTeam {
        public override TeamType TeamType => TeamType.Enemy;

        public override void StartTurn() {
            base.StartTurn();
            Debug.Log("<color=orange>It's enemy turn!</color>");
            //Randomly apply effects to player cards
        }
    }
}