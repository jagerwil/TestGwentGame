using UnityEngine;

namespace TestGwentGame {
    public sealed class InputManager : Singleton<InputManager> {
        [SerializeField] KeyCode _refreshKey;
        [SerializeField] KeyCode _endTurnKey;

        public KeyCode RefreshKey => _refreshKey;
        public KeyCode EndTurnKey => _endTurnKey;

        void Update() {
            if (Input.GetKeyDown(_refreshKey)) {
                EventManager.Input.onRefreshKeyPressed.Invoke();
            }

            if (Input.GetKeyDown(_endTurnKey)) {
                EventManager.Input.onEndTurnKeyPressed.Invoke();
            }
        }
    }
}
