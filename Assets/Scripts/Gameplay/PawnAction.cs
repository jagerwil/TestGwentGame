using UnityEngine;

namespace TestGwentGame {
    public abstract class PawnAction : MonoBehaviour {
        public bool WasUsed { get; private set; } = false;

        public void RefreshUsages() => WasUsed = false;

        protected abstract TargetType TargetType { get; }

        public virtual bool TryUse(Pawn target) {
            if ( WasUsed ) {
                return false;
            }

            WasUsed = true;
            return true;
        }

        public bool CanUseOn(TargetType target) {
            return (TargetType & target) == target;
        }
    }
}
