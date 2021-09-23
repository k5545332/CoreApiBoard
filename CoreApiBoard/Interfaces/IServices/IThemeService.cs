using CoreApiBoard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IServices
{
    public interface IThemeService
    {
        public string IndexGetData();
        public string CreateThemeSubmitData(CreateThemeSubmitDto data);
        public string UpdateThemeSubmitData(UpdateThemeSubmitDto data);
        public string UpdateThemeGetData(int id);
        public string DeleteThemeData(int id);
    }
}
