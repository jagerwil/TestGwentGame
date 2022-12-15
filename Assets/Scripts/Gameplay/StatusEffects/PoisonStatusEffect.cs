using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    [Serializable]
    public sealed class PoisonStatusEffect : BaseStatusEffect {
        [SerializeField] int _damage;

        public override StatusEffectType EffectType => StatusEffectType.Poison;
        
        public PoisonStatusEffect() { }
        public PoisonStatusEffect(int duration, int damage) : base(duration) {
            _damage = damage;
        }

        public override void Tick() {
            base.Tick();
            _target.Health.TakeDamage(_damage);
        }

        public override BaseStatusEffect Clone() => new PoisonStatusEffect(_duration, _damage);
    }
}

