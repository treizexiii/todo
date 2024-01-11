using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using GrpcMessage;

namespace GrpcService.Services;

public static class ResponseFactory
{
    public static Response CreateSuccessResponse()
    {
        var response = new Response
        {
            Success = true,
            Code = (int)ResponseCode.Success,
            ExecuteTime = Timestamp.FromDateTime(DateTime.UtcNow),
        };

        return response;
    }

    public static Response CreateSuccessResponse<T>(IEnumerable<T> data) where T : IMessage
    {
        var repeatedField = new RepeatedField<Any>();
        foreach (var item in data)
        {
            repeatedField.Add(Any.Pack(item));
        }
        var response = new Response
        {
            Success = true,
            Code = (int)ResponseCode.Success,
            ExecuteTime = Timestamp.FromDateTime(DateTime.UtcNow),
            Datas = { repeatedField }
        };
        return response;
    }

    public static Response CreateSuccessResponse<T>(T data) where T : IMessage
    {
        var response = new Response
        {
            Success = true,
            Code = (int)ResponseCode.Success,
            ExecuteTime = Timestamp.FromDateTime(DateTime.UtcNow),
            Data = Any.Pack(data)
        };
        return response;
    }

    public static Response CreateErrorResponse(int code, string message)
    {
        var response = new Response();
        response.Success = false;
        response.Code = code;
        response.Message = message;
        response.ExecuteTime = Timestamp.FromDateTime(DateTime.UtcNow);
        return response;
    }

    public static Response CreateErrorResponse(ResponseCode code, string message)
    {
        var response = new Response();
        response.Success = false;
        response.Code = (int)code;
        response.Message = message;
        response.ExecuteTime = Timestamp.FromDateTime(DateTime.UtcNow);
        return response;
    }
}
