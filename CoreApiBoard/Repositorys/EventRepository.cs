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
    public class EventRepository :  IEventRepository
    {
        private readonly BoardContext _boardContext;
        private readonly IMapper _mapper;

        public EventRepository(BoardContext boardContext, IMapper mapper)
        {
            _boardContext = boardContext;
            _mapper = mapper;
        }

        public string Create(EventDto data)
        {
            var Data = _mapper.Map<Event>(data);

            if (Data != null)
            {
                _boardContext.Set<Event>().Add(Data);
                this.SaveChanges();
                return "ok";
            }
            return "fail";
        }

        public UpdateEventDto UpdateGet(int id)
        {
            var data = _boardContext.Events.Where(x => x.Del == false && x.Eventid == id).Include(x => x.User).Include(x => x.Theme).Select(x => x).FirstOrDefault();
            var UpdateEventDto = _mapper.Map<UpdateEventDto>(data);

            return UpdateEventDto;
        }

        public IEnumerable<IndexEventDto> GetAll()
        {

            var data = _boardContext.Events.Where(x => x.Del == false).Include(x => x.User).Include(x => x.Theme).Select(x => x).OrderByDescending(x => x.Eventid);

            var EventDtos = _mapper.Map<IEnumerable<IndexEventDto>>(data);

            return EventDtos;
        }

        public Event Get(int id)
        {

            var data = _boardContext.Events.Where(x => x.Del == false && x.Eventid == id).Include(x => x.User).Include(x => x.Theme).Select(x => x).FirstOrDefault();


            return data;
        }

        public void SaveChanges()
        {
            this._boardContext.SaveChanges();
        }

        public string Update(EventDto data)
        {
            if (data != null)
            {
                var Data = _mapper.Map<Event>(data);
                var entry = _boardContext.Entry(Data);
                if (entry.State == EntityState.Detached)
                {
                    var set = _boardContext.Set<Event>();
                    Event attachedEntity = set.Find(Data.Eventid);

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

        public string Delete(EventDto data)
        {
            if (data != null)
            {
                var Data = _mapper.Map<Event>(data);
                var entry = _boardContext.Entry(Data);
                if (entry.State == EntityState.Detached)
                {
                    var set = _boardContext.Set<Event>();
                    Event attachedEntity = set.Find(Data.Eventid);

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

        public EventDto DeleteGet(int id)
        {
            var data = _boardContext.Events.Where(x => x.Del == false && x.Eventid == id).Select(x => x).FirstOrDefault();
            var EventDto = _mapper.Map<EventDto>(data);

            return EventDto;
        }

    }
}