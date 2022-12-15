using UnityEngine;

namespace TestGwentGame {
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
        public static T Instance { get; private set; }

        protected virtual void Awake() {
            if ( Instance && this != Instance ) {
                Debug.LogError($"Error: scene contains multiple singletons of type {typeof(T).Name}");
                Destroy(this);
                return;
            }

            Instance = (T)this;
        }
    } 
}

