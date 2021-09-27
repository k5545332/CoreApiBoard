using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.Dto.Users;
using CoreApiBoard.Interfaces.IRepositorys;
using CoreApiBoard.Interfaces.IServices;
using CoreApiBoard.PostgreSQLModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreApiBoard.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessLevelRepository _accessLevelRepository;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IAccessLevelRepository accessLevelRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _accessLevelRepository = accessLevelRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public string IndexGetData()
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var Userid = 0;
            var AccessLevelid = 0;

            if (Claims.Count() > 0)
            {
                Int32.TryParse(Claims.Where(x => x.Type == "Userid").First().Value.ToString(), out Userid);
                Int32.TryParse(Claims.Where(x => x.Type == ClaimTypes.Role).First().Value.ToString(), out AccessLevelid);
            }

            var IndexUserDto = new IndexUserGetDto();

            if (AccessLevelid == 1)
            {
                IndexUserDto.UserDtos = _userRepository.GetAll();
            }
            else if (AccessLevelid == 2)
            {
                IndexUserDto.UserDtos = _userRepository.GetAll().Where(x => x.Userid == Userid);
            }

            return JsonConvert.SerializeObject(IndexUserDto);
        }
       
        public string CreateUserGetData()
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var AccessLevelid = 0;

            if (Claims.Count() > 0)
            {
                Int32.TryParse(Claims.Where(x => x.Type == ClaimTypes.Role).First().Value.ToString(), out AccessLevelid);
            }

            var CreateUserGetDto = new CreateUserGetDto();
            if (AccessLevelid == 1)
            {
                CreateUserGetDto.AccessLevelDtos = _mapper.Map<IEnumerable<AccessLevelDto>>(_accessLevelRepository.GetAll());
            }
            else if(AccessLevelid == 2)
            {
                CreateUserGetDto.AccessLevelDtos = _mapper.Map<IEnumerable<AccessLevelDto>>(_accessLevelRepository.GetAll().Where(x => x.AccessLevelid == 2));
            }
            return JsonConvert.SerializeObject(CreateUserGetDto);
        }

        public string CreateUserSubmitData(CreateUserSubmitDto data)
        {
            try
            {
                var Data = _mapper.Map<UserDto>(data);
                Data.UpdateTime = DateTime.Now;
                Data.Del = false;

                return _userRepository.Create(Data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateUserGetData(int id)
        {
            var Data = _userRepository.UpdateGet(id);
            if (Data!=null)
            {
                var AccessLevelDtos = _mapper.Map<IEnumerable<AccessLevelDto>>(_accessLevelRepository.GetAll());
                var UpdateUserGetDto = new UpdateUserGetDto
                {
                    UpdateUserDto = Data,
                    AccessLevelDtos = AccessLevelDtos
                };
                
                return JsonConvert.SerializeObject(UpdateUserGetDto);
            }
            return "查無資料";
        }

        public string UpdateUserSubmitData(UpdateUserSubmitDto data)
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var Userid = 0;
            var AccessLevelid = 0;

            if (Claims.Count() > 0)
            {
                Int32.TryParse(Claims.Where(x => x.Type == "Userid").First().Value.ToString(), out Userid);
                Int32.TryParse(Claims.Where(x => x.Type == ClaimTypes.Role).First().Value.ToString(), out AccessLevelid);
            }

            if (AccessLevelid != 1)
            {
                if (data.Userid != Userid)
                {
                    return "不要亂改別人的資料!!!";
                }
                
            }
            var Data = _mapper.Map<UserDto>(data);
            Data.UpdateTime = DateTime.Now;

            return _userRepository.Update(Data);
        }

        public string DeleteUserData(int id)
        {
            var Data = _userRepository.DeleteGet(id);
            if (Data != null)
            {
                Data.UpdateTime = DateTime.Now;
                Data.Del = true;
                return _userRepository.Delete(Data);
            }
            return "查無資料";
        }
    }
}
