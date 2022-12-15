using System.Collections.Generic;

namespace TestGwentGame.Gameplay {
    public sealed class PawnStatusEffects {
        Pawn _owner;
        Dictionary<StatusEffectType, StatusEffectsContainer> _statusEffects = new();

        public void Setup(Pawn owner) {
            _owner = owner;
        }

        public void Refresh() {
            foreach (var effect in _statusEffects.Values) {
                effect.RemoveAllEffects();
            }
        }

        public void StartTurn(int turn) {
            foreach ( var effectPair in _statusEffects ) {
                var effect = effectPair.Value;
                var effectType = effectPair.Key;

                if (!effect.HasEffects) {
                    continue;
                }

                effect.Tick();
                if ( !effect.HasEffects ) {
                    EventManager.onPawnStatusEffectEnded.Invoke(_owner, effectType);
                }
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

            var hadEffects = container.HasEffects;
            container.AddEffect(effect);
            if ( !hadEffects ) {
                EventManager.onPawnStatusEffectStarted.Invoke(_owner, effectType);
            }
        }

        public void RemoveStatusEffect(StatusEffectType type) {
            if ( _statusEffects.TryGetValue(type, out var container) ) {
                if ( !container.HasEffects ) {
                    return;
                }

                container.RemoveAllEffects();
                EventManager.onPawnStatusEffectEnded.Invoke(_owner, type);
            }
        }

        public bool HaveStatusEffect(StatusEffectType type) {
            if ( _statusEffects.TryGetValue(type, out var container) ) {
                return container.HasEffects;
            }
            return false;
        }
    }
}
