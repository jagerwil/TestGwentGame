using TestGwentGame.Gameplay;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class PawnUi : MonoBehaviour {
        [SerializeField] Canvas   _canvas;
        [SerializeField] HealthUi _healthUi;
        [SerializeField] HealthUi _extraHealthUi;

        Pawn _pawn;

        public void Setup(Pawn pawn, Camera worldCamera) {
            _pawn = pawn;
            _canvas.worldCamera = worldCamera;
            SetupPawnHealth();
        }

        void OnEnable() {
            EventManager.onPawnHealthChanged.AddListener(OnHealthChanged);
        }

        void OnDisable() {
            EventManager.onPawnHealthChanged.RemoveListener(OnHealthChanged);
        }

        void SetupPawnHealth() {
            if (!_pawn) {
                return;
            }

            var isDead = _pawn.IsDead;
            gameObject.SetActive(!isDead);
            if ( isDead ) {
                return;
            }

            var healthInfo = _pawn.HealthInfo;
            _healthUi.SetupHealth(healthInfo.Health);
            _extraHealthUi.SetupHealth(healthInfo.TotalExtraHealth);
        }

        void OnHealthChanged(Pawn pawn) {
            if ( pawn != _pawn ) {
                return;
            }
            
            SetupPawnHealth();
        }
    }
}
