using TestGwentGame.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TestGwentGame.UI {
    public sealed class PawnActionUi : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
        [SerializeField] Image _icon;
        
        Pawn _pawn;
        bool _isDragging;

        Vector3   _initialPosition = Vector3.zero;
        Transform _initialParent;
        Vector3   _mouseOffset;

        public void Setup(Pawn pawn) {
            _initialPosition = transform.localPosition;
            _initialParent   = transform.parent;

            _pawn = pawn;

            var effectInfo = _pawn.Action.EffectInfo;
            _icon.sprite = effectInfo.Icon;
            _icon.color  = effectInfo.IconColor;
        }

        public void Refresh() {
            gameObject.SetActive(true);
            transform.localPosition = _initialPosition;
        }

        void OnEnable() {
            EventManager.onTurnStarted.AddListener(OnTurnStarted);
        }

        void OnDisable() {
            EventManager.onTurnStarted.RemoveListener(OnTurnStarted);
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

            var mainCamera = Camera.main;
            if ( !mainCamera ) {
                Debug.LogError("PawnActionUi.OnPointerUp: main camera is not found");
                return;
            }
            
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if ( !Physics.Raycast(ray, out var hit )) {
                return;
            }

            var targetPawn = hit.transform.GetComponent<Pawn>();
            if ( !targetPawn ) {
                return;
            }

            var targetType = GameManager.Instance.GetTargetType(_pawn, targetPawn);
            _pawn.Action.TryUse(targetPawn, targetType);

            gameObject.SetActive(!_pawn.Action.WasUsed);
        }

        void OnTurnStarted(int turn) {
            gameObject.SetActive(true);
        }
    }
}

