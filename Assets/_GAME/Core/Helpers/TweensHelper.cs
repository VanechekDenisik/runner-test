using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Helpers
{
    public static class TweensHelper
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }

        public static void PunchScale(this Transform transform)
        {
            transform.DOKill();
            transform.transform.localScale = Vector3.one;
            transform.transform.DOPunchScale(0.7f * Vector3.one, 0.3f, 1);
        }

        public static Tween FadeIn(this Image image, float duration = 0.3f, bool enable = true)
        {
            if (enable)
                image.gameObject.SetActive(true);
            image.SetAlpha(0);
            return image.DOFade(1, duration);
        }

        public static Tween FadeOut(this Image image, float duration = 0.3f, bool disable = true)
        {
            image.SetAlpha(1);
            return image.DOFade(0, duration).OnComplete(() =>
            {
                if (disable)
                    image.gameObject.SetActive(false);
            });
        }

        public static Tween FadeInAndOut(this Image image, float durationIn = 0.3f, float durationOut = 0.3f,
            bool enable = true)
        {
            if (enable)
                image.gameObject.SetActive(true);
            image.SetAlpha(0);
            return image.DOFade(1, durationIn).OnComplete
            (
                () => image.DOFade(0, durationOut).OnComplete(() =>
                {
                    if (enable)
                        image.gameObject.SetActive(false);
                })
            );
        }

        public static Tween Counter(this TextMeshProUGUI text, int from, int to, float duration, string format = "{0}")
        {
            var num = from;
            return DOTween.To(() => num, x => text.text = string.Format(format, x), to, duration)
                .SetEase(Ease.OutSine);
        }

        public static Tween ScaleOut(this Transform transform, float duration = 0.3f, bool disable = true)
        {
            transform.DOKill();
            transform.gameObject.SetActive(true);
            transform.localScale = Vector3.one;
            return transform.DOScale(0, duration).OnComplete(() =>
            {
                if (disable)
                    transform.gameObject.SetActive(false);
            });
        }
    }
}