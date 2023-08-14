using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Security.Authorization;
using RW.PMS.CrossCutting.Security.User;

namespace RW.PMS.Application.Authorization
{
    public class PermissionChecker : IPermissionChecker
    {
        private readonly ICurrentUser _currentUser;
        private readonly IRoleService _roleService;

        public PermissionChecker(ICurrentUser currentUser, IRoleService roleService)
        {
            _currentUser = currentUser;
            _roleService = roleService;
        }

        public bool IsGranted(string module, OperationType operation)
        {
            return true;
            //return _currentUser.IsAuthenticated && _roleService.Check(_currentUser.RoleId, module, operation);
        }
    }
}