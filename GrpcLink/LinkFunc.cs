using Grpc.Core;
using LinkService;
using System;
using static LinkService.Link;

namespace GrpcLink
{
    public class LinkFunc
    {
        /// <summary>
        /// 用于服务端回复委托
        /// </summary>
        public static Func<string,string> ReplyMes;

        // 定义服务端和客户端
        public static Server LinkServer;
        public static LinkClient LinkClient;

        /// <summary>
        ///  服务端启动
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public static void LinkServerStart(string host,int port)
        {
            LinkServer = new Server
            {
                Services =
                    {
                        BindService(new LinkServerFunc())
                    },
                Ports = { new ServerPort(host,port,ServerCredentials.Insecure) }
            };
            LinkServer.Start();
        }

        /// <summary>
        ///  服务端关闭
        /// </summary>
        public static void LinkServerClose()
        {
            LinkServer?.ShutdownAsync().Wait();
        }

        /// <summary>
        /// 客户端启动
        /// </summary>
        /// <param name="strIp"></param>
        public static void LinkClientStart(string strIp)
        {
            Channel prechannel = new Channel(strIp,ChannelCredentials.Insecure);
            LinkClient = new LinkClient(prechannel);
        }

        /// <summary>
        /// 客户端发送消息函数
        /// </summary>
        /// <param name="strRequest"></param>
        /// <returns></returns>
        public static string SendMes(string strRequest)
        {
            Mes mes = new Mes();
            mes.StrRequest = strRequest;
            var res = LinkClient.GetMessage(mes);
            return res.StrReply;
        }



    }
}