using CoreApiBoard.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IServices
{
    public interface IEventService
    {
        public string IndexGetData();
        public string GetData(int id);
        public string CreateEventGetData();
        public string CreateEventSubmitData(CreateEventSubmitDto data);
        public string UpdateEventSubmitData(UpdateEventSubmitDto data);
        public string UpdateEventGetData(int id);
        public string DeleteEventData(int id);
        public string AddDataView(int id);
        public string ImageUpload(IFormFileCollection files);
        
    }
}
