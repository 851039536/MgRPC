using GrpcLink;
using System;
using System.Threading;

namespace TestServer
{
    /// <summary>
    /// 测试服务端
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkFunc.LinkServerStart("127.0.0.1",9008);
            Thread.Sleep(500);
            LinkFunc.ReplyMes = ReplyMes;
            Console.ReadKey();
           

        }


        /// <summary>
        /// 接收到客户端信息后回复
        /// </summary>
        /// <param name="strRequest">客户端发送过来的内容</param>
        /// <returns></returns>
        public static string ReplyMes(string strRequest)
        {
            Console.WriteLine("接收到:" + strRequest);
            switch (strRequest)
            {
                case "1":
                return "Server识别到1";
                case "2":
                return "Server识别到2";
                case "测试":
                return "开始测试"; 
                case "连接服务端":
                return "true";
            }
            return "Server未识别到指定参数";
        }
    }
}
