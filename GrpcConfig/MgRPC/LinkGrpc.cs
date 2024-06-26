// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Link.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace LinkService {
  /// <summary>
  ///定义了一个名为 Link 的 gRPC 服务。在 gRPC 中，服务是由一个或多个 RPC 方法组成的
  /// </summary>
  public static partial class Link
  {
    static readonly string __ServiceName = "Link";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LinkService.Mes> __Marshaller_Mes = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LinkService.Mes.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::LinkService.Mes, global::LinkService.Mes> __Method_GetMessage = new grpc::Method<global::LinkService.Mes, global::LinkService.Mes>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetMessage",
        __Marshaller_Mes,
        __Marshaller_Mes);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::LinkService.LinkReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Link</summary>
    [grpc::BindServiceMethod(typeof(Link), "BindService")]
    public abstract partial class LinkBase
    {
      /// <summary>
      ///定义了一个 RPC 方法。这个方法名为 GetMessage，它接受一个 Mes 类型的消息作为参数，
      ///并返回一个 Mes 类型的消息。在 gRPC 中，客户端可以调用这个方法，并发送一个 Mes 消息给服务端，然后服务端会处理这个消息并返回一个 Mes 消息给客户端。
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::LinkService.Mes> GetMessage(global::LinkService.Mes request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for Link</summary>
    public partial class LinkClient : grpc::ClientBase<LinkClient>
    {
      /// <summary>Creates a new client for Link</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public LinkClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Link that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public LinkClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected LinkClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected LinkClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      ///定义了一个 RPC 方法。这个方法名为 GetMessage，它接受一个 Mes 类型的消息作为参数，
      ///并返回一个 Mes 类型的消息。在 gRPC 中，客户端可以调用这个方法，并发送一个 Mes 消息给服务端，然后服务端会处理这个消息并返回一个 Mes 消息给客户端。
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::LinkService.Mes GetMessage(global::LinkService.Mes request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetMessage(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///定义了一个 RPC 方法。这个方法名为 GetMessage，它接受一个 Mes 类型的消息作为参数，
      ///并返回一个 Mes 类型的消息。在 gRPC 中，客户端可以调用这个方法，并发送一个 Mes 消息给服务端，然后服务端会处理这个消息并返回一个 Mes 消息给客户端。
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::LinkService.Mes GetMessage(global::LinkService.Mes request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetMessage, null, options, request);
      }
      /// <summary>
      ///定义了一个 RPC 方法。这个方法名为 GetMessage，它接受一个 Mes 类型的消息作为参数，
      ///并返回一个 Mes 类型的消息。在 gRPC 中，客户端可以调用这个方法，并发送一个 Mes 消息给服务端，然后服务端会处理这个消息并返回一个 Mes 消息给客户端。
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::LinkService.Mes> GetMessageAsync(global::LinkService.Mes request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetMessageAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///定义了一个 RPC 方法。这个方法名为 GetMessage，它接受一个 Mes 类型的消息作为参数，
      ///并返回一个 Mes 类型的消息。在 gRPC 中，客户端可以调用这个方法，并发送一个 Mes 消息给服务端，然后服务端会处理这个消息并返回一个 Mes 消息给客户端。
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::LinkService.Mes> GetMessageAsync(global::LinkService.Mes request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetMessage, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override LinkClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new LinkClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(LinkBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetMessage, serviceImpl.GetMessage).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, LinkBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetMessage, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::LinkService.Mes, global::LinkService.Mes>(serviceImpl.GetMessage));
    }

  }
}
#endregion
