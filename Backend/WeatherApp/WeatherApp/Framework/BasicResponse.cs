using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;

namespace WeatherApp.Framework
{
    public class BasicResponse
    {
        private Collection<Message> _messages;

        public Collection<Message> Messages => _messages ?? (_messages = new Collection<Message>());
        public int StatusCode = 0;

        public bool IsError()
        {
            return Messages.Count(item => item.Type == CoreEnum.MessageType.Error) > 0;
        }

        public string[] GetMessageErrorTextArray()
        {
            return Messages.Where(item => item.Type == CoreEnum.MessageType.Error)
                .Select(item => item.MessageText)
                .ToArray();
        }

        public int GetErrorStatusCode()
        {
            return StatusCode;
        }

        public string GetErrorMessage()
        {
            var messageBuilder = new StringBuilder();
            foreach (var message in Messages)
                messageBuilder.AppendLine(message.MessageText);

            return messageBuilder.ToString().Trim();
        }

        public void AddErrorMessage(string errorMessage, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            Messages.Add(new Message
            {
                MessageText = errorMessage,
                Type = CoreEnum.MessageType.Error
            });

            StatusCode = statusCode;
        }

    }
}
