using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NorthwindAPI.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private const string QueueName = "my_queue";

        [HttpPost]
        public IActionResult ReceiveMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    // Ejemplo: Imprimir el mensaje en la consola
                    Console.WriteLine("Mensaje recibido: " + message);

                    channel.BasicAck(ea.DeliveryTag, false);
                };

                channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);
            }

            return Ok("Mensaje recibido correctamente");
        }
    }
}
