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

            foreach (var device in Microphone.devices)
            {
                Debug.Log("Name: " + device);
            }
            if (Microphone.devices.Length > 0)
            {
                //StartRecording();
            }
            else
            {
                Debug.LogError("No microphone found!");
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

                if (!Keyboard.current.nKey.wasPressedThisFrame || time >= duration)
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