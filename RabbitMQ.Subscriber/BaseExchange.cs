using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Subscriber
{
    public class BaseExchange
    {
        public static ConnectionFactory factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672
        };
    }
}
