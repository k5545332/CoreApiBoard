using CoreApiBoard.Dto;
using CoreApiBoard.PostgreSQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IRepositorys
{
    public interface IEventRepository
    {
        public IEnumerable<IndexEventDto> GetAll();
        public Event Get(int id);
        public UpdateEventDto UpdateGet(int id);

        public string Create(EventDto data);
        public string Update(EventDto data);

        public string Delete(EventDto data);
        public EventDto DeleteGet(int id);
    }
}
