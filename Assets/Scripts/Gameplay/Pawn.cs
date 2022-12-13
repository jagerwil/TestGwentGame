using UnityEngine;

namespace TestGwentGame.Gameplay {
    [RequireComponent(typeof(PawnAction))]
    public sealed class Pawn : MonoBehaviour {
        [SerializeField] int        _maxHealth = 10;
        [SerializeField] PawnAction _action;

        public PawnHealthInfo HealthInfo { get; private set; }
        public PawnAction Action => _action;
        public bool       IsDead => HealthInfo.IsDead;

        public string DebugName => $"{transform.parent.name}/{gameObject.name}";

        void Awake() {
            gameObject.SetActive(true);
            HealthInfo = new PawnHealthInfo(_maxHealth);
        }

        void OnEnable() {
            HealthInfo.onHealthChanged += OnHealthChanged;
        }

        void OnDisable() {
            HealthInfo.onHealthChanged -= OnHealthChanged;
        }

        public void StartTurn() {
            HealthInfo.StartTurn();
            Action.RefreshUsages();
        }

        public bool TryUse(Pawn target) {
            if ( IsDead ) {
                return false;
            }
            return _action.TryUse(target);
        }

        void Die() {
            Debug.Log("Pawn is ded");
            gameObject.SetActive(false);
            EventManager.onPawnDied.Invoke(this);
        }

        void OnHealthChanged() {
            EventManager.onPawnHealthChanged.Invoke(this);
            if ( IsDead ) {
                Die();
            }
        }
    }
}
