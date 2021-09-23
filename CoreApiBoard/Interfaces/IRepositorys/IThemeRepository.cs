using CoreApiBoard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IRepositorys
{
    public interface IThemeRepository
    {
        public IEnumerable<ThemeDto> GetAll();
        public IEnumerable<IndexThemeDto> IndexGetAll();
        public UpdateThemeGetDto UpdateGet(int id);

        public string Create(ThemeDto data);
        public string Update(ThemeDto data);

        public string Delete(ThemeDto data);
        public ThemeDto DeleteGet(int id);
    }
}
