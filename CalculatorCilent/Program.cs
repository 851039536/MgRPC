using Calculator;
using Grpc.Core;
using System;
using System.Threading.Tasks;

class Program
{
    const string ServerAddress = "127.0.0.1";
    const int Port = 50051;
    static void Main(string[] args)
    {
        //MyMethod();
        ServerStreamingMethod();
        SelfIncreaseClient();
        BidirectionalStreamingMethod();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    /// <summary>
    /// 发布响应
    /// </summary>
    private static void MyMethod()
    {
        Channel channel = new Channel($"{ServerAddress}:{Port}",ChannelCredentials.Insecure);
        var client = new CalculatorService.CalculatorServiceClient(channel);

        MyRequest myRequest = new MyRequest { Num = { 1,2,3,4 } };
        MyResponse myResponse = client.MyMethod(myRequest);
        Console.WriteLine($"Result: {myResponse.Strs}");
        channel.ShutdownAsync().Wait();
    }

    /// <summary>
    /// 服务器流（Server Streaming）
    /// </summary>
    private static void ServerStreamingMethod()
    {
        Channel channel = new Channel($"{ServerAddress}:{Port}",ChannelCredentials.Insecure);
        var client = new CalculatorService.CalculatorServiceClient(channel);
        Request1 request1 = new Request1 { Message = "ceshi" };
        AsyncServerStreamingCall<Response1> response1s = client.ServerStreamingMethod(request1);
        while (response1s.ResponseStream.MoveNext().Result)
        {
            Console.WriteLine(response1s.ResponseStream.Current.Message);
        }
        channel.ShutdownAsync().Wait();
    }

    /// <summary>
    /// 客户端流（Client Streaming）
    /// </summary>
    private static async void SelfIncreaseClient()
    {
        Channel channel = new Channel($"{ServerAddress}:{Port}",ChannelCredentials.Insecure);
        var client = new CalculatorService.CalculatorServiceClient(channel);
        var call = client.ClientStreamingMethod();
        for (int i = 0 ; i < 10 ; i++)
        {
            await call.RequestStream.WriteAsync(new Request2() { Message = $"第{i}个" });
            await Task.Delay(500);
        }
        await call.RequestStream.CompleteAsync();
        Console.WriteLine($"Result: {call.ResponseAsync.Result.Message}");
        channel.ShutdownAsync().Wait();
    }
    /// <summary>
    /// 双向流（Bidirectional Streaming）
    /// </summary>
    private static async void BidirectionalStreamingMethod()
    {
        Channel channel = new Channel($"{ServerAddress}:{Port}",ChannelCredentials.Insecure);
        var client = new CalculatorService.CalculatorServiceClient(channel);
        var call = client.BidirectionalStreamingMethod();
        for (int i = 1 ; i <= 5 ; i++)
        {
            // 发送请求
            await call.RequestStream.WriteAsync(new Request3 { Message = i.ToString() });
            await Task.Delay(500);
        }
        await call.RequestStream.CompleteAsync();

        while (call.ResponseStream.MoveNext().Result)
        {
            // 处理服务器发送的响应
            Console.WriteLine(call.ResponseStream.Current.Message);
        }
        channel.ShutdownAsync().Wait();
    }
}
