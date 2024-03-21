using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class SetActiveOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject[] activateOnMouseOver;
        [SerializeField] private GameObject[] deactivateOnMouseOver;

        private void Awake()
        {
            SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetActive(false);
        }
        
        private void SetActive(bool isActive)
        {
            foreach (var obj in activateOnMouseOver)
                obj.SetActive(isActive);
            foreach (var obj in deactivateOnMouseOver)
                obj.SetActive(!isActive);
        }
    }
}