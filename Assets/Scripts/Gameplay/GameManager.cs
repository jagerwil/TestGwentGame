using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGwentGame.Gameplay {
    public sealed class GameManager : Singleton<GameManager> {
        [SerializeField] BaseTeam _playerTeam;
        [SerializeField] BaseTeam _aiTeam;
        [SerializeField] StatusEffectsDatabase _effectsDatabase;

        bool     _isGameEnded;
        int      _currentTurn;
        BaseTeam _currentTeam;

        public BaseTeam PlayerTeam => _playerTeam;
        public BaseTeam AiTeam     => _aiTeam;
        public StatusEffectsDatabase EffectsDatabase => _effectsDatabase;

        protected override void Awake() {
            base.Awake();
            Setup();
        }

        void OnEnable() {
            EventManager.onTeamTurnEnded.AddListener(OnTeamTurnEnded);
            EventManager.onTeamDied.AddListener(OnTeamDied);

            EventManager.Input.onRefreshKeyPressed.AddListener(Refresh);
            EventManager.Input.onEndTurnKeyPressed.AddListener(EndPlayerTurn);
        }

        void OnDisable() {
            EventManager.onTeamTurnEnded.RemoveListener(OnTeamTurnEnded);
            EventManager.onTeamDied.RemoveListener(OnTeamDied);

            EventManager.Input.onRefreshKeyPressed.RemoveListener(Refresh);
            EventManager.Input.onEndTurnKeyPressed.RemoveListener(EndPlayerTurn);
        }

        void Start() => Init();

        public void EndPlayerTurn() {
            if (_currentTeam == _playerTeam) {
                _currentTeam.EndTeamTurn();
            }
        }

        public List<Pawn> GetPawnTargets(Pawn pawn) {
            var allyTeam  = GetPawnTeam(pawn);
            var enemyTeam = GetEnemyTeam(allyTeam);

            var allyPawns  = allyTeam.Pawns;
            var enemyPawns = enemyTeam.Pawns;
            
            var effect  = pawn.Action;
            var targets = new List<Pawn>(allyPawns.Count + enemyPawns.Count);

            if (effect.CanUseOn(TargetType.Enemy)) {
                targets.AddRange(enemyPawns.Where(enemyPawn => !enemyPawn.IsDead));
            }

            if (effect.CanUseOn(TargetType.Ally)) {
                targets.AddRange(allyPawns.Where(allyPawn => !allyPawn.IsDead && allyPawn != pawn));
            }

            if (effect.CanUseOn(TargetType.Self)) {
                targets.Add(pawn);
            }

            return targets;
        }

        public TargetType GetTargetType(Pawn pawn, Pawn target) {
            var allyPawns  = _currentTeam.Pawns;

            if (pawn == target) {
                return TargetType.Self;
            }

            var isSameTeam = allyPawns.Contains(target);
            if (isSameTeam) {
                return TargetType.Ally;
            }

            return TargetType.Enemy;
        }

        void Setup() {
            _isGameEnded = false;
            _currentTeam = _playerTeam;
            _currentTurn = 0;
            EndTurn();
        }

        void Init() {
            _playerTeam.Init();
            _aiTeam.Init();

            _currentTeam.StartTeamTurn();
        }

        void Refresh() {
            Setup();
            _playerTeam.Refresh();
            _aiTeam.Refresh();
            _currentTeam.StartTeamTurn();
        }

        void SwitchSides() {
            if (_isGameEnded) {
                return;
            }

            if (_currentTeam == _aiTeam) {
                EndTurn();
            }

            _currentTeam = GetEnemyTeam(_currentTeam);
            _currentTeam.StartTeamTurn();
        }

        void EndTurn() {
            _currentTurn += 1;
            EventManager.onTurnStarted.Invoke(_currentTurn);
        }

        BaseTeam GetPawnTeam(Pawn pawn) {
            var isPlayerPawn = _playerTeam.Pawns.Any(playerPawn => playerPawn == pawn);
            return isPlayerPawn ? _playerTeam : _aiTeam;
        }

        BaseTeam GetEnemyTeam(BaseTeam team) {
            return team == _playerTeam ? _aiTeam : _playerTeam;
        }

        void OnTeamTurnEnded(TeamType teamType) => SwitchSides();
        void OnTeamDied(TeamType teamType) => _isGameEnded = true;
    }
}


