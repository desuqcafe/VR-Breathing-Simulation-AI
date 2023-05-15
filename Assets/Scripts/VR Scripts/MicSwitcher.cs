using UnityEngine;
using UnityEngine.InputSystem;

public class MicSwitcher : MonoBehaviour
{
    private int currentMicIndex = 0;
    private AudioSource audioSource;

    void Start()
    {
        // Create an AudioSource for the microphone input
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // Loop the audio source

        // Start with the default microphone
        SwitchMicrophone();
    }

    void Update()
    {
        // If 'b' key is pressed
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            // Move to next microphone
            currentMicIndex = (currentMicIndex + 1) % Microphone.devices.Length;
            SwitchMicrophone();
        }
    }

    void SwitchMicrophone()
    {
        // Stop the current microphone
        Microphone.End(null);

        // Start the new microphone
        string device = Microphone.devices[currentMicIndex];
        audioSource.clip = Microphone.Start(device, true, 10, 44100);
        audioSource.Play();

        // Log the new active microphone
        Debug.Log("Switched to microphone: " + device);
    }
}
