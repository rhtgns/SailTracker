using SailTracker.Business.Operations.User.Dtos;
using SailTracker.Business.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);

        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}

