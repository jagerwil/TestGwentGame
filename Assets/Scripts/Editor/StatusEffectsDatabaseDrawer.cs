using System.Collections.Generic;
using TestGwentGame.Gameplay;
using UnityEditor;
using UnityEngine;

namespace TestGwentGame.Editor {
    [CustomEditor(typeof(StatusEffectsDatabase))]
    public sealed class StatusEffectsDatabaseDrawer : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            var database = serializedObject.targetObject as StatusEffectsDatabase;
            var effectsInfo = database.Effects;
            ValidateEffectsInfo(effectsInfo);

            base.OnInspectorGUI();
        }

        void ValidateEffectsInfo(List<StatusEffectInfo> effectsInfo) {
            //Check if any effect is missing
            foreach ( var effectInfo in effectsInfo ) {
                var effect = effectInfo.Effect;
                var type = effectInfo.Type;
                if ( effect == null || effect.EffectType != type ) {
                    SpawnNewEffect(effectInfo);
                    Debug.Log("Spawn effect");
                }
            }

            //Check for effect duplicates
            for (var i = 0; i < effectsInfo.Count; i++) {
                for (var j = i + 1; j < effectsInfo.Count; j++) {
                    if (effectsInfo[i].Effect == effectsInfo[j].Effect) {
                        SpawnNewEffect(effectsInfo[j]);
                    }
                }
            }
        }

        void SpawnNewEffect(StatusEffectInfo effectInfo) {
            effectInfo.Effect = BaseStatusEffect.SpawnFromEffectType(effectInfo.Type);
        }
    }
}

