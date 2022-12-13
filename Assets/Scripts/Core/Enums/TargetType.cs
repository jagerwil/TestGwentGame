using System;

namespace TestGwentGame {
    [Flags]
    public enum TargetType {
        Enemy = 1,
        Ally  = 2,
        Self  = 4,
    }
}
