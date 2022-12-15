using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TestGwentGame.Gameplay;
using UnityEngine;

namespace TestGwentGame {
    [CreateAssetMenu(fileName = "Status Effects Database", menuName = "Scriptable Objects/Status Effects Database", order = 1)]
    public sealed class StatusEffectsDatabase : ScriptableObject {
        [SerializeField] List<StatusEffectTypes> _effectTypes;
        [SerializeField] List<StatusEffectInfo> _effects;

        Dictionary<StatusEffectType, TargetType> _effectTypesDict;
        Dictionary<string, StatusEffectInfo> _effectsDict;

        public List<StatusEffectInfo> Effects => _effects;

        public TargetType GetTargetType(StatusEffectType type) {
            if ( _effectTypesDict == null ) {
                SetupDictionaries();
            }
            if ( _effectTypesDict.TryGetValue(type, out var targetType) ) {
                return targetType;
            }
            return TargetType.None;
        }

        [CanBeNull]
        public StatusEffectInfo GetStatusEffectInfo(string id) {
            if ( _effectsDict == null ) {
                SetupDictionaries();
            }
            if ( _effectsDict.TryGetValue(id, out var effectInfo) ) {
                return effectInfo;
            }
            return null;
        }

        void SetupDictionaries() {
            _effectTypesDict = new Dictionary<StatusEffectType, TargetType>();
            foreach (var effectType in _effectTypes) {
                _effectTypesDict.Add(effectType.Type, effectType.TargetType);
            }
            
            _effectsDict = new Dictionary<string, StatusEffectInfo>();
            foreach ( var effect in _effects ) {
                _effectsDict.Add(effect.Id, effect);
            }
        }
    }

    [Serializable]
    public class StatusEffectTypes {
        public StatusEffectType Type;
        public TargetType TargetType;
    }

    [Serializable]
    public class StatusEffectInfo {
        public string Id;
        public StatusEffectType Type;
        [SerializeReference] public BaseStatusEffect Effect;
        public Sprite Icon;
        public Color IconColor = Color.white;
    }
}

