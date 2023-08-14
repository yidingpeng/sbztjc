using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.DTO.Module;

public class ModuleWithOperationDto : TreeList<ModuleWithOperationDto>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public List<int> RoleChecked { get; set; }

    public List<int> UserChecked { get; set; }

    public List<OperationDto> Operation { get; set; }
}


public class OperationDto
{
    public int Id { get; set; }

    public string Title { get; set; }
}