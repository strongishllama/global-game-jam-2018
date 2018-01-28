using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public enum eState
    {
        FadeIn,
        FadeOut
    }

    [SerializeField]
    private float fadeSpeed;

    [SerializeField]
    private Image GGJImage;
    [SerializeField]
    private Image gameNameImage;
    [SerializeField]
    private Image fadeImage;

    private eState state = eState.FadeIn;

    private void Update()
    {
        if (state == eState.FadeIn)
        {
            Color fadeColor = fadeImage.color;
            fadeColor.a -= fadeSpeed * Time.deltaTime;
            fadeImage.color = fadeColor;

            if (fadeColor.a >= 0.9f)
            {
                state =  eState.FadeOut;
            }
        }
        else if (state == eState.FadeOut)
        {
            Color fadeColor = fadeImage.color;
            fadeColor.a += fadeSpeed * Time.deltaTime;
            fadeImage.color = fadeColor;

            if (fadeColor.a <= 0.1f)
            {
                state = eState.FadeIn;
            }
        }
    }
}
