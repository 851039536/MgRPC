[gRPC 官方文档中文版_V1.0 (oschina.net)](https://doc.oschina.net/grpc?t=60132)

[[转\]Protobuf3 语法指南 (colobu.com)](https://colobu.com/2017/03/16/Protobuf3-language-guide/)

1. gRPC 框架的目标就是让远程服务调用更加简单、透明，其负责屏蔽底层的传输方式（TCP/UDP）、序列化方式（XML/Json）和通信细节。服务调用者可以像调用本地接口一样调用远程的服务提供者，而不需要关心底层通信细节和调用过程。
2. gRPC 是一个高性能、开源和通用的 RPC 框架，面向移动和 HTTP/2 设计。目前提供 C、Java 和 Go 语言版本，分别是：grpc, grpc-java, grpc-go. 其中 C 版本支持 C, C++, Node.js, Python, Ruby, Objective-C, PHP 和 C# 支持.
3. 在 gRPC 里客户端应用可以像调用本地对象一样直接调用另一台不同的机器上服务端应用的方法，使得您能够更容易地创建分布式应用和服务。与许多 RPC 系统类似，gRPC 也是基于以下理念：定义一个服务，指定其能够被远程调用的方法（包含参数和返回类型）。在服务端实现这个接口，并运行一个 gRPC 服务器来处理客户端调用。在客户端拥有一个存根能够像服务端一样的方法。



## gRPC的组成部分

- 使用 http2 作为网络传输层
- 使用 protobuf 这个高性能的数据包序列化协议
- 通过 protoc gprc 插件生成易用的 SDK

### 什么是ProtoBuf

ProtoBuf(Protocol Buffers)是一种跨平台、语言无关、可扩展的序列化结构数据的方法，可用于网络数据交换及存储。

protoBuf 是一种Google开发的高效的二进制数据交换格式，常用于不同编程语言之间的数据通信。

在序列化结构化数据的机制中，ProtoBuf是灵活、高效、自动化的，相对常见的XML、JSON，描述同样的信息，ProtoBuf序列化后数据量更小 (在网络中传输消耗的网络流量更少)、序列化/反序列化速度更快、更简单。

一旦定义了要处理的数据的数据结构之后，就可以利用ProtoBuf的代码生成工具生成相关的代码。只需使用 Protobuf 对数据结构进行一次描述，即可利用各种不同语言(proto3支持C++, Java, Python, Go, Ruby, Objective-C, C#)或从各种不同流中对你的结构化数据轻松读写。



### 定义服务

要定义一个服务，你必须在你的 .proto 文件中指定 service:

```csharp
service Link
{
	...
}
```

然后在你的服务中定义 rpc 方法，指定请求的和响应类型。gRPC允 许你定义4种类型的 service 方法，在 RouteGuide 服务中都有使用：

- 一个 简单 RPC ， 客户端使用存根发送请求到服务器并等待响应返回，就像平常的函数调用一样。

```csharp
service Link
{
	//定义了一个 RPC 方法。这个方法名为 GetMessage，它接受一个 Mes 类型的消息作为参数，
	//并返回一个 Mes 类型的消息。在 gRPC 中，客户端可以调用这个方法，并发送一个 Mes 消息给服务端，然后服务端会处理这个消息并返回一个 Mes 消息给客户端。
	rpc GetMessage(Mes) returns (Mes);
}
```



### 定义消息类型

```csharp
syntax = "proto3";
message SearchRequest {
  string query = 1;
  int32 page_number = 2;
  int32 result_per_page = 3;
}
```

- 文件的第一行指定了你正在使用proto3语法：如果你没有指定这个，编译器会使用proto2。这个指定语法行必须是文件的非空非注释的第一个行。
- SearchRequest消息格式有3个字段，在消息中承载的数据分别对应于每一个字段。其中每个字段都有一个名字和一种类型。

指定字段类型

在上面的例子中，所有字段都是标量类型：两个整型（page_number和result_per_page），一个string类型（query）。当然，你也可以为字段指定其他的合成类型，包括枚举（enumerations）或其他消息类型。

分配标识号

正如你所见，在消息定义中，每个字段都有唯一的一个数字标识符。这些标识符是用来在消息的二进制格式中识别各个字段的，一旦开始使用就不能够再改变。注：[1,15]之内的标识号在编码的时候会占用一个字节。[16,2047]之内的标识号则占用2个字节。所以应该为那些频繁出现的消息元素保留 [1,15]之内的标识号。切记：要为将来有可能添加的、频繁出现的标识号预留一些标识号。

最小的标识号可以从1开始，最大到2^29 - 1, or 536,870,911。不可以使用其中的[19000－19999]（ (从FieldDescriptor::kFirstReservedNumber 到 FieldDescriptor::kLastReservedNumber)）的标识号， Protobuf协议实现中对这些进行了预留。如果非要在.proto文件中使用这些预留标识号，编译时就会报警。同样你也不能使用早期保留的标识号。

### 添加更多消息类型

在一个.proto文件中可以定义多个消息类型。在定义多个相关的消息的时候，这一点特别有用——例如，如果想定义与SearchResponse消息类型对应的回复消息格式的话，你可以将它添加到相同的.proto文件中，如：

```csharp
message SearchRequest {
  string query = 1;
  int32 page_number = 2;
  int32 result_per_page = 3;
}
message SearchResponse {
 ...
}
```

### 添加注释

向.proto文件添加注释，可以使用C/C++/Java风格的双斜杠（//） 语法格式，如：

```csharp
message SearchRequest {
  string query = 1;
  int32 page_number = 2;  // Which page number do we want?
  int32 result_per_page = 3;  // Number of results to return per page.
}
```

### 保留标识符（Reserved）

如果你通过删除或者注释所有域，以后的用户在更新这个类型的时候可能重用这些标识号。如果你使用旧版本加载相同的.proto文件会导致严重的问题，包括数据损坏、隐私错误等等。现在有一种确保不会发生这种情况的方法就是为字段tag（reserved name可能会JSON序列化的问题）指定reserved标识符，protocol buffer的编译器会警告未来尝试使用这些域标识符的用户。

```csharp
message Foo {
  reserved 2, 15, 9 to 11;
  reserved "foo", "bar";
}
```

注：不要在同一行reserved声明中同时声明域名字和tag number。

### 从.proto文件生成了什么

当用protocol buffer编译器来运行.proto文件时，编译器将生成所选择语言的代码，这些代码可以操作在.proto文件中定义的消息类型，包括获取、设置字段值，将消息序列化到一个输出流中，以及从一个输入流中解析消息。

- 对C++来说，编译器会为每个.proto文件生成一个.h文件和一个.cc文件，.proto文件中的每一个消息有一个对应的类。
- 对Java来说，编译器为每一个消息类型生成了一个.java文件，以及一个特殊的Builder类（该类是用来创建消息类接口的）。
- 对Python来说，有点不太一样——Python编译器为.proto文件中的每个消息类型生成一个含有静态描述符的模块，，该模块与一个元类（metaclass）在运行时（runtime）被用来创建所需的Python数据访问类。
- 对go来说，编译器会位每个消息类型生成了一个.pd.go文件。
- 对于Ruby来说，编译器会为每个消息类型生成了一个.rb文件。
- javaNano来说，编译器输出类似域java但是没有Builder类
- 对于Objective-C来说，编译器会为每个消息类型生成了一个pbobjc.h文件和pbobjcm文件，.proto文件中的每一个消息有一个对应的类。
- 对于C#来说，编译器会为每个消息类型生成了一个.cs文件，.proto文件中的每一个消息有一个对应的类。
   你可以从如下的文档链接中获取每种语言更多API(proto3版本的内容很快就公布)。[API Reference](https://developers.google.com/protocol-buffers/docs/reference/overview)

### 枚举

当需要定义一个消息类型的时候，可能想为一个字段指定某“预定义值序列”中的一个值。例如，假设要为每一个SearchRequest消息添加一个 corpus字段，而corpus的值可能是UNIVERSAL，WEB，IMAGES，LOCAL，NEWS，PRODUCTS或VIDEO中的一个。 其实可以很容易地实现这一点：通过向消息定义中添加一个枚举（enum）并且为每个可能的值定义一个常量就可以了。

在下面的例子中，在消息格式中添加了一个叫做Corpus的枚举类型——它含有所有可能的值 ——以及一个类型为Corpus的字

```csharp
message SearchRequest {
  string query = 1;
  int32 page_number = 2;
  int32 result_per_page = 3;
  enum Corpus {
    UNIVERSAL = 0;
    WEB = 1;
    IMAGES = 2;
    LOCAL = 3;
    NEWS = 4;
    PRODUCTS = 5;
    VIDEO = 6;
  }
  Corpus corpus = 4;
}
```

如你所见，Corpus枚举的第一个常量映射为0：每个枚举类型必须将其第一个类型映射为0，这是因为：

- 必须有有一个0值，我们可以用这个0值作为默认值。
- 这个零值必须为第一个元素，为了兼容proto2语义，枚举类的第一个值总是默认值。



## 标量数值类型

一个标量消息字段可以含有一个如下的类型——该表格展示了定义于.proto文件中的类型，以及与之对应的、在自动生成的访问类中定义的类型：

| .proto Type | Notes                                                        | C++ Type | Java Type  | Python Type[2] | Go Type | Ruby Type                      | C# Type    | PHP Type       |
| ----------- | ------------------------------------------------------------ | -------- | ---------- | -------------- | ------- | ------------------------------ | ---------- | -------------- |
| double      |                                                              | double   | double     | float          | float64 | Float                          | double     | float          |
| float       |                                                              | float    | float      | float          | float32 | Float                          | float      | float          |
| int32       | 使用变长编码，对于负值的效率很低，如果你的域有可能有负值，请使用sint64替代 | int32    | int        | int            | int32   | Fixnum 或者 Bignum（根据需要） | int        | integer        |
| uint32      | 使用变长编码                                                 | uint32   | int        | int/long       | uint32  | Fixnum 或者 Bignum（根据需要） | uint       | integer        |
| uint64      | 使用变长编码                                                 | uint64   | long       | int/long       | uint64  | Bignum                         | ulong      | integer/string |
| sint32      | 使用变长编码，这些编码在负值时比int32高效的多                | int32    | int        | int            | int32   | Fixnum 或者 Bignum（根据需要） | int        | integer        |
| sint64      | 使用变长编码，有符号的整型值。编码时比通常的int64高效。      | int64    | long       | int/long       | int64   | Bignum                         | long       | integer/string |
| fixed32     | 总是4个字节，如果数值总是比总是比228大的话，这个类型会比uint32高效。 | uint32   | int        | int            | uint32  | Fixnum 或者 Bignum（根据需要） | uint       | integer        |
| fixed64     | 总是8个字节，如果数值总是比总是比256大的话，这个类型会比uint64高效。 | uint64   | long       | int/long       | uint64  | Bignum                         | ulong      | integer/string |
| sfixed32    | 总是4个字节                                                  | int32    | int        | int            | int32   | Fixnum 或者 Bignum（根据需要） | int        | integer        |
| sfixed64    | 总是8个字节                                                  | int64    | long       | int/long       | int64   | Bignum                         | long       | integer/string |
| bool        |                                                              | bool     | boolean    | bool           | bool    | TrueClass/FalseClass           | bool       | boolean        |
| string      | 一个字符串必须是UTF-8编码或者7-bit ASCII编码的文本。         | string   | String     | str/unicode    | string  | String (UTF-8)                 | string     | string         |
| bytes       | 可能包含任意顺序的字节数据。                                 | string   | ByteString | str            | []byte  | String (ASCII-8BIT)            | ByteString | string         |

## 消息传输类型

第一种是简单的RPC，就是一问一答，传入一个请求对象，返回一个返回对象。如果前面的请求没有 处理完，则后面的请求就一直不处理。
 第二种是服务端的流式RPC，就是一问多答，传入一个请求对象，服务端可以返回多个结果对象。
 第三种是客户端的流式RPC，就是多问一答，客户端传入多个请求对象，服务端返回一个结果对象。
 第四种是双向的流式RPC，就是结合客户端流式和服务端流式的RPC，可以传入多个请求对象，返回多 个结果对象。





## c#方式使用gRPC

参考 : [C#封装GRPC类库及调用简单实例 - wtc87 - 博客园 (cnblogs.com)](https://www.cnblogs.com/wtc87/p/17002800.html)

包括：GRPC文件的创建生成、服务端和客户端函数类库的封装、创建服务端和客户端调用测试。

### 创建并生成GRPC服务文件

1. 创建新项目控制台应用 , 项目名称(MgRPC)
2. 安装三个nuget包 Google.Protobuf , Grpc.Core , Grpc.Tools
3. 项目添加新建项，选择类，修改名称为Link.proto，添加后把Link.proto里面内容清空

#### 定义Protocol

添加代码。测试实例为服务端和客户端传输字符串消息，只定义了一个方法（客户端调用，服务端重写），传输内容包括请求字符串和回复字符串。此处可自行定义。

```csharp
syntax = "proto3";//proto3 是 Protocol Buffers 的第三个版本

// 指定了生成的 C# 代码的命名空间为 LinkService。当使用 protobuf 编译器 (protoc) 将这个 .proto 文件转换为 C# 代码时，生成的类将位于 LinkService 命名空间中
option csharp_namespace = "LinkService";

//定义了一个名为 Link 的 gRPC 服务。在 gRPC 中，服务是由一个或多个 RPC 方法组成的
service Link
{
	//定义了一个 RPC 方法。这个方法名为 GetMessage，它接受一个 Mes 类型的消息作为参数，
	//并返回一个 Mes 类型的消息。在 gRPC 中，客户端可以调用这个方法，并发送一个 Mes 消息给服务端，然后服务端会处理这个消息并返回一个 Mes 消息给客户端。
	rpc GetMessage(Mes) returns (Mes);
}

//定义了一个名为 Mes 的消息类型。在 protobuf 中，消息是由一系列字段组成的，每个字段都有一个名称、一个类型和一个标识符。
message Mes
{
    // 客户端发送
	// 定义了一个名为 StrRequest 的字段，类型为 string，标识符为 1。这个标识符在消息内部是唯一的，并且一旦分配就不能更改，因为它被用于序列化和反序列化过程中的字段识别。
	string StrRequest = 1;
	// 同样定义了一个名为 StrReply 的字段，类型为 string，标识符为 2。(服务端回复)
	string StrReply = 2;
}
```

#### 设置Link.proto

右键Link.proto文件选择属性，生成操作选择如图：

![image](D:\sw\Console\MgRPC\assets\image-20240410141147-iwtmto9.png)



#### 生成cs文件代码

生成解决方案。在下图路径得到自动生成的两个类。

![image](D:\sw\Console\MgRPC\assets\image-20240410141337-cia2735.png)

至此，获得GRPC服务需要的三个文件：Link.proto、Link.cs、LinkGrpc.cs。可以将这三个文件放在一个项目中直接使用，需要重写一下服务端方法、创建服务端和客户端的启动方法。但是如果不同的项目软件之间通讯需要各自如此开发。可以先封装成一个GRPC类库供其他项目直接调用。

### 服务端和客户端类库的封装

1. 创建类库(.NET Framework)项目 , 项目名称(GrpcLink)
2. 项目添加现有项，上面获得的三个文件(Link.cs , LinkGrpc.cs)。安装nuget包：Grpc.Core和Google.Protobuf。
3. 创建两个类：LinkFunc用于放此类库可用于外部引用调用的方法。LinkServerFunc基于Link.LinkBase，用于重写在proto文件中定义的方法。

对于不同的项目，在客户端请求时，服务端要根据自身情况回复想回的内容，因此可以提供一个委托供外部自行开发回复函数。

在LinkFunc类中定义如下：

```csharp
public static Func<string, string> ReplyMes;
```

#### LinkServerFunc

在LinkServerFunc类重写GetMessage方法如下

```csharp
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
```

#### LinkFunc

```csharp
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
```



#### 生成引用库

生成解决方案。Debug中可以得到项目的dll文件GrpcLink.dll，其他项目可以引用使用了。



### 创建服务端和客户端调用测试

1. 创建两个控制台(.NET Framework)项目 , 项目名称TestCilent , TestServer
2. 将上述GrpcLink.dll文件分别放入两个项目中，并添加dll引用。 如果没有则安装nuget包：Grpc.Core和Google.Protobuf(此次测试没有安装)

#### TestServer服务端

```csharp
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
```

#### TestCilent客户端

```csharp
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
                    var line = Console.ReadLine();
                    var ret = LinkFunc.SendMes(line);
                    //获取到服务端返回的值
                    Console.WriteLine(ret);

                }
            }

            Console.WriteLine("连接失败 , 即将退出");
            Thread.Sleep(2000);
        }
    }
}
```

### 测试图例

![image](D:\sw\Console\MgRPC\assets\image-20240410142820-r180j0l.png)



## c#在gRPC多种通信类型使用

参考:[基于C](https://blog.csdn.net/qq1084517825/article/details/134272985)[的GRPC_c](https://blog.csdn.net/qq1084517825/article/details/134272985)[ grpc-CSDN博客](https://blog.csdn.net/qq1084517825/article/details/134272985)

### 创建并生成GRPC服务文件

1. 创建新项目控制台应用 , 项目名称(MgRPC)
2. 安装三个nuget包 Google.Protobuf , Grpc.Core , Grpc.Tools
3. 项目添加新建项，选择类，修改名称为Calculator.proto，添加后把Calculator.proto里面内容清空

```csharp
// Protocol Buffers 语法版本 proto3 版本
syntax = "proto3";
// 定义了消息类型和服务的包名，类似于命名空间，用于避免命名冲突。
package Calculator;
// 定义了一个 gRPC 服务。在大括号中，您可以列出服务方法的定义。
service CalculatorService 
{
    // 定义了一个服务方法。rpc 表示定义一个远程过程调用（RPC）方法。MyMethod 是方法的名称，MyRequest 是输入参数类型，MyResponse 是输出参数类型。
    rpc MyMethod (MyRequest) returns (MyResponse);

  // 服务器流式方法
  rpc ServerStreamingMethod(Request1) returns (stream Response1);

  // 客户端流式方法
  rpc ClientStreamingMethod(stream Request2) returns (Response2);

  // 双向流式方法
  rpc BidirectionalStreamingMethod(stream Request3) returns (stream Response3);
}
message MyRequest 
{
    repeated int32 num = 10;
}
message MyResponse 
{
    repeated string strs = 10;
}

message Request1
{
    string Message = 1;
}
message Response1
{
    string Message = 1;
}
message Request2
{
    string Message = 1;
}
message Response2
{
    string Message = 1;
}
message Request3
{
    string Message = 1;
}
message Response3
{
    string Message = 1;
}
```

#### 设置Calculator.proto

右键Calculator.proto文件选择属性，生成操作选择如图：

![image](D:\sw\Console\MgRPC\assets\image-20240410155639-p9kvi72.png)

#### 生成cs文件代码

生成解决方案。在下图路径得到自动生成的两个类。



![image](D:\sw\Console\MgRPC\assets\image-20240410155735-obqc706.png)



### 服务端代码

新建CalculatorServer控制台

CalculatorServiceImpl类

```csharp
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
```

使用

```csharp
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
```

### 客户端代码

新建CalculatorCilent控制台

```csharp
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
```

### 通信类型使用场景

在gRPC中，服务器流、客户端流、双向流以及发布响应（这里可能指的是Unary RPC，即简单的请求-响应模式）各自适用于不同的使用场景。

#### 服务器流 (Server Streaming RPC)

使用场景：适用于服务器需要主动向客户端推送一系列消息的场景。

- 实时数据推送：如金融应用中，客户端请求实时股票数据，服务器则持续发送最新的股票价格信息。
- 日志或事件流：服务器可以将实时日志或系统事件流推送给客户端。

#### 客户端流 (Client Streaming RPC)

使用场景：适用于客户端需要持续发送数据到服务器，而服务器只需要返回一个响应的场景。

- 数据上传：如大文件上传，客户端可以分块发送数据，服务器在接收完所有数据后返回一个确认响应。
- 实时数据采集：如物联网应用中，传感器设备可以持续发送数据到服务器进行分析。

#### 双向流 (Bidirectional Streaming RPC)

使用场景：适用于客户端和服务器都需要能够发送和接收消息的场景。

- 实时通信：如聊天应用，客户端和服务器之间可以实时发送和接收消息。
- 在线多人游戏：多个客户端可以实时发送游戏动作到服务器，同时接收服务器的游戏状态更新。
- 音视频通话：在音视频通话中，每个参与者都需要发送音频和视频流，并接收其他参与者的音视频流。

#### 发布响应 (Unary RPC)

使用场景：适用于简单的请求-响应模式，客户端发送一个请求，服务器返回一个响应。

- 远程过程调用：客户端调用服务器上的一个方法，并等待服务器的响应。
- 数据查询：客户端发送查询请求，服务器返回查询结果。
- 简单的CRUD操作：如数据库的增删改查操作，客户端发送操作请求，服务器执行操作并返回结果。

总的来说，这四种通信模式在gRPC中提供了灵活性和可扩展性，使得开发者能够根据具体的应用需求选择合适的通信模式。

## 使用场景

1. 微服务架构：在微服务架构中，服务之间的通信是一个核心问题。gRPC 提供了一种标准化的方式来定义和实现服务接口，使得不同的服务可以轻松地相互通信。
2. 跨语言通信：由于 gRPC 支持多种语言，它允许不同语言编写的服务之间进行通信。这在多语言环境中特别有用，比如一个团队可能使用 Go 编写后端服务，而另一个团队可能使用 C# 或 Java。
3. 高性能通信：gRPC 使用了 HTTP/2 作为其传输层，这提供了诸如双向流、多路复用和头部压缩等高级功能，从而提高了通信的性能和效率。
4. 实时数据流：gRPC 支持双向流式传输，这意味着客户端和服务器可以长时间保持连接，并实时地发送和接收数据。这对于需要实时数据更新的应用（如实时股票行情、在线游戏等）非常有用。
5. 分布式系统：在分布式系统中，服务之间需要频繁地通信以协调工作。gRPC 提供了一种可靠且高效的方式来实现这种通信。
6. 跨平台兼容性：gRPC 可以在多种操作系统和平台上运行，包括 Windows、Linux、macOS 等，这使得它成为一种非常灵活的通信解决方案。
7. 安全性：gRPC 支持使用 TLS/SSL 进行加密通信，这确保了数据在传输过程中的安全性。此外，它还可以与身份验证和授权机制结合使用，以提供更强的安全性。

## 总结

gRPC 是一个功能强大且灵活的 RPC 框架，特别适用于微服务架构中的服务间通信。通过使用 Protobuf 定义服务和消息，gRPC 提供了一种高效、可靠且跨语言的方式来实现服务之间的通信。在需要高性能、实时通信或跨语言互操作性的场景中，gRPC 是一个理想的选择。