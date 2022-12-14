using TestGwentGame.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace TestGwentGame.UI {
    public sealed class UiManager : Singleton<UiManager> {
        [SerializeField] PawnsUiHolder _pawnsUiHolder;
        [SerializeField] Transform     _dragAndDropRoot;
        [SerializeField] Button        _endTurnButton;
        [SerializeField] EndScreenUi   _endScreenUi;

        public Transform DragAndDropRoot => _dragAndDropRoot;

        protected override void Awake() {
            base.Awake();
            _endTurnButton.enabled = true;
            _endTurnButton.onClick.AddListener(GameManager.Instance.EndPlayerTurn);
        }

        void OnEnable() {
            EventManager.onTeamTurnEnded.AddListener(OnTeamTurnEnded);
            EventManager.Input.onRefreshKeyPressed.AddListener(Refresh);
        }

        void OnDisable() {
            EventManager.onTeamTurnEnded.RemoveListener(OnTeamTurnEnded);
            EventManager.Input.onRefreshKeyPressed.RemoveListener(Refresh);
        }

        void Refresh() {
            _endTurnButton.enabled = true;
            _pawnsUiHolder.Refresh();
            _endScreenUi.Refresh();
        }

        void OnTeamTurnEnded(TeamType teamType) {
            var isPlayerTurn = teamType == TeamType.Ai;
            _endTurnButton.enabled = isPlayerTurn;
        }
    }
}
