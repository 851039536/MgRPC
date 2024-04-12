using Grpc.Core;
using System;

namespace FileServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int Port = 50051; // 你可以选择一个未被使用的端口号  

            Server server = new Server
            {
                //10.55.11.6
                //127.0.0.1
                Services = { FileService.FileService.BindService(new FileServiceImpl()) },
                Ports = { new ServerPort("127.0.0.1",Port,ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("FileService server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    }
}
