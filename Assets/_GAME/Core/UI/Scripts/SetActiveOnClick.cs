using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class SetActiveOnClick : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private GameObject[] activateOnIsActive;
        [SerializeField] private GameObject[] activateOnIsInactive;
        [SerializeField] private bool isActiveAtStart;

        private bool _isActive;

        private void Awake()
        {
            btn.onClick.AddListener(OnClick);
            _isActive = isActiveAtStart;
            SetActiveObjects();
        }

        private void OnClick()
        {
            _isActive = !_isActive;
            SetActiveObjects();
        }

        private void SetActiveObjects()
        {
            foreach (var obj in activateOnIsActive) obj.SetActive(_isActive);
            foreach (var obj in activateOnIsInactive) obj.SetActive(!_isActive);
        }
    }
}