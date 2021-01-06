using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameClient.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator LoadSceneTimer(float loadTime)
        {
            yield return new WaitForSeconds(loadTime);
        }

        IEnumerator FadeOutIn(float time)
        {
            yield return FadeOut(3f);
            print("Faded Out");
            yield return FadeIn(1f);
            print("Faded in");
        }


        public IEnumerator FadeOut(float time)
        {
            print("Fading Out black");
            // time = fadeTime;
            while(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        public IEnumerator FadeIn(float time)
        {
            print("Fading in");
            // time = fadeTime;
            while(canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
