using UnityEngine;

namespace TestGwentGame.Gameplay {
    public sealed class AttackAction : PawnAction {
        [SerializeField] int _damage = 3;
        
        protected override TargetType TargetType => TargetType.Enemy;

        public override bool TryUse(Pawn target, TargetType targetType) {
            if ( !base.TryUse(target, targetType) ) {
                return false;
            }

            target.HealthInfo.TakeDamage(_damage);
            return true;
        }
    }
}
