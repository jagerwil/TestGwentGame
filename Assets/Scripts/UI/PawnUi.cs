using TestGwentGame.Gameplay;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class PawnUi : MonoBehaviour {
        [SerializeField] PawnHealthUi _healthUi;
        [SerializeField] PawnHealthUi _extraHealthUi;
        [SerializeField] PawnActionUi _actionUi;

        Pawn _pawn;

        public void Setup(Pawn pawn) {
            _pawn = pawn;
            SetupPawnHealth();
            _actionUi.Setup(pawn);
        }

        public void Refresh() {
            SetupPawnHealth();
            _actionUi.Refresh();
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
