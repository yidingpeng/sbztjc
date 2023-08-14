using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.Application.Contracts.Input.System
{
    public class ConfigSearchInput : PagedQuery
    {
        public string ConfigCode { get; set; }
    }
}