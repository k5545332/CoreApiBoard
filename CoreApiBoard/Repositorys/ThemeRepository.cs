using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.Interfaces.IRepositorys;
using CoreApiBoard.Interfaces.IServices;
using CoreApiBoard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Repositorys
{
    class ThemeRepository : IThemeRepository
    {
        private readonly BoardContext _boardContext;
        private readonly IMapper _mapper;

        public ThemeRepository(BoardContext boardContext, IMapper mapper)
        {
            _boardContext = boardContext;
            _mapper = mapper;
        }
        public IEnumerable<ThemeDto> GetAll()
        {

            var data = _boardContext.Themes.Where(x => x.Del == false).Select(x => x);

            var ThemeDtos = _mapper.Map<IEnumerable<ThemeDto>>(data);

            return ThemeDtos;
        }

        public IEnumerable<IndexThemeDto> IndexGetAll()
        {
            var data = _boardContext.Themes.Where(x => x.Del == false).Include(x => x.User).Select(x => x);

            var ThemeDtos = _mapper.Map<IEnumerable<IndexThemeDto>>(data);

            return ThemeDtos;
        }

        public string Create(ThemeDto data)
        {
            var Data = _mapper.Map<Theme>(data);

            if (Data != null)
            {
                _boardContext.Set<Theme>().Add(Data);
                this.SaveChanges();
                return "ok";
            }
            return "fail";
        }

        public UpdateThemeGetDto UpdateGet(int id)
        {
            var data = _boardContext.Themes.Where(x => x.Del == false && x.Themeid == id).Select(x => x).FirstOrDefault();
            var UpdateThemeDto = _mapper.Map<UpdateThemeGetDto>(data);

            return UpdateThemeDto;
        }


        public void SaveChanges()
        {
            this._boardContext.SaveChanges();
        }

        public string Update(ThemeDto data)
        {
            if (data != null)
            {
                var Data = _mapper.Map<Theme>(data);
                var entry = _boardContext.Entry(Data);
                if (entry.State == EntityState.Detached)
                {
                    var set = _boardContext.Set<Theme>();
                    Theme attachedEntity = set.Find(Data.Themeid);

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

        public string Delete(ThemeDto data)
        {
            if (data != null)
            {
                var Data = _mapper.Map<Theme>(data);
                var entry = _boardContext.Entry(Data);
                if (entry.State == EntityState.Detached)
                {
                    var set = _boardContext.Set<Theme>();
                    Theme attachedEntity = set.Find(Data.Themeid);

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

        public ThemeDto DeleteGet(int id)
        {
            var data = _boardContext.Themes.Where(x => x.Del == false && x.Themeid == id).Select(x => x).FirstOrDefault();
            var ThemeDto = _mapper.Map<ThemeDto>(data);

            return ThemeDto;
        }
    }
}