using TMPro;
using UnityEngine;

namespace TestGwentGame.UI {
    public sealed class EndScreenUi : MonoBehaviour {
        [SerializeField] TMP_Text Text;
        [SerializeField] Color _playerWinsColor  = Color.green;
        [SerializeField] Color _playerLosesColor = Color.red;
        [SerializeField] string _playerWinsText;
        [SerializeField] string _playerLosesText;

        void Awake() => Setup();

        public void Refresh() => Setup();

        void Setup() {
            gameObject.SetActive(false);
        }

        void OnEnable() {
            EventManager.onTeamDied.AddListener(OnTeamDied);
        }

        void OnDisable() {
            EventManager.onTeamDied.AddListener(OnTeamDied);
        }

        void OnTeamDied(TeamType teamType) {
            gameObject.SetActive(true);
            var isPlayerWon = teamType == TeamType.Ai;
            Text.color = isPlayerWon ? _playerWinsColor : _playerLosesColor;
            Text.text  = isPlayerWon ? _playerWinsText  : _playerLosesText;
        }
    }
}

