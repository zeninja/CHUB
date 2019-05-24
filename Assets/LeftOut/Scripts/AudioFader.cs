using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFader : MonoBehaviour
{

    public AudioSource lastSource;

    void OnEnable()
    {
        MetaSlider.OnActiveSliderChanged += FadeOutAudio;
    }

    void OnDisable()
    {
        MetaSlider.OnActiveSliderChanged -= FadeOutAudio;
    }

    public void SetLastSource(AudioSource l)
    {
        lastSource = l;
    }

    void FadeOutAudio()
    {
        if(MetaSlider.GetInstance().stageInfo.world == 1 && MetaSlider.GetInstance().stageInfo.level == 1) { return; }

        if (lastSource != null)
        {
            StartCoroutine(FadeLastSource(lastSource));
        }
    }

    IEnumerator FadeLastSource(AudioSource sourceToFade)
    {
        // Debug.Log("FADING PREVIOUS SOURCE");

        float t = 0;
        float d = 1;

        // Debug.Log(sourceToFade.clip);

        if (sourceToFade != null)
        {
            while (t < d)
            {
                t += Time.fixedDeltaTime;
                float p = t / d;
                sourceToFade.volume = 1 - EZEasings.SmoothStop3(p);

                // Debug.Log("ADJUSTING " + sourceToFade.volume);

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
