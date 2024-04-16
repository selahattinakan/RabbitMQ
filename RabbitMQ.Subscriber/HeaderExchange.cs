using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Subscriber
{
    public class HeaderExchange : BaseExchange
    {
        private const string exchangeName = "header-exchange";

        public static void Consumer()
        {
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();


                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                var queueName = channel.QueueDeclare().QueueName;

                Dictionary<string, object> headers = new Dictionary<string, object>();

                headers.Add("format", "pdf");//publisher ile aynı olması lazım
                headers.Add("shape", "a4");//publisher ile aynı olması lazım
                headers.Add("x-match", "all");

                channel.QueueBind(queueName, exchangeName, string.Empty, headers);

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
