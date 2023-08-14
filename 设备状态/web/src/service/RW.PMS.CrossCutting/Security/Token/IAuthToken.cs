using System.Security.Claims;

namespace RW.PMS.CrossCutting.Security.Token
{
    public interface IAuthToken
    {
        string Create(params Claim[] claims);
    }
}