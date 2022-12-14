using TestGwentGame.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestGwentGame.UI {
    public sealed class PawnActionUi : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
        Pawn _pawn;
        bool _isDragging;

        Vector3   _initialPosition = Vector3.zero;
        Transform _initialParent;
        Vector3   _mouseOffset;

        public void Setup(Pawn pawn) {
            _initialPosition = transform.localPosition;
            _initialParent   = transform.parent;
            _pawn = pawn;
        }

        void Update() {
            if ( !_isDragging ) {
                return;
            }
            transform.position = Input.mousePosition + _mouseOffset;
        }

        public void OnPointerDown(PointerEventData eventData) {
            _isDragging  = true;

            _mouseOffset = transform.position - Input.mousePosition;
            _mouseOffset.z = 0f;
            transform.SetParent(UiManager.Instance.DragAndDropRoot);
        }

        public void OnPointerUp(PointerEventData eventData) {
            _isDragging = false;

            transform.SetParent(_initialParent);
            transform.localPosition = _initialPosition;
        }
    }
}

