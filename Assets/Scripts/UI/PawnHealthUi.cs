using TMPro;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class PawnHealthUi : MonoBehaviour {
        [SerializeField] TMP_Text _text;

        public void SetupHealth(int health) {
            var hasHealth = health > 0;
            gameObject.SetActive(hasHealth);

            if ( !hasHealth ) {
                return;
            }
            _text.text = health.ToString();
        }
    }
}
