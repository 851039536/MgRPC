using FileService;
using Grpc.Core;
using System.IO;
using System.Threading.Tasks;

public class FileServiceImpl : FileService.FileService.FileServiceBase
{
    public override async Task SendFileContent(FileRequest request,IServerStreamWriter<FileResponse> responseStream,ServerCallContext context)
    {
        //指定在程序根目录中logs文件夹下的Filename
        string filePath = Path.Combine("logs",request.Filename);
        using (var fileStream = new FileStream(filePath,FileMode.Open,FileAccess.Read))
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = fileStream.Read(buffer,0,buffer.Length)) != 0)
            {
                var response = new FileResponse { Content = Google.Protobuf.ByteString.CopyFrom(buffer,0,bytesRead) };
                await responseStream.WriteAsync(response);
            }
        }
    }
}