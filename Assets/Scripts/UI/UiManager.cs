using System.Collections.Generic;
using TestGwentGame.Gameplay;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class UiManager : Singleton<UiManager> {
        [SerializeField] PawnUi    _playerPawnUiPrefab;
        [SerializeField] PawnUi    _aiPawnUiPrefab;
        [SerializeField] Transform _pawnsUiHolder;
        [Space]
        [SerializeField] Transform _dragAndDropRoot;
        [SerializeField] Transform _uiPlacementHeight;

        public Transform DragAndDropRoot => _dragAndDropRoot;

        List<PawnUi> _pawnsUi = new List<PawnUi>();

        public void Refresh() {
            foreach (var pawnUi in _pawnsUi) {
                pawnUi.Refresh();
            }
        }

        void Start() {
            var mainCamera = Camera.main;
            var placementHeight = _uiPlacementHeight.position.y;

            var playerTeam = GameManager.Instance.PlayerTeam;
            SpawnTeamUi(playerTeam, _playerPawnUiPrefab, mainCamera, placementHeight);

            var aiTeam = GameManager.Instance.AiTeam;
            SpawnTeamUi(aiTeam, _aiPawnUiPrefab, mainCamera, placementHeight);
        }

        void SpawnTeamUi(BaseTeam team, PawnUi prefab, Camera mainCamera, float placementHeight) {
            var pawns = team.Pawns;
            foreach ( var pawn in pawns ) {
                var uiWorldPos = pawn.transform.position;
                uiWorldPos.y = placementHeight;

                var uiScreenPos = mainCamera.WorldToScreenPoint(uiWorldPos);
                uiScreenPos.z = 0f;

                var pawnUi = prefab.CreateInstance(_pawnsUiHolder);
                pawnUi.transform.position = uiScreenPos;
                
                pawnUi.Setup(pawn);
                _pawnsUi.Add(pawnUi);
            }
        }
    }
}
