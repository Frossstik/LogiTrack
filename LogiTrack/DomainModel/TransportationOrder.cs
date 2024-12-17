using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.DomainModel
{
    public class TransportationOrder
    {
        public Guid Id { get; private set; }
        public OrderStatus Status { get; private set; }
        public List<Message> Messages { get; private set; } = new();
        public DeliveryRoute Route { get; private set; }

        public TransportationOrder(DeliveryRoute route)
        {
            Id = Guid.NewGuid();
            Status = OrderStatus.Active;
            Route = route;
        }

        public void AddMessage(string content, List<Attachment> attachments)
        {
            if (Status != OrderStatus.Active)
                throw new InvalidOperationException("Сообщения можно добавлять только к активным заказам.");

            var message = new Message(content);
            message.AddAttachments(attachments);
            Messages.Add(message);
        }

        public void CloseOrder()
        {
            if (Status == OrderStatus.Closed)
                throw new InvalidOperationException("Заказ закрыт.");

            Status = OrderStatus.Closed;
        }

        public bool IsInactive(TimeSpan inactivityThreshold)
        {
            return Messages.Count == 0 ||
                   Messages.Max(m => m.Timestamp) < DateTime.UtcNow - inactivityThreshold;
        }
    }


}
