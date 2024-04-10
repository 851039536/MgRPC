using Calculator;
using CalculatorServer;
using Grpc.Core;
using System;


class Program
{
    static void Main(string[] args)
    {

        //配置服务
        const int Port = 50051;
        Server server = new Server
        {
            Services = { CalculatorService.BindService(new CalculatorServiceImpl()) },
            Ports = { new ServerPort("127.0.0.1",Port,ServerCredentials.Insecure) }
        };
        server.Start();

        Console.WriteLine("服务器端口监听: " + Port);
        Console.WriteLine("任意键停止服务...");
        Console.ReadKey();

        server.ShutdownAsync().Wait();
    }
}
