namespace RW.PMS.Application.Contracts.DTO
{
    /// <summary>
    ///     分页查询对象
    /// </summary>
    public class PagedQuery
    {
        /// <summary>
        ///     页码
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        ///     条数
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
