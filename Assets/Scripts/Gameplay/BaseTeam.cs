using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public abstract class BaseTeam : MonoBehaviour {
        [SerializeField] int   _teamSize;
        [SerializeField] float _distanceBetweenPawns;
        [SerializeField] List<Pawn> _prefabs;

        protected List<Pawn> _pawns = new();
        
        public abstract TeamType TeamType { get; }

        public List<Pawn> Pawns => _pawns;

        void Awake() => gameObject.SetActive(true);

        public void Init() {
            SpawnPawns();
            foreach (var pawn in _pawns) {
                pawn.Init(OnPawnUsed);
            }
        }

        public virtual void Refresh() {
            gameObject.SetActive(true);
            foreach (var pawn in _pawns) {
                pawn.Refresh();
            }
        }

        void OnEnable() {
            EventManager.onPawnDied.AddListener(OnPawnDied);
        }

        void OnDisable() {
            EventManager.onPawnDied.RemoveListener(OnPawnDied);
        }

        public virtual void StartTeamTurn() {
            foreach (var pawn in _pawns) {
                pawn.StartTeamTurn();
            }
        }

        public void EndTeamTurn() {
            EventManager.onTeamTurnEnded.Invoke(TeamType);
        }

        void SpawnPawns() {
            _pawns.Clear();
            for (var i = 0; i < _teamSize; i++) {
                var index = Random.Range(0, _prefabs.Count);
                var prefab = _prefabs[index];

                var pawn = prefab.CreateInstance(transform);
                _pawns.Add(pawn);
            }

            var totalDistance = _distanceBetweenPawns * (_teamSize - 1);
            var startPosX = totalDistance * 0.5f;
            for (var i = 0; i < _teamSize; i++) {
                _pawns[i].transform.localPosition = new Vector3(startPosX - i * _distanceBetweenPawns, 0f, 0f);
            }
        }

        void OnPawnDied(Pawn pawn) {
            if (_pawns.All(pawn => pawn.IsDead)) {
                Debug.Log("The entire team is ded!");
                gameObject.SetActive(false);
                EventManager.onTeamDied.Invoke(TeamType);
            }
        }

        void OnPawnUsed() {
            if ( _pawns.All(a => a.IsDead || a.Action.WasUsed )) {
                EndTeamTurn();
            }
        }
    }
}
