
using RabbitMQ.Client;
using RabbitMQ.Publisher;
using System.Text;

Console.WriteLine("Publisher running");

#region Basic Usage
//verinin publisherdan direkt kuyruğa gönderilmesi örneği
/*

var factory = new ConnectionFactory
{
    HostName = "localhost",
    Port = 5672
};

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();
    channel.QueueDeclare("hello-queue", true, false, false); //yoksa yeniden oluşturur varsa bir işlem yapmaz

    Enumerable.Range(1, 50).ToList().ForEach(x =>
    {
        string message = $"Hello World {x}";
        var messageBody = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

        Console.WriteLine($"Message sended : {x}");
    });


}//channel ve connection kapanıyor
*/
#endregion


#region Fanout Exchange Usage

//FanoutExchange.Publisher();

#endregion


#region Direct Exchange Usage

//DirectExchange.Publisher();

#endregion


#region Topic Exchange Usage

//TopicExchange.Publisher();

#endregion


#region Header Exchange Usage

HeaderExchange.Publisher();

#endregion

Console.ReadLine();


