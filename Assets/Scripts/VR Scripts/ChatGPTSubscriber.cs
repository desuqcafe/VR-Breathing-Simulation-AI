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
    public string initialPrompt = "You are meditationGPT, you give meditation advice to the user. meditationGPT must reply in 10 words or less. Do not be too over-supportive. The user is participating in a Virtual Reality Boxbox Breathing technique and is watching a ball expand and contract within the environment.";


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
        SendToChatGPT(message);
        chatInputField.text = "";
    }

    public void SendVoiceMessage(string message)
    {
        SendToChatGPT(initialPrompt + " \n" + message);
    }

    private void SendToChatGPT(string message)
    {
        chatGPTConversation.SendToChatGPT(initialPrompt + " \n" + message);
    }

    public void SendGazeOffMessage()
    {
        chatGPTConversation.SendToChatGPT(initialPrompt + " \n" + "The user is not focusing so try to encourage them to focus briefly. Reply in 10 or less tokens.");
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