using Calculator;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace CalculatorServer
{
    public class CalculatorServiceImpl : CalculatorService.CalculatorServiceBase
    {
        /// <summary>
        /// 发布响应
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<MyResponse> MyMethod(MyRequest request,ServerCallContext context)
        {
            MyResponse myResponse = new MyResponse();
            foreach (int i in request.Num)
            {
                myResponse.Strs.Add(i.ToString());
            }
            return myResponse;
        }

        /// <summary>
        /// 服务器流（Server Streaming）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ServerStreamingMethod(Request1 request,IServerStreamWriter<Response1> responseStream,ServerCallContext context)
        {
            for (int i = 0 ; i < 10 ; i++)
            {
                Response1 response = new Response1 { Message = request.Message + $"Message {i}" };
                await responseStream.WriteAsync(response);
                await Task.Delay(500); // 模拟每秒发送一次数据
            }
        }

        /// <summary>
        /// 客户端流（Client Streaming）
        /// </summary>
        /// <param name="requestStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<Response2> ClientStreamingMethod(IAsyncStreamReader<Request2> requestStream,ServerCallContext context)
        {
            string str = "";
            while (requestStream.MoveNext().Result)
            {
                str += requestStream.Current.Message;
                Console.WriteLine(requestStream.Current.Message);
            }
            return new Response2 { Message = str };
        }
        /// <summary>
        /// 双向流（Bidirectional Streaming）
        /// </summary>
        /// <param name="requestStream"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task BidirectionalStreamingMethod(IAsyncStreamReader<Request3> requestStream,IServerStreamWriter<Response3> responseStream,ServerCallContext context)
        {
            while (requestStream.MoveNext().Result)
            {
                // 处理客户端发送的请求
                Console.WriteLine(requestStream.Current.Message);
                Response3 response = new Response3 { Message = requestStream.Current.Message + "abc" };
                await responseStream.WriteAsync(response); // 发送响应
                await Task.Delay(500);
            }
        }

    }
}
