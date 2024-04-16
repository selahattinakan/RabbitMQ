using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Subscriber
{
    public class FanoutExchange : BaseExchange
    {
        private const string exchangeName = "logs-fanout";

        public static void Consumer()
        {
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                var randomQueueName = channel.QueueDeclare().QueueName;// kalıcı olmayan kuyruk senaryosunda

                //var queueName = "logs-database-save-name"; // kalıcı olan kuyruk senaryosunda
                //channel.QueueDeclare(queueName, true, false, false);

                channel.QueueBind(randomQueueName, exchangeName, string.Empty, null);

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume(randomQueueName, false, consumer);// kalıcı olmayan kuyruk senaryosunda

                //channel.BasicConsume(queueName, false, consumer);// kalıcı olan kuyruk senaryosunda

                Console.WriteLine($"Logs listening");

                consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                    Thread.Sleep(500);
                    Console.WriteLine($"Recieved Message : {message}");

                    channel.BasicAck(e.DeliveryTag, false);
                };
                Console.ReadLine();
            }
        }
    }
}
