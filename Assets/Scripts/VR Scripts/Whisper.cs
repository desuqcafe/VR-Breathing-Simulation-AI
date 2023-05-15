using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OpenAI
{
    public class Whisper : MonoBehaviour
    {
        [SerializeField] private ChatGPTSubscriber chatGPTSubscriber;
        
        private readonly string fileName = "output.wav";
        private readonly int duration = 5;
        
        private AudioClip clip;
        private bool isRecording;
        private float time;
        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            // Get the list of available microphones
            string[] mics = Microphone.devices;

            // Check if there are any microphones
            if (mics.Length > 0)
            {
                // Log the name of the default microphone
                Debug.Log("Current active microphone: " + mics[0]);
            }
            else
            {
                // Log a warning if no microphones were found
                Debug.LogWarning("No microphones found!");
            }
        }

        private void Update()
        {
            if (Keyboard.current.mKey.wasPressedThisFrame && !isRecording)
            {
                Debug.Log("M key was pressed. Starting transcription...");
                StartRecording();
            }

            if (isRecording)
            {
                time += Time.deltaTime;

                if (Keyboard.current.nKey.wasPressedThisFrame || time >= duration)
                {
                    time = 0;
                    isRecording = false;
                    EndRecording();
                }
            }
        }

        private void StartRecording()
        {
            isRecording = true;
            clip = Microphone.Start(Microphone.devices[0], false, duration, 44100);
        }

        private async void EndRecording()
        {
            Debug.Log("RecordingEnded");
            
            Microphone.End(null);
            byte[] data = SaveWav.Save(fileName, clip);
            
            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() {Data = data, Name = "audio.wav"},
                Model = "whisper-1",
                Language = "en"
            };
            var res = await openai.CreateAudioTranscription(req);

            Debug.Log("Transcription result: " + res.Text);

            // Pass the transcribed message to ChatGPT
            chatGPTSubscriber.SendVoiceMessage(res.Text);
        }
    }
}