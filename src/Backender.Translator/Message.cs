namespace Backender.Translator
{
	public class Message
	{
		public string Code { get; set; }
		public MessageType MessageType { get; set; }
		public string Description { get; set; }

		public Message(string Code, MessageType MessageType, string Description)
		{
			this.Code = Code;
			this.MessageType = MessageType;
			this.Description = Description;

		}
	}
	public enum MessageType
	{
		Error,
		Warning,
		Message
	}
}