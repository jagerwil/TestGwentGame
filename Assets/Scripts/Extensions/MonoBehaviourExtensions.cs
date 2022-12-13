using UnityEngine;

namespace TestGwentGame {
    public static class MonoBehaviourExtensions {
        public static T CreateInstance<T>(this T component, Transform parent) where T : MonoBehaviour {
            return Object.Instantiate(component, parent);
        }
    }
}
