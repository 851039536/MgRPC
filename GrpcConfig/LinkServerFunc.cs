using Grpc.Core;
using LinkService;
using System.Threading.Tasks;
using static LinkService.Link;

namespace GrpcLink
{
    /// <summary>
    /// 重写在proto文件中定义的方法
    /// </summary>
    public class LinkServerFunc : LinkBase
    {
        public override Task<Mes> GetMessage(Mes request,ServerCallContext context)
        {
            Mes mes = new Mes();
            mes.StrReply = LinkFunc.ReplyMes(request.StrRequest);
            return Task.FromResult(mes);
        }

    }
}