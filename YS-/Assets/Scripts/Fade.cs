using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace vanilla
{
    public class Fade : MonoBehaviour
    {
        public Image blueImage; // 파란색 이미지
        public Image redImage;  // 빨간색 이미지

        public float fadeDuration = 1.0f; // 페이드 인/아웃 지속 시간

        public IEnumerator FadeImage(Image image, float targetAlpha)
        {
            image.gameObject.SetActive(true);
            Color startColor = image.color;
            startColor.a = 0.3f;

            float startTime = Time.time;
            float elapsedTime = 0;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime = Time.time - startTime;
                float percentageComplete = Mathf.Clamp01(elapsedTime / fadeDuration);

                Color newColor = image.color;
                newColor.a = Mathf.Lerp(startColor.a, targetAlpha, percentageComplete);
                image.color = newColor;

                yield return null;
            }
             image.gameObject.SetActive(false);
        }
    }
}
