using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TI.Usuarios.Modelos;

namespace TI.Usuarios.BUS
{
    internal static class RescisaoListener
    {
        internal static void StartListening()
        {
            string EXCHANGE_NAME = "rh_rescisao";

            ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(EXCHANGE_NAME, "fanout");
                    string queueName = channel.QueueDeclare();
                    channel.QueueBind(queueName, EXCHANGE_NAME, "");

                    Console.WriteLine("Aguardando notificação de rescisão...");

                    QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, true, consumer);

                    while (true)
                    {
                        BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        string json = Encoding.ASCII.GetString(e.Body);
                        var rescisao = Rescisao.GetOneFromJson(json);
                        rescisao.Save();
                        Console.WriteLine("Rescisão notificada: {0} - {1}", rescisao.Id, rescisao.ColaboradorNome);
                    }
                }
            }
        }
    }
}
