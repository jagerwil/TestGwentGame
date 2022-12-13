using TestGwentGame.Gameplay;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class UiManager : MonoBehaviour {
        [SerializeField] PawnUi PawnUiPrefab;

        void Start() {
            var worldCamera = Camera.main;
            
            var playerTeam = GameManager.Instance.PlayerTeam;
            SpawnTeamUi(playerTeam, worldCamera);

            var aiTeam = GameManager.Instance.AiTeam;
            SpawnTeamUi(aiTeam, worldCamera);
        }

        void SpawnTeamUi(BaseTeam team, Camera worldCamera) {
            var pawns = team.Pawns;
            foreach ( var pawn in pawns ) {
                var pawnUi = PawnUiPrefab.CreateInstance(pawn.transform);
                pawnUi.Setup(pawn, worldCamera);
            }
        }
    }
}
