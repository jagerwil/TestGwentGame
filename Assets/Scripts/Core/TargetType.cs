using System;

namespace TestGwentGame {
    [Flags]
    public enum TargetType {
        None  = 0,
        Enemy = 1,
        Ally  = 2,
        Self  = 4,
    }
}
