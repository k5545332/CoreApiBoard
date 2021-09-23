using CoreApiBoard.Dto;
using CoreApiBoard.Dto.Users;
using CoreApiBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IRepositorys
{
    public interface IUserRepository
    {
        public UserDto Get(string account);

        public IEnumerable<IndexUserDto> GetAll();
        public UpdateUserDto UpdateGet(int id);

        public string Create(UserDto data);
        public string Update(UserDto data);

        public string Delete(UserDto data);
        public UserDto DeleteGet(int id);

    }
}
