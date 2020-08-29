using StackExchange.Redis;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Taisce.Runner
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            //await Task.Factory.StartNew(Action, TaskCreationOptions.LongRunning).Unwrap();
            //await Task.Factory.StartNew(Action).Unwrap();



            //* Array
            //2 Items
            //$3 chars
            //$3 chars
            //"*2\r\n$3\r\nfoo\r\n$3\r\nbar\r\n"

            string host = "127.0.0.1";
            int port = 6379;
            var msg = "*3\r\n$3\r\nset\r\n$4\r\nkarl\r\n$3\r\nwin\r\n";
            var encoding = Encoding.UTF8.GetBytes(msg);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                NoDelay = true,
                SendTimeout = 10
            };

            socket.Connect(host, port);

            if (socket.Connected)
            {
                var number = socket.Send(encoding);
                Console.WriteLine(number);
            }

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            IDatabase db = redis.GetDatabase();
            string value = db.StringGet("karl");
            Console.WriteLine(value); // writes: "

            Console.WriteLine("End of program");
            Console.ReadLine();
        }

        private static async Task Action()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            Console.WriteLine("PRINT");
        }
    }
}
