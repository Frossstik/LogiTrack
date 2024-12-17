using LogiTrack.ActiveRecord;
using LogiTrack.DomainModel;
using LogiTrack.Repository;
using LogiTrack.TransactionSript;

class Program
{
    static void Main(string[] args)
    {
        var orderRepository = new TransportationOrderRepository();
        var driverRepository = new DriverRepository();

        var lifecycleService = new OrderLifecycleService(orderRepository);
        var fraudDetectionService = new FraudDetectionService();
        var scheduleTransactionScript = new ScheduleTransactionScript(driverRepository);

        var route = new DeliveryRoute("Москва", "Санкт-Петербург");
        var order = new TransportationOrder(route);

        orderRepository.Add(order);
        Console.WriteLine($"Создан новый заказ: ID={order.Id}, Статус={order.Status}, Маршрут: {route.StartPoint} -> {route.EndPoint}");

        if (fraudDetectionService.DetectFraud(order))
        {
            Console.WriteLine("Заказ отклонен: подозрение на мошенничество.");
        }
        else
        {
            Console.WriteLine("Заказ прошел проверку на мошенничество.");
        }

        var attachments = new List<Attachment> { new Attachment("/path/to/file1.pdf"), new Attachment("/path/to/file2.jpg") };
        order.AddMessage("Груз готов к отправке", attachments);
        Console.WriteLine("Добавлено сообщение к заказу: \"Груз готов к отправке\"");

        lifecycleService.CloseInactiveOrders(TimeSpan.FromDays(30));
        Console.WriteLine($"Проверка заказов на неактивность завершена. Текущий статус заказа: {order.Status}");

        var driver = new Driver(Guid.NewGuid(), "Иван Иванов");
        driverRepository.Save(driver);
        Console.WriteLine($"Добавлен новый водитель: {driver.Name} (ID={driver.Id})");

        var shift = new Shift(DateTime.Now.AddHours(1), DateTime.Now.AddHours(9));
        try
        {
            scheduleTransactionScript.AddShift(driver.Id, shift);
            Console.WriteLine($"Смена успешно добавлена водителю: {driver.Name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка назначения смены: {ex.Message}");
        }

        var isAvailable = scheduleTransactionScript.IsDriverAvailable(driver.Id, DateTime.Now.AddHours(2));
        Console.WriteLine($"Водитель доступен для назначения: {isAvailable}");



        Console.WriteLine("\n--- Новый кейс ---");
        Driver driver2 = new Driver(Guid.NewGuid(), "Петр Петров");
        Console.WriteLine($"Добавлен новый водитель: {driver2.Name} (ID={driver2.Id})");

        bool isAvailable2 = driver2.IsAvailable(DateTime.Now);
        Console.WriteLine($"Водитель доступен для назначения: {isAvailable2}");
    }
}


