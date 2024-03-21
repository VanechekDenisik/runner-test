using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class MyButton : Button
    {
        private bool _fakeDisabled;
        private Graphic[] _graphics;
        private MyButtonGraphics _targetGraphics;

        public bool FakeDisabled
        {
            get => _fakeDisabled;
            set
            {
                _fakeDisabled = value;
                DoStateTransition(currentSelectionState, false);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            onClick.AddListener(ClickEvent);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            onClick.RemoveListener(ClickEvent);
        }

        public event Action OnClick;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            if (!GetGraphics())
                return;

            var targetColor =
                state == SelectionState.Pressed && FakeDisabled ? colors.disabledColor * colors.pressedColor :
                state == SelectionState.Pressed ? colors.pressedColor :
                state == SelectionState.Disabled || FakeDisabled ? colors.disabledColor :
                state == SelectionState.Highlighted ? colors.highlightedColor :
                state == SelectionState.Normal ? colors.normalColor :
                state == SelectionState.Selected ? colors.selectedColor : Color.white;

            foreach (var graphic in _graphics)
            {
                if (graphic == null)
                    continue;

                graphic.CrossFadeColor(targetColor, instant ? 0 : colors.fadeDuration, true, true);
            }
        }

        private void ClickEvent()
        {
            OnClick?.Invoke();
        }

        private bool GetGraphics()
        {
            if (!_targetGraphics)
                _targetGraphics = GetComponent<MyButtonGraphics>();
            if (!_targetGraphics)
                _graphics = GetComponentsInChildren<Graphic>(true);
            else
                _graphics = _targetGraphics.targetGraphics;
            return _graphics != null && _graphics.Length > 0;
        }
    }
}