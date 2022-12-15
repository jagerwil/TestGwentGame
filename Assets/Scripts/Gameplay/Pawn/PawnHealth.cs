using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public sealed class PawnHealth : MonoBehaviour {
        [SerializeField] int _maxHealth = 10;
        int _maxExtraHealth;

        public int Health { get; private set; }
        public int ExtraHealth { get; private set; }

        public bool IsDead => Health <= 0;

        public event Action onHealthChanged;

        void Awake() => Setup();

        void Setup() {
            Health = _maxHealth;
            ExtraHealth = 0;
            _maxExtraHealth = 0;
        }

        public void Refresh() => Setup();

        public void AddHealth(int deltaHealth) {
            Health += deltaHealth;
            onHealthChanged?.Invoke();
        }

        public void AddExtraHealth(int deltaHealth) {
            ExtraHealth += deltaHealth;
            _maxExtraHealth += deltaHealth;

            ExtraHealth = Mathf.Clamp(ExtraHealth, 0, _maxExtraHealth);
            onHealthChanged?.Invoke();
        }

        public void RemoveExtraHealth(int deltaHealth) {
            _maxExtraHealth -= deltaHealth;
            ExtraHealth = Mathf.Clamp(ExtraHealth, 0, _maxExtraHealth);
            onHealthChanged?.Invoke();
        }

        public void TakeDamage(int damage) {
            var damageLeft = damage;

            var deltaHealth = Mathf.Min(ExtraHealth, damage);
            ExtraHealth -= deltaHealth;
            damageLeft -= deltaHealth;

            if ( damageLeft > 0 ) {
                ReduceHealth(damageLeft);
            }

            onHealthChanged?.Invoke();
        }

        void ReduceHealth(int value) {
            Health = Mathf.Max(Health - value, 0);
        }
    }
}

