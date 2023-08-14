using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.Application.Contracts.Input.System
{
    public class ModuleSearchInput : PagedQuery
    {
        public int? ParentId { get; set; }
    }
}