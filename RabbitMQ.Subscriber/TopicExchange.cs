using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Subscriber
{
    public class TopicExchange : BaseExchange
    {
        private const string exchangeName = "logs-topic";

        public static void Consumer()
        {
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();


                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                var queueName = channel.QueueDeclare().QueueName;

                var routeKey = "*.Error.*";
                //var routeKey = "*.*.Warning"; //examples
                //var routeKey = "#.Info";

                channel.QueueBind(queueName, exchangeName, routeKey);

                channel.BasicConsume(queueName, false, consumer);


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
