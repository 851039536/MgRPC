using GrpcLink;
using System;
using System.Threading;

namespace TestCilent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            LinkFunc.LinkClientStart("127.0.0.1:9008");
            Console.WriteLine("连接服务端中");
            string conn = LinkFunc.SendMes("连接服务端");

            if (conn.Equals("true"))
            {
                Console.WriteLine("连接服务端成功!");


                for (int i = 0 ; i < 10 ; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("输入测试内容..");
                    try
                    {
                        var line = Console.ReadLine();
                        var ret = LinkFunc.SendMes(line);
                        //获取到服务端返回的值
                        Console.WriteLine(ret);
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            Console.WriteLine("连接失败 , 即将退出");
            Thread.Sleep(2000);
        }
    }
}
