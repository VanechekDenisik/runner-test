using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    /// <summary>
    ///     Класс отлавливает нажатие на спрайт (реализует интерфейс ITapCatcher).
    /// </summary>
    public class SpriteTapCatcher : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
        IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const float DoubleTapMaxDelay = 0.4f;
        private DateTime _lastClickTime;

        private bool _visible;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public bool Visible
        {
            get => _visible;
            set
            {
                var color = GetComponent<Image>().color;
                color.a = value ? 0.5f : 0.01f;
                GetComponent<Image>().color = color;
            }
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData data)
        {
            OnBeginDrag?.Invoke();
            OnBeginDragWithData?.Invoke(data);
        }

        void IDragHandler.OnDrag(PointerEventData data)
        {
            OnDrag?.Invoke();
        }

        void IEndDragHandler.OnEndDrag(PointerEventData data)
        {
            OnEndDrag?.Invoke();
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData data)
        {
            OnClick?.Invoke(data);

            if ((DateTime.Now - _lastClickTime).TotalSeconds < DoubleTapMaxDelay)
                OnDoubleClick?.Invoke();

            _lastClickTime = DateTime.Now;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData data)
        {
            OnDown?.Invoke();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData data)
        {
            OnEnter?.Invoke();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData data)
        {
            OnExit?.Invoke();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData data)
        {
            OnUp?.Invoke();
        }

        public event Action OnEnter;
        public event Action OnExit;

        public event Action OnDown;
        public event Action OnUp;
        public event Action<PointerEventData> OnClick;
        public event Action OnDoubleClick;

        public event Action OnBeginDrag;
        public event Action OnDrag;
        public event Action OnEndDrag;
        public event Action<PointerEventData> OnBeginDragWithData;
    }
}