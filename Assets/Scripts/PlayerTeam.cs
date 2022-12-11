
using UnityEngine;

namespace TestGwentGame {
    public class PlayerTeam : BaseTeam {
        public override TeamType TeamType => TeamType.Player;

        public override void StartTurn() {
            base.StartTurn();
            Debug.Log("<color=green>It's player turn!</color>");
            //Allow player to use his cards or something
        }
    }
}
