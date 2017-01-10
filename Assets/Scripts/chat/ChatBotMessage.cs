using System;

[Serializable]
public class ChatBotMessage
{
	public ChatBotMessage(string body)
	{
		this.body = body;
	}

	public string body;
}
