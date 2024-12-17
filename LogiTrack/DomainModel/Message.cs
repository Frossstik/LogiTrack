using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.DomainModel
{
    public class Message
    {
        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public List<Attachment> Attachments { get; private set; } = new();
        public DateTime Timestamp { get; private set; }

        public Message(string content)
        {
            Id = Guid.NewGuid();
            Content = content;
            Timestamp = DateTime.UtcNow;
        }

        public void AddAttachments(List<Attachment> attachments)
        {
            Attachments.AddRange(attachments);
        }
    }
}
