using TestGwentGame.Gameplay;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class PawnUi : MonoBehaviour {
        [SerializeField] PawnHealthUi _healthUi;
        [SerializeField] PawnHealthUi _extraHealthUi;
        [SerializeField] GameObject   _poisonIcon;
        [SerializeField] PawnActionUi _actionUi;

        Pawn _pawn;

        public void Setup(Pawn pawn) {
            _pawn = pawn;
            SetupPawnHealth();
            SetupPoisonIcon();
            _actionUi.Setup(pawn);
        }

        public void Refresh() {
            SetupPawnHealth();
            SetupPoisonIcon();

            _actionUi.Refresh();
        }

        void OnEnable() {
            EventManager.onPawnStatusEffectStarted.AddListener(OnStatusEffectStarted);
            EventManager.onPawnStatusEffectEnded.AddListener(OnStatusEffectEnded);
            EventManager.onPawnHealthChanged.AddListener(OnHealthChanged);
        }

        void OnDisable() {
            EventManager.onPawnStatusEffectStarted.RemoveListener(OnStatusEffectStarted);
            EventManager.onPawnStatusEffectEnded.RemoveListener(OnStatusEffectEnded);
            EventManager.onPawnHealthChanged.RemoveListener(OnHealthChanged);
        }

        void SetupPawnHealth() {
            if ( !_pawn ) {
                return;
            }

            var isDead = _pawn.IsDead;
            gameObject.SetActive(!isDead);
            if ( isDead ) {
                return;
            }

            var healthInfo = _pawn.Health;
            _healthUi.SetupHealth(healthInfo.Health);
            _extraHealthUi.SetupHealth(healthInfo.ExtraHealth);
        }

        void SetupPoisonIcon() {
            var hasPoison = _pawn.StatusEffects.HaveStatusEffect(StatusEffectType.Poison);
            _poisonIcon.SetActive(hasPoison);
        }

        void OnStatusEffectStarted(Pawn pawn, StatusEffectType type) {
            if ( pawn != _pawn || type != StatusEffectType.Poison ) {
                return;
            }

            _poisonIcon.SetActive(true);
        }

        void OnStatusEffectEnded(Pawn pawn, StatusEffectType type) {
            if ( pawn != _pawn || type != StatusEffectType.Poison ) {
                return;
            }

            _poisonIcon.SetActive(false);
        }

        void OnHealthChanged(Pawn pawn) {
            if ( pawn != _pawn ) {
                return;
            }
            
            SetupPawnHealth();
        }
    }
}
