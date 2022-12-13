using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public class PawnHealthInfo {
        public int  Health { get; private set; }
        public bool IsDead => Health <= 0;

        public event Action onHealthChanged;

        public int TotalExtraHealth {
            get {
                var extraHealth = 0;
                foreach ( var health in _extraHealthContainer ) {
                    extraHealth += health.Value;
                }
                return extraHealth;
            }
        }

        SortedList<int, int> _extraHealthContainer; //key - turns left, value - extra health amount

        public PawnHealthInfo(int health) {
            Health      = health;
            _extraHealthContainer = new SortedList<int, int>();
        }

        public void AddHealth(int deltaHealth) {
            Health += deltaHealth;
            onHealthChanged?.Invoke();
        }

        public void AddExtraHealth(int deltaHealth, int duration) {
            if (_extraHealthContainer.ContainsKey(duration)) {
                _extraHealthContainer[duration] += deltaHealth;
                return;
            }

            _extraHealthContainer.Add(duration, deltaHealth);
            onHealthChanged?.Invoke();
        }

        public void TakeDamage(int damage) {
            if (_extraHealthContainer.Count == 0) {
                ReduceHealth(damage);
                onHealthChanged?.Invoke();
                return;
            }

            var damageLeft = damage;
            List<int> keysToRemove = new List<int>();
            
            foreach (var healthPair in _extraHealthContainer) {
                var turnsLeft = healthPair.Key;
                var health    = healthPair.Value;
                var deltaHealth = Mathf.Min(health, damageLeft);

                health -= deltaHealth;
                if (health > 0) {
                    _extraHealthContainer[turnsLeft] = health;
                } else {
                    keysToRemove.Add(healthPair.Key);
                }

                damageLeft -= deltaHealth;
                if (damageLeft == 0) {
                    break;
                }
            }

            foreach (var key in keysToRemove) {
                _extraHealthContainer.Remove(key);
            }

            if (damageLeft > 0) {
                ReduceHealth(damageLeft);
            }

            onHealthChanged?.Invoke();
        }

        public void StartTurn() {
            if (_extraHealthContainer.Count == 0) {
                return;
            }

            var oldValues = new SortedList<int, int>(_extraHealthContainer);
            foreach (var healthPair in oldValues) {
                var oldDuration = healthPair.Key;
                _extraHealthContainer[oldDuration] = 0;

                if (oldDuration <= 1) {
                    continue;
                }

                var newDuration = oldDuration - 1;
                var health = healthPair.Value;

                if (_extraHealthContainer.ContainsKey(newDuration)) {
                    _extraHealthContainer[newDuration] = health;
                    return;
                }

                _extraHealthContainer.Add(newDuration, health);
            }
        }

        void ReduceHealth(int value) {
            Health = Mathf.Max(Health - value, 0);
        }
    }
}

