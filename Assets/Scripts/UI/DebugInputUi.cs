using TMPro;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class DebugInputUi : MonoBehaviour {
        [SerializeField] TMP_Text _text;
        [SerializeField] float _appearDuration;

        float _timeUntilHide = 0f;

        void Awake() {
            _text.gameObject.SetActive(false);
        }

        void OnEnable() {
            EventManager.Input.onRefreshKeyPressed.AddListener(SetupRefreshKeyText);
            EventManager.Input.onEndTurnKeyPressed.AddListener(SetupEndTurnKeyText);
        }

        void OnDisable() {
            EventManager.Input.onRefreshKeyPressed.RemoveListener(SetupRefreshKeyText);
            EventManager.Input.onEndTurnKeyPressed.RemoveListener(SetupEndTurnKeyText);
        }

        void Update() {
            if ( _timeUntilHide <= 0f ) {
                return;
            }

            _timeUntilHide -= Time.deltaTime;
            if ( _timeUntilHide <= 0f ) {
                _text.gameObject.SetActive(false);
            }
        }

        void SetupRefreshKeyText() => SetupText(InputManager.Instance.RefreshKey);
        void SetupEndTurnKeyText() => SetupText(InputManager.Instance.EndTurnKey);

        void SetupText(KeyCode key) {
            _text.gameObject.SetActive(true);
            _timeUntilHide = _appearDuration;
            _text.text = $"{key.ToString()} was pressed!";
        }
    }
}

