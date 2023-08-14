namespace RW.PMS.Application.Contracts.DTO
{
    public interface IResponseDto
    {
        /// <summary>
        ///     状态码
        /// </summary>
        int Code { get; }

        /// <summary>
        ///     信息
        /// </summary>
        string Msg { get; }
    }

    public interface IResponseDto<out T> : IResponseDto
    {
        /// <summary>
        ///     数据
        /// </summary>
        T Data { get;}
    }

    public interface IPagedResponseDto<out T> : IResponseDto<T>
    {
        /// <summary>
        ///     数据总数
        /// </summary>
        long Count { get; }
    }
}