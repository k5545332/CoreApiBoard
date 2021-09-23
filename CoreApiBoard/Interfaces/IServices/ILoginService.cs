using CoreApiBoard.Dto.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IServices
{
    public interface ILoginService
    {
        public string Login(LoginDto LoginDto);
    }
}
