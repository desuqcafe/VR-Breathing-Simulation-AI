using UnityEngine;
using UnityEngine.UI;
using ChatGPTWrapper;
using TMPro;

public class ChatGPTSubscriber : MonoBehaviour
{
    // Reference to ChatGPTConversation script
    public ChatGPTConversation chatGPTConversation;

    // Reference to input field for chat input
    public TMP_InputField chatInputField;

    // Reference to text component for chat display
    public TextMeshProUGUI chatDisplay;

    // Initial prompt for ChatGPT
    public string initialPrompt = "You are MeditationGPT, you give meditation motivation to the user. You must reply in 20 tokens or less. If you receive a prompt User Gaze Unfocused, then give advice how to focus better. The user is doing breathing meditation technique and watching magic ball expand and contract on queue.";


    private void Awake()
    {
        if (chatGPTConversation == null)
        {
            Debug.LogError("ChatGPTConversation reference is not set.");
            return;
        }

        // Set the initial prompt
        chatGPTConversation.ResetChat(initialPrompt);

        chatGPTConversation.chatGPTResponse.AddListener(OnChatGPTResponse);
    }

    public void SendChatMessage()
    {
        string message = chatInputField.text;
        chatGPTConversation.SendToChatGPT(initialPrompt + " \n" + message);
        // Remove this line if you don't want to display your message
        // chatDisplay.text += "You: " + message + "\n";
        chatInputField.text = "";
    }

    public void SendGazeOffMessage()
    {
        chatGPTConversation.SendToChatGPT(initialPrompt + " \n" + "The user's Gaze Prompt was off");
    }

    public void SendPhaseFinishedMessage(string message)
    {
        chatGPTConversation.SendToChatGPT(initialPrompt + " \n" + message);
    }

    private void OnChatGPTResponse(string response)
    {
        // Only display ChatGPT's reply
        chatDisplay.text = response;
    }
}
