using System.Collections.Generic;
using TestGwentGame.Gameplay;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class PawnsUiHolder : MonoBehaviour {
        [SerializeField] PawnUi    _playerPawnUiPrefab;
        [SerializeField] PawnUi    _aiPawnUiPrefab;
        [Space]
        [SerializeField] Transform _uiPlacementHeight;

        List<PawnUi> _pawnsUi = new();

        void Start() {
            var mainCamera = Camera.main;
            var placementHeight = _uiPlacementHeight.position.y;

            var playerTeam = GameManager.Instance.PlayerTeam;
            SpawnTeamUi(playerTeam, _playerPawnUiPrefab, mainCamera, placementHeight);

            var aiTeam = GameManager.Instance.AiTeam;
            SpawnTeamUi(aiTeam, _aiPawnUiPrefab, mainCamera, placementHeight);

        }

        public void Refresh() {
            foreach (var pawnUi in _pawnsUi) {
                pawnUi.Refresh();
            }
        }

        void SpawnTeamUi(BaseTeam team, PawnUi prefab, Camera mainCamera, float placementHeight) {
            var pawns = team.Pawns;
            foreach ( var pawn in pawns ) {
                var uiWorldPos = pawn.transform.position;
                uiWorldPos.y = placementHeight;

                var uiScreenPos = mainCamera.WorldToScreenPoint(uiWorldPos);
                uiScreenPos.z = 0f;

                var pawnUi = prefab.CreateInstance(transform);
                pawnUi.transform.position = uiScreenPos;
                
                pawnUi.Setup(pawn);
                _pawnsUi.Add(pawnUi);
            }
        }
    }
}
