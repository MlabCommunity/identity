using Grpc.Core;

namespace Lappka.Identity.Application.Exceptions;

public class ProjectGrpcException : RpcException
{
    protected ProjectGrpcException(string message,StatusCode errorCode = StatusCode.Unknown) : base(new Status(errorCode,errorCode.ToString()),message)
    {
    }

}