using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    [Serializable]
    public sealed class HealingStatusEffect : BaseStatusEffect {
        [SerializeField] int _healing;

        public override StatusEffectType EffectType => StatusEffectType.Healing;
        
        public HealingStatusEffect() { }
        public HealingStatusEffect(int duration, int healing) : base(duration) {
            _healing = healing;
        }

        public override void StartEffect(Pawn target) {
            base.StartEffect(target);
            target.StatusEffects.RemoveStatusEffect(StatusEffectType.Poison);
            target.Health.AddHealth(_healing);
        }

        public override BaseStatusEffect Clone() => new HealingStatusEffect(_duration, _healing);
    }
}

