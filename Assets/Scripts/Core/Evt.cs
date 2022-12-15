using System;

namespace TestGwentGame {
    public sealed class Evt {
        event Action evt;

        public void AddListener(Action listener) {
            evt += listener;
        }

        public void RemoveListener(Action listener) {
            evt += listener;
        }

        public void Invoke() {
            evt?.Invoke();
        }
    }

    public sealed class Evt<T> {
        event Action<T> evt;

        public void AddListener(Action<T> listener) {
            evt += listener;
        }

        public void RemoveListener(Action<T> listener) {
            evt += listener;
        }

        public void Invoke(T value) {
            evt?.Invoke(value);
        }
    }

    public sealed class Evt<T, K> {
        event Action<T, K> evt;

        public void AddListener(Action<T, K> listener) {
            evt += listener;
        }

        public void RemoveListener(Action<T, K> listener) {
            evt += listener;
        }

        public void Invoke(T value1, K value2) {
            evt?.Invoke(value1, value2);
        }
    }
}

