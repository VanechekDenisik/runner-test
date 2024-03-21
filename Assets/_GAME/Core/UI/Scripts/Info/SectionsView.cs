using TMPro;
using UnityEngine;

namespace Core.UI.Info
{
    public class SectionsView : MonoBehaviour
    {
        [SerializeField] private GameObject pointObj;
        [SerializeField] private TextMeshProUGUI text;

        public void Draw(string description)
        {
            text.text = string.IsNullOrEmpty(description) ? "error" : description;
        }
    }
}