using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    [RequireComponent(typeof(PawnAction))]
    public sealed class Pawn : MonoBehaviour {
        [SerializeField] PawnHealth _health;
        [SerializeField] PawnAction _action;

        public PawnStatusEffects StatusEffects { get; } = new();
        
        public PawnHealth Health => _health;
        public PawnAction Action => _action;
        public bool IsDead => Health.IsDead;

        public string DebugName => $"{transform.parent.name}/{gameObject.name}";

        void Awake() => Setup();

        void Setup() {
            gameObject.SetActive(true);
            StatusEffects.Setup(this);
        }

        void OnEnable() {
            _health.onHealthChanged += OnHealthChanged;
            EventManager.onTurnStarted.AddListener(StatusEffects.StartTurn);
        }

        void OnDisable() {
            _health.onHealthChanged -= OnHealthChanged;
            EventManager.onTurnStarted.RemoveListener(StatusEffects.StartTurn);
        }

        public void Init(Action actionUsedCallback) {
            _action.Setup(actionUsedCallback);
        }

        public void Refresh() {
            Setup();
            _health.Refresh();
        }

        public void StartTeamTurn() {
            _action.RefreshUsages();
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