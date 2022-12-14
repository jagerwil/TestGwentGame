using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public class PawnAction : MonoBehaviour {
        [SerializeField] string _effectId;

        Action _actionUsedCallback;
        BaseStatusEffect _statusEffect;
        TargetType _targetType;

        BaseStatusEffect StatusEffect {
            get {
                if (_statusEffect == null) {
                    var effectInfo = GameManager.Instance.EffectsDatabase.GetStatusEffectInfo(_effectId);
                    _statusEffect  = effectInfo?.Effect;
                }
                return _statusEffect;
            }
        }

        TargetType TargetType {
            get {
                if (_targetType == TargetType.None) {
                    _targetType = GameManager.Instance.EffectsDatabase.GetTargetType(StatusEffect.EffectType);
                }
                return _targetType;
            }
        }

        public bool WasUsed { get; private set; } = false;

        public void RefreshUsages() => WasUsed = false;

        public void Setup(Action actionUsedCallback) {
            _actionUsedCallback = actionUsedCallback;
        }

        public virtual bool TryUse(Pawn target, TargetType targetType) {
            if (!CanUseOn(targetType)) {
                return false;
            }

            if ( WasUsed ) {
                return false;
            }

            WasUsed = true;
            target.StatusEffects.AddStatusEffect(StatusEffect.Clone());
            _actionUsedCallback?.Invoke();
            return true;
        }

        public bool CanUseOn(TargetType target) {
            return (TargetType & target) == target;
        }
    }
}
