using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Subscriber;
using System.Text;

Console.WriteLine("Subscriber running");

#region Basic Usage
/*
var factory = new ConnectionFactory
{
    HostName = "localhost",
    Port = 5672
};

using (var connection = factory.CreateConnection())
{
    var channel = connection.CreateModel();
    channel.BasicQos(0, 1, false);// false: her bir subscribere 1 tane, true: tüm subscriberlara toplam 1 tane mesaj gitsin

    var consumer = new EventingBasicConsumer(channel);

    channel.BasicConsume("hello-queue", false, consumer);// true : mesaj alıcısına iletildiği zaman ilgili mesaj rabbitmq'dan otamatik silinir, false: mesajı manuel olarak sildiğimiz senaryo(best practise), çünkü belki de mesajı işlerken hata alacağız işlem yarım kalacak ama rabbitmq tarafından da silinmemiş olmalı

    consumer.Received += (object sender, BasicDeliverEventArgs e) =>
    {
        var message = Encoding.UTF8.GetString(e.Body.ToArray());
        Thread.Sleep(500);
        Console.WriteLine($"Recieved Message : {message}");

        channel.BasicAck(e.DeliveryTag, false);
    };
    Console.ReadLine();
}//birden fazla RabbitMQ.Subscriber.exe çalıştırırsak kuyruktan hangi mesaj gelirse ekrana basacaktır. 
*/

#endregion


#region Fanout Exchange Usage

//FanoutExchange.Consumer();

#endregion


#region Direct Exchange Usage

//DirectExchange.Consumer();

#endregion


#region Topic Exchange Usage

//TopicExchange.Consumer();

#endregion


#region Header Exchange Usage

HeaderExchange.Consumer();

#endregion

Console.ReadLine();

