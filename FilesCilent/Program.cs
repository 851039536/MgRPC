using FileService;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        await test();
    }
    public async static Task test()
    {
        try
        {

            //10.55.11.6 
            //127.0.0.1
            string ip = "127.0.0.1:50051";
            Channel channel = new Channel(ip,ChannelCredentials.Insecure);
            var client = new FileService.FileService.FileServiceClient(channel);

            //using (var call = client.SendFileContent(new FileRequest { Filename = "example.txt" }))
            //指定获取哪个文件Filename
            using (var call = client.SendFileContent(new FileRequest { Filename = "LoginetMediaLogger.log" }))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    FileResponse response = call.ResponseStream.Current;
                    // 或者将字节转换为适当的格式，如保存到文件等。  
                    Console.Write(response.Content.ToStringUtf8());
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Thread.Sleep(1000);
        }
    }
}