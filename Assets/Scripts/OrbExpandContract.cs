using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class OrbExpandContract : MonoBehaviour
{

    public VisualEffect vfx;
    public BreathingExercise breath_script;

    int countMe = 1;

    // Start is called before the first frame update
    void Start()
    {
        //get a reference to the Visual Effect Asset attached to the GameObject
        vfx = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {

     switch (breath_script.currentPhase)
        {
            case BreathPhase.INHALE:
             if (countMe == 1) {
             SetSize(3.8f);
             countMe++;
            }
             break;
            case BreathPhase.HOLD:
             if (countMe == 2) {
             SetNewColorHold();
             countMe++;
             }
             break;
            case BreathPhase.EXHALE:
             if (countMe == 3) {
             SetBaseColorHold();
             SetSize(3.8f);
             countMe = (countMe - 2);
             }
             break;
            case BreathPhase.REST:
             SetSize(0.3f);
             break;
        }
    }

    public void SetNewColorHold()
    {
     //   vfx.SetFloat("size", 0.2f * Mathf.PingPong(Time.time * 0.5f, 0.5f) + 0.3f);
         // Create a new Color object with the specified red, green, blue, and alpha values
    Color customColor = new Color(0.3275424f, 1.467875f, 0.9219711f, 0.0f);

    // Set the "color" parameter to the custom color
    vfx.SetVector4("color", customColor);

    }

    public void SetBaseColorHold()
    {
     //   vfx.SetFloat("size", 0.2f * Mathf.PingPong(Time.time * 0.5f, 0.5f) + 0.3f);
         // Create a new Color object with the specified red, green, blue, and alpha values
    Color customColor = new Color(0.6065599f, 1.273776f, 2.317059f, 0.0f);

    // Set the "color" parameter to the custom color
    vfx.SetVector4("color", customColor);

    }

    public void SetSize(float duration)
    {
    // Define an animation curve that interpolates the size value over time
    AnimationCurve sizeCurve = new AnimationCurve();
    sizeCurve.AddKey(0, vfx.GetFloat("size")); // start at the current size

        switch (breath_script.currentPhase)
        {
            case BreathPhase.INHALE:
             sizeCurve.AddKey(duration, 2.8f);
             break;
            case BreathPhase.EXHALE:
             sizeCurve.AddKey(duration, 0.7f);
             break;
            case BreathPhase.REST:
             sizeCurve.AddKey(duration, 1.5f); // end at 0.8f after the given duration
             break;
        }


    // Use a coroutine to update the size value over time
    StartCoroutine(UpdateSizeOverTime(duration, sizeCurve));   
    }

    private IEnumerator UpdateSizeOverTime(float duration, AnimationCurve sizeCurve)
{
    float elapsedTime = 0;
    while (elapsedTime < duration)
    {
        // Interpolate the size value using the animation curve
        float size = sizeCurve.Evaluate(elapsedTime);

        // Set the size value on the vfx object
        vfx.SetFloat("size", size);

        // Wait for the next frame before continuing the loop
        yield return null;

        // Increase the elapsed time
        elapsedTime += Time.deltaTime;
    }

    // Set the final size value to ensure that it is exactly 0.8f
    // vfx.SetFloat("size", 0.8f);
}
}
