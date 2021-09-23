using CoreApiBoard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IServices
{
    public interface IUserService
    {
        public string IndexGetData();
        public string CreateUserGetData();
        public string CreateUserSubmitData(CreateUserSubmitDto data);
        public string UpdateUserSubmitData(UpdateUserSubmitDto data);
        public string UpdateUserGetData(int id);
        public string DeleteUserData(int id);
    }
}
