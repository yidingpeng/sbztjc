using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.Application.Contracts.Output.System
{
    public class OrganizationTreeOutput: TreeList<OrganizationTreeOutput>
    {
        public int Id { get; set; }

        public string OrganizationName { get; set; }
    }
}