using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Publisher
{
    public class HeaderExchange : BaseExchange
    {
        private const string exchangeName = "header-exchange";

        public static void Publisher()
        {
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Headers);

                Dictionary<string,object> headers = new Dictionary<string, object>();

                headers.Add("format", "pdf");
                headers.Add("shape", "a4");

                var properties = channel.CreateBasicProperties();
                properties.Headers = headers;
                //properties.Persistent = true; //mesajların kalıcı olmasını sağlar, rabbitmq restart olsa bile mesajlar diske yazıldığı için kaybolmaz
                //mesajların kalıcı olması için sadece durable=true yeterli değildir.

                string message = "This is a header message";
                var messageBody = Encoding.UTF8.GetBytes(message);

                Console.WriteLine("Header message sended");

                channel.BasicPublish(exchangeName, string.Empty, properties, messageBody);

            }
        }
    }
}
