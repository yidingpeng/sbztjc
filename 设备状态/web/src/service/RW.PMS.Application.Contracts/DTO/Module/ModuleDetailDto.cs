using System.ComponentModel.DataAnnotations;

namespace RW.PMS.Application.Contracts.DTO.Module;

public class ModuleDetailDto
{
    public int? Id { get; set; }

    public int ParentId { get; set; }

    public string ModuleName { get; set; }

    public string Title { get; set; }

    public string Icon { get; set; }

    public string Path { get; set; }

    public string Component { get; set; }

    public int OrderIndex { get; set; }

    public string ModuleCode { get; set; } = "";

    public bool Hidden { get; set; }
}