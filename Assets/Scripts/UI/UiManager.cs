using TestGwentGame.Gameplay;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class UiManager : Singleton<UiManager> {
        [SerializeField] PawnUi    _pawnUiPrefab;
        [SerializeField] Transform _pawnsUiHolder;
        [SerializeField] Transform _dragAndDropRoot;
        [SerializeField] Transform _uiPlacementHeight;

        public Transform DragAndDropRoot => _dragAndDropRoot;

        void Start() {
            var camera = Camera.main;
            var placementHeight = _uiPlacementHeight.position.y;

            var playerTeam = GameManager.Instance.PlayerTeam;
            SpawnTeamUi(playerTeam, camera, placementHeight);

            var aiTeam = GameManager.Instance.AiTeam;
            SpawnTeamUi(aiTeam, camera, placementHeight);
        }

        void SpawnTeamUi(BaseTeam team, Camera camera, float placementHeight) {
            var pawns = team.Pawns;
            foreach ( var pawn in pawns ) {
                var uiWorldPos = pawn.transform.position;
                uiWorldPos.y = placementHeight;

                var uiScreenPos = camera.WorldToScreenPoint(uiWorldPos);
                uiScreenPos.z = 0f;

                var pawnUi = _pawnUiPrefab.CreateInstance(_pawnsUiHolder);
                pawnUi.transform.position = uiScreenPos;
                
                pawnUi.Setup(pawn);
            }
        }
    }
}
