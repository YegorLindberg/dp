using System;
using StackExchange.Redis;

namespace TextListener
{
    class Program
    {
        private const string MessagesChannelName = "events";
        private const string RedisConnectionURL = "localhost:6379";

        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnectionURL);
            ISubscriber sub = redis.GetSubscriber();

            sub.Subscribe(MessagesChannelName, (channel, id) => {
                string identifier = id.ToString();
                if (identifier == null) {
                    Console.WriteLine("Registred wrong identifier");
                    return;
                }
                string value = redis.GetDatabase().StringGet(identifier).ToString() ?? "Nil";

                Console.WriteLine("Text listener has registred data");
                Console.WriteLine("Id: " + identifier + "\nValue: " + value);
            });

            Console.WriteLine("Application started. Press Ctrl+C to shut down.(TextListener)");
            while (true) {

            }
        }
    }
}
