using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class MyProgressBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI textField;

        public void Draw(float fillAmount, string text = null)
        {
            gameObject.SetActive(fillAmount > 0);
            fillImage.fillAmount = fillAmount;
            if (textField != null) textField.text = text;
        }

        public void Draw(float startAmount, float endAmount, float duration)
        {
        }
    }
}