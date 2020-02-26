using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TinyErpDesktopClient.ViewModel.Commands;

namespace TinyErpDesktopClient.ViewModel
{
    public class MainWindowModel : BaseViewModel
    {
        public ICommand ButtonClicked { get; }

        private string _text;
        public string Text
        {
            get { return _text; }
            private set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public MainWindowModel()
        {
            ButtonClicked = new DelegateCommand(OnButtonClicked);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);

        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);

            Text += $"Message received: {message}. {Environment.NewLine}";
        }

        private void OnButtonClicked(object obj)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                }
            }
        }
    }
}
