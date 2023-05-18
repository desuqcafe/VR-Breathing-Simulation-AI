using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using ChatGPTWrapper; // for Goodies

public class GPT4_ElevenLabsManager : MonoBehaviour
{
    public string gpt4ApiKey = Goodies.api_OpenAIKey;
    public string elevenLabsAPIKey = Goodies.api_ElevenLabKey;
    public string elevenLabsVoiceID = Goodies.api_ElevenLabUrl;

    void Start()
    {
        // Example usage
        string userText = "What's the weather like today?";
        ProcessUserInput(userText);
    }

    public async void ProcessUserInput(string userInput)
    {
        string gpt4Response = await GetGPT4Response(userInput);
        SynthesizeAndPlayResponse(gpt4Response);
    }

    private async Task<string> GetGPT4Response(string userInput)
    {
        string prompt = userInput;
        int maxTokens = 50;

        string apiUrl = "https://api.openai.com/v1/engines/davinci-codex/completions";
        string jsonBody = $@"{{
            ""prompt"": ""{prompt}"",
            ""max_tokens"": {maxTokens},
            ""n"": 1,
            ""stop"": """",
            ""temperature"": 1
        }}";

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + gpt4ApiKey);

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseBody = request.downloadHandler.text;
                var json = JsonUtility.FromJson<OpenAIResponse>(responseBody);
                return json.choices[0].text;
            }
            else
            {
                Debug.LogError("GPT-4 API Error: " + request.error);
                return null;
            }
        }
    }

    public void SynthesizeAndPlayResponse(string text)
    {
        ElevenLabsTextToSpeechManager ttsScript = gameObject.GetComponent<ElevenLabsTextToSpeechManager>();
        string json = ttsScript.BuildTTSJSON(text);
        RTDB db = new RTDB();  // create an instance of RTDB
        ttsScript.SpawnTTSRequest(json, OnTTSCompletedCallback, db, elevenLabsAPIKey, elevenLabsVoiceID);
    }

    void OnTTSCompletedCallback(RTDB db, AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("Error getting audio clip");
            return;
        }

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
}

[Serializable]
public class OpenAIResponse
{
    public List<Choice> choices;
}

[Serializable]
public class Choice
{
    public string text;
}