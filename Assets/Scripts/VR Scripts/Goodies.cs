using System.IO;
using UnityEngine;

// this script has also been placed into chatgptconversation though its in packages so seperated.
public class Goodies : MonoBehaviour
{
    public static string api_ElevenLabUrl = "https://api.elevenlabs.io/v1/text-to-speech";
    public static string api_ElevenLabKey;
    public static string id_ElevenLabVoice;
    public static string api_OpenAIKey;

    private void Awake()
    {
        string path = "C:/Users/desuq/luminavault/meditation_vr_app/meditation_access.txt";
        if (!File.Exists(path))
        {
            Debug.LogError("Config file doesn't exist");
            return;
        }

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length < 2)
                    {
                        Debug.LogError("Error in config file: invalid line");
                        continue;
                    }
                    switch (parts[0])
                    {
                        case "API_ELEVENLABKEY":
                            api_ElevenLabKey = parts[1];
                            break;
                        case "ID_ELEVENLABVOICE":
                            id_ElevenLabVoice = parts[1];
                            break;
                        case "API_OPENAIKEY":
                            api_OpenAIKey = parts[1];
                            break;
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Unexpected error: " + e.Message);
        }
    }
}
