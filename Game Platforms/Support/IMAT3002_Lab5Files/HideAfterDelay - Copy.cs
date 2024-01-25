using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HideAfterDelay : MonoBehaviour
{
    public float delayInSeconds = 5f;
    public float fadeRate = 0.25f;

    private CanvasGroup canvaGroup;
    private float startTimer;
    private float fadoutTimer;

    void OnEnable()
    {
        canvaGroup = GetComponent<CanvasGroup>();
        canvaGroup.alpha = 1f;

        startTimer = Time.time + delayInSeconds;
        fadoutTimer = fadeRate;
    }

    void Update()
    {
        // time to fade out?
        if (Time.time >= startTimer)
        {
            fadoutTimer -= Time.deltaTime;

            // fade out complete?
            if (fadoutTimer <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                // reduce the alpha value
                canvaGroup.alpha = fadoutTimer / fadeRate;
            }

            //canvaGroup.
            //    DOFade(0f, fadeRate).
            //    OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}
