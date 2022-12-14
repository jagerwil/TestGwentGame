using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    [Serializable]
    public sealed class AttackStatusEffect : BaseStatusEffect {
        [SerializeField] int _damage;

        public override StatusEffectType EffectType => StatusEffectType.Attack;
        
        public AttackStatusEffect() { }
        public AttackStatusEffect(int duration, int damage) : base(duration) {
            _damage = damage;
        }

        public override void StartEffect(Pawn target) {
            base.StartEffect(target);
            target.Health.TakeDamage(_damage);
        }

        public override BaseStatusEffect Clone() => new AttackStatusEffect(_duration, _damage);
    }
}

