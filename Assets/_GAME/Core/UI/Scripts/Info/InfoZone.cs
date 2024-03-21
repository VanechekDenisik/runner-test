using System;
using Core.Common.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI.Info
{
    [RequireComponent(typeof(EntityReference))]
    public class InfoZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private InfoViewParameter parameter;

        [InjectFromEntity] private EntityReference _entityReference;

        private InfoComponent _info;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _info = _entityReference.Entity.GetComponent<InfoComponent>();
            UpdateInfo();

            _info.OnUpdateInfo += UpdateInfo;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideInfo();
        }

        private void OnDisable()
        {
            HideInfo();
        }

        private void HideInfo()
        {
            parameter.Value.Hide();
            if (_info != null)
                _info.OnUpdateInfo -= UpdateInfo;
        }

        private void UpdateInfo()
        {
            parameter.Value.Show(_info.Header, _info.Descriptions);
        }
    }
}