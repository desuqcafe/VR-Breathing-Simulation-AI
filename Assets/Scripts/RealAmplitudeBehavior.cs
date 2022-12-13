using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RealAmplitudeBehavior : MonoBehaviour
{

    public ChangeColor realSineWave;


    // The speed at which the sine wave oscillates
    public float speed = 1.0f;

    // The maximum scale of the object
    public float maxScale = 1.0f;

    // The minimum scale of the object
    public float minScale = 0.5f;

    // The previous amplitude of the sine wave
    float previousAmplitude = 0;

    // The previous speed of the sine wave
    float previousSpeed = 0;

    // The transition time for changing the speed
    public float speedTransitionTime = 0.1f;

    // The timer for the speed transition
    float speedTransitionTimer = 0;

    void Update()
    {
        // Check if the speed has changed
        if (speed != previousSpeed)
        {
            // If the speed has changed, start the transition timer
            speedTransitionTimer = speedTransitionTime;
        }

        // Interpolate between the previous speed and the new speed
        // if the transition timer is running
        if (speedTransitionTimer > 0)
        {
            speedTransitionTimer -= Time.deltaTime;
            speed = Mathf.Lerp(previousSpeed, speed, 1 - speedTransitionTimer / speedTransitionTime);
        }

        // Calculate the current amplitude of the sine wave
        float amplitude = GetAmplitude();

        // Normalize the amplitude to be between 0 and 1
        float normalizedAmplitude = (amplitude + 1) / 2;

        // Use the normalized amplitude to set the scale of the object
        transform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, normalizedAmplitude);        // Expand or Contract
    //    transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(minScale, maxScale, normalizedAmplitude), transform.localPosition.z);   // Up or Down

        // Store the current amplitude and speed as the previous values for the next frame
        previousAmplitude = amplitude;
        previousSpeed = speed;
    }

    // This function calculates the amplitude of the sine wave at a given point in time
    float GetAmplitude()
    {
        return Mathf.Sin(Time.time * speed);
    }

// Used for real amplitude value if needed, connected to ChangeColor.cs  
float GetRealAmplitude() {
  // Call the SetColors function and pass a variable to receive the sine wave amplitude value.
  float sineValue = realSineWave.SetColors("1,2,3,4", out float sineWaveAmplitude);

  // Use the sine wave amplitude value as needed.
  return sineWaveAmplitude;
}

}