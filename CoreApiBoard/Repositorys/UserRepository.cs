using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.Dto.Users;
using CoreApiBoard.Interfaces.IRepositorys;
using CoreApiBoard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly BoardContext _boardContext;
        private readonly IMapper _mapper;

        public UserRepository(BoardContext boardContext, IMapper mapper)
        {
            _boardContext = boardContext;
            _mapper = mapper;
        }

        public UserDto Get(string account)
        {
            var data = _boardContext.Users.Where(x => x.Account == account).Select(x => x).FirstOrDefault();

            var UserDto = _mapper.Map<UserDto>(data);

            return UserDto;
        }

        public string Create(UserDto data)
        {
            var Data = _mapper.Map<User>(data);

            if (Data != null)
            {
                _boardContext.Set<User>().Add(Data);
                this.SaveChanges();
                return "ok";
            }
            return "fail";
        }

        public UpdateUserDto UpdateGet(int id)
        {
            var data = _boardContext.Users.Where(x => x.Del == false && x.Userid == id).Include(x => x.AccessLevel).Select(x => x).FirstOrDefault();
            var UpdateUserDto = _mapper.Map<UpdateUserDto>(data);

            return UpdateUserDto;
        }

        public IEnumerable<IndexUserDto> GetAll()
        {

            var data = _boardContext.Users.Where(x => x.Del == false).Include(x => x.AccessLevel).Select(x => x).OrderByDescending(x => x.Userid);

            var UserDtos = _mapper.Map<IEnumerable<IndexUserDto>>(data);

            return UserDtos;
        }

        public void SaveChanges()
        {
            this._boardContext.SaveChanges();
        }


        public string Update(UserDto data)
        {
            if (data != null)
            {
                var Data = _mapper.Map<User>(data);
                var entry = _boardContext.Entry(Data);
                if (entry.State == EntityState.Detached)
                {
                    var set = _boardContext.Set<User>();
                    User attachedEntity = set.Find(Data.Userid);

                    if (attachedEntity != null)
                    {
                        var attachedEntry = _boardContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(Data);
                    }
                    else
                    {
                        entry.State = EntityState.Modified;
                    }
                }
                this.SaveChanges();
                return "ok";
            }
            return "fail";
        }

        public string Delete(UserDto data)
        {
            if (data != null)
            {
                var Data = _mapper.Map<User>(data);
                var entry = _boardContext.Entry(Data);
                if (entry.State == EntityState.Detached)
                {
                    var set = _boardContext.Set<User>();
                    User attachedEntity = set.Find(Data.Userid);

                    if (attachedEntity != null)
                    {
                        var attachedEntry = _boardContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(Data);
                    }
                    else
                    {
                        entry.State = EntityState.Modified;
                    }
                }
                this.SaveChanges();
                return "ok";
            }
            return "fail";
        }

        public UserDto DeleteGet(int id)
        {
            var data = _boardContext.Users.Where(x => x.Del == false && x.Userid == id).Select(x => x).FirstOrDefault();
            var UserDto = _mapper.Map<UserDto>(data);

            return UserDto;
        }

    }
}
