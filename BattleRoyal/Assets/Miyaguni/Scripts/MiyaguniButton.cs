using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    // Button that's meant to work with mouse or touch-based devices.
    [AddComponentMenu("UI/Button", 30)]
    public class MiyaguniButton : Selectable, IPointerClickHandler, IPointerDownHandler, ISubmitHandler
    {
        [Serializable]
        public class ButtonClickedEvent : UnityEvent { }

        // Event delegates triggered on click.
        [FormerlySerializedAs("onClick")]
        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

        // Event delegates triggered on down.
        [FormerlySerializedAs("onDown")]
        [SerializeField]
        private ButtonClickedEvent m_OnDown = new ButtonClickedEvent();

        protected MiyaguniButton()
        { }

        public ButtonClickedEvent onClick {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }

        public ButtonClickedEvent onDown {
            get { return m_OnDown; }
            set { m_OnDown = value; }
        }

        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
        }

        private void Down()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("Button.onDown", this);
            m_OnDown.Invoke();
        }

        // Trigger all registered callbacks.
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
            //base.OnDeselect(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            //if (eventData.button != PointerEventData.InputButton.Left)
            //    return;

            ////// Selection tracking
            ////if (IsInteractable() && navigation.mode != Navigation.Mode.None && EventSystem.current != null)
            ////    EventSystem.current.SetSelectedGameObject(gameObject, eventData);

            ////isPointerDown = true;
            //EvaluateAndTransitionToSelectionState(eventData);
            Down();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            Press();

            // if we get set disabled during the press
            // don't run the coroutine.
            if (!IsActive() || !IsInteractable())
                return;

            DoStateTransition(SelectionState.Pressed, false);
            StartCoroutine(OnFinishSubmit());
        }

        private IEnumerator OnFinishSubmit()
        {
            var fadeTime = colors.fadeDuration;
            var elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            DoStateTransition(currentSelectionState, false);
        }
    }
}