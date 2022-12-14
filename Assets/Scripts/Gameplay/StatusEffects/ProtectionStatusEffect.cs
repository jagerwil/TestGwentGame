using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    [Serializable]
    public sealed class ProtectionStatusEffect : BaseStatusEffect {
        [SerializeField] int _extraHealth;

        public override StatusEffectType EffectType => StatusEffectType.Protection;

        public ProtectionStatusEffect() { }
        public ProtectionStatusEffect(int duration, int extraHealth) : base(duration) {
            _extraHealth = extraHealth;
        }

        public override void StartEffect(Pawn target) {
            base.StartEffect(target);
            target.Health.AddExtraHealth(_extraHealth);
        }

        public override void RemoveEffect() {
            base.RemoveEffect();
            _target.Health.RemoveExtraHealth(_extraHealth);
        }

        public override BaseStatusEffect Clone() => new ProtectionStatusEffect(_duration, _extraHealth);
    }
}