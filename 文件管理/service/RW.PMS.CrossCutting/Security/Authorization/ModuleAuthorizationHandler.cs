using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RW.PMS.CrossCutting.Security.Authorization
{
    public class ModuleAuthorizationHandler : AuthorizationHandler<ModuleAuthorizationRequirement>
    {
        private readonly IPermissionChecker _permissionChecker;

        public ModuleAuthorizationHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ModuleAuthorizationRequirement requirement)
        {
            if (_permissionChecker.IsGranted(requirement.Module, requirement.Operation))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
