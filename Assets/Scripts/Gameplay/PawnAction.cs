using System;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public abstract class PawnAction : MonoBehaviour {
        Action _actionUsedCallback;
        
        public bool WasUsed { get; private set; } = false;

        public void RefreshUsages() => WasUsed = false;

        protected abstract TargetType TargetType { get; }

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
            _actionUsedCallback?.Invoke();
            return true;
        }

        public bool CanUseOn(TargetType target) {
            return (TargetType & target) == target;
        }
    }
}
