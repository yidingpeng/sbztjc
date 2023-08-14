namespace RW.PMS.Application.Contracts.DTO;

public partial class ResponseDto : IResponseDto
{
    public ResponseDto(int code)
    {
        Code = code;
        Msg = string.Empty;
    }

    public ResponseDto(int code, string msg)
    {
        Code = code;
        Msg = msg;
    }

    public int Code { get; }

    public string Msg { get; }
}

public class ResponseDto<T> : ResponseDto, IResponseDto<T>
{
    public ResponseDto(int code, T data) : base(code)
    {
        Data = data;
    }

    public ResponseDto(int code, string msg, T data) : base(code, msg)
    {
        Data = data;
    }

    public T Data { get; }
}

public partial class ResponseDto
{
    public static IResponseDto Success() => new ResponseDto(0, "操作成功");

    public static IResponseDto Success(string msg) => new ResponseDto(0, msg);

    public static IResponseDto Success<T>(T data) => new ResponseDto<T>(0, data);

    public static IResponseDto Error(int code, string msg) => new ResponseDto(code, msg);

    public static IResponseDto Error<T>(int code, string msg, T data = default) => new ResponseDto<T>(code, msg, data);

    public static IResponseDto GetResult(bool result, string successMsg = "成功", string errorMsg = "失败") => new ResponseDto(result ? 0 : 500, result ? successMsg : errorMsg);

    public static IResponseDto UpdateResult(bool result = true, string successMsg = "修改成功", string errorMsg = "修改失败") => new ResponseDto(result ? 0 : 500, result ? successMsg : errorMsg);
    public static IResponseDto InsertResult(bool result = true, string successMsg = "添加成功", string errorMsg = "添加失败") => new ResponseDto(result ? 0 : 500, result ? successMsg : errorMsg);
    public static IResponseDto DeleteResult(bool result = true, string successMsg = "删除成功", string errorMsg = "删除失败") => new ResponseDto(result ? 0 : 500, result ? successMsg : errorMsg);
}