using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    [Serializable]
    public abstract class BaseStatusEffect {
        [SerializeField] protected int _duration;
        
        protected int  _turnsLeft;
        protected Pawn _target;
        
        public abstract StatusEffectType EffectType { get; }

        public BaseStatusEffect() {}

        protected BaseStatusEffect(int duration) {
            _duration = duration;
        }

        public bool IsEffectActive() => _turnsLeft == 0;

        public virtual void StartEffect(Pawn target) {
            _turnsLeft = _duration;
            _target = target;
        }

        public virtual void RemoveEffect() { }

        public virtual void Tick() {
            _turnsLeft -= 1;
            if (!IsEffectActive()) {
                RemoveEffect();
            }
        }

        public static BaseStatusEffect SpawnFromEffectType(StatusEffectType type) {
            switch (type) {
                case StatusEffectType.Attack:
                    return new AttackStatusEffect();
                default:
                    Debug.LogError($"BaseStatusEffect.SpawnFromEffectType: type '{type}' is not supported");
                    return null;
            }
        }

        public abstract BaseStatusEffect Clone();
    }
}
