using System.Collections.Generic;

namespace TestGwentGame.Gameplay {
    public sealed class StatusEffectsContainer {
        List<BaseStatusEffect> _effects = new();

        public bool HasEffects => _effects.Count > 0;

        public void AddEffect(BaseStatusEffect effect) {
            _effects.Add(effect);
        }

        public void RemoveAllEffects() {
            _effects.Clear();
        }

        public void Tick() {
            foreach (var effect in _effects) {
                effect.Tick();
            }

            foreach (var effect in _effects) {
                if (!effect.IsEffectActive()) {
                    effect.RemoveEffect();
                }
            }

            _effects.RemoveAll(effect => !effect.IsEffectActive());
        }
    }
}
