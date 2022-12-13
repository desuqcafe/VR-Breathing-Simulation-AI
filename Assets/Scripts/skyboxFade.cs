using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class skyboxFade : MonoBehaviour
{
    public Material skybox1;
    public Material skybox2;
    public float transitionDuration = 5.0f;

    private float transitionStartTime;
    private bool isTransitioning = false;

    void Update()
    {
        if ((Keyboard.current.sKey.wasPressedThisFrame) && !isTransitioning)
        {
            transitionStartTime = Time.time;
            isTransitioning = true;
        }

        if (isTransitioning)
        {
            float timeSinceTransitionStarted = Time.time - transitionStartTime;
            float percentageComplete = timeSinceTransitionStarted / transitionDuration;

            if (percentageComplete >= 1.0f)
            {
                isTransitioning = false;
                RenderSettings.skybox = skybox2;
            }
            else
            {
                float interpolation = Mathf.Clamp01(percentageComplete);
                RenderSettings.skybox.Lerp(skybox1, skybox2, interpolation);
            }
        }
    }
}