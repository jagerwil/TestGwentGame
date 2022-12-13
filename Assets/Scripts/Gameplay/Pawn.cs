using System;
using UnityEngine;

namespace TestGwentGame {
    [RequireComponent(typeof(PawnAction))]
    public sealed class Pawn : MonoBehaviour {
        [SerializeField] int    _maxHealth = 10;
        [SerializeField] PawnAction _action;

        int  _curHealth;

        public PawnAction Action => _action;
        public bool       IsDead => _curHealth == 0;

        public string DebugName => $"{transform.parent.name}/{gameObject.name}";

        void Awake() {
            gameObject.SetActive(true);
            _curHealth = _maxHealth;
        }

        public void StartTurn() => Action.RefreshUsages();

        public void ChangeHealth(int deltaHealth) {
            var newHealth = _curHealth + deltaHealth;
            _curHealth = Mathf.Clamp(newHealth, 0, _maxHealth);
            if ( IsDead ) {
                Die();
            }
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
    }
}
