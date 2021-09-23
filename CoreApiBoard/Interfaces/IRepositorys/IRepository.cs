using CoreApiBoard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiBoard.Interfaces.IRepositorys
{
    public interface IRepository<TEntity> : IDisposable
    where TEntity : class
    {
        void Create(TEntity Entity);

        void Edit(TEntity Entity,int Id);

        bool Delete(TEntity Entity, int Id);
    }
}
