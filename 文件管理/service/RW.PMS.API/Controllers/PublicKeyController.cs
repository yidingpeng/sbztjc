using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.RSA;

namespace RW.PMS.API.Controllers
{
    [Route("api/[controller]")]

    public class PublicKeyController : ControllerBase
    {
        [HttpGet]
        public IResponseDto Get()
        {
            LoginRSADto dto = new LoginRSADto();
            dto.PublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDBT2vr+dhZElF73FJ6xiP181txKWUSNLPQQlid6DUJhGAOZblluafIdLmnUyKE8mMHhT3R+Ib3ssZcJku6Hn72yHYj/qPkCGFv0eFo7G+GJfDIUeDyalBN0QsuiE/XzPHJBuJDfRArOiWvH0BXOv5kpeXSXM8yTt5Na1jAYSiQ/wIDAQAB";
            return ResponseDto.Success(dto);
        }
    }
}
