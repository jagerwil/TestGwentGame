using UnityEngine;

namespace TestGwentGame {
    public abstract class BaseTeam : MonoBehaviour {
        [SerializeField] protected Pawn[] _cards;
        
        public abstract TeamType TeamType { get; }

        public virtual void StartTurn() {
        }

        public void PlayCard(Pawn target) {
            
        }
    }
}
