using System.Collections.Generic;

namespace TestGwentGame.Gameplay {
    public sealed class PawnStatusEffects {
        Pawn _owner;
        Dictionary<StatusEffectType, StatusEffectsContainer> _statusEffects = new();

        public void Setup(Pawn owner) {
            _owner = owner;
        }

        public void StartTurn(int turn) {
            foreach (var effect in _statusEffects.Values) {
                effect.Tick();
            }
        }

        public void AddStatusEffect(BaseStatusEffect effect) {
            effect.StartEffect(_owner);
            if ( !effect.IsEffectActive() ) {
                return;
            }

            var effectType = effect.EffectType;
            if ( !_statusEffects.TryGetValue(effectType, out var container) ) {
                container = new StatusEffectsContainer();
                _statusEffects.Add(effectType, container);
            }
            container.AddEffect(effect);
        }

        public void RemoveStatusEffect(StatusEffectType type) {
            if (_statusEffects.TryGetValue(type, out var container)) {
                container.RemoveAllEffects();
            }
        }
    }
}
