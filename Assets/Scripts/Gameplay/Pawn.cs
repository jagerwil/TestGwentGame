using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    [RequireComponent(typeof(PawnAction))]
    public sealed class Pawn : MonoBehaviour {
        [SerializeField] int        _maxHealth = 10;
        [SerializeField] PawnAction _action;

        BaseTeam _team;

        public PawnHealthInfo HealthInfo { get; } = new();
        public PawnAction Action => _action;
        public bool IsDead => HealthInfo.IsDead;

        public string DebugName => $"{transform.parent.name}/{gameObject.name}";

        void Awake() => Setup();

        void Setup() {
            gameObject.SetActive(true);
            HealthInfo.Setup(_maxHealth);
        }

        void OnEnable() {
            HealthInfo.onHealthChanged += OnHealthChanged;
            EventManager.onTurnStarted.AddListener(HealthInfo.StartTurn);
        }

        void OnDisable() {
            HealthInfo.onHealthChanged -= OnHealthChanged;
            EventManager.onTurnStarted.RemoveListener(HealthInfo.StartTurn);
        }

        public void Init(BaseTeam team, Action actionUsedCallback) {
            _team = team;
            _action.Setup(actionUsedCallback);
        }


        public void StartTeamTurn() {
            _action.RefreshUsages();
        }

        public bool TryUseAction(Pawn target) {
            var targetType = _team.GetTargetType(this, target);
            return _action.TryUse(target, targetType);
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
