namespace RW.PMS.Application.Contracts.DTO.Module;

public class OperationListDto
{
    public int Id { get; set; }

    /// <summary>
    ///     操作编码
    /// </summary>
    public string OperationCode { get; set; }

    /// <summary>
    ///     操作标题
    /// </summary>
    public string Title { get; set; }
}