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
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public readonly BoardContext _boardContext;
        public readonly IMapper _mapper;

        public Repository(BoardContext boardContext, IMapper mapper)
        {
            _boardContext = boardContext;
            _mapper = mapper;
        }
        public void Create(TEntity Entity)
        {
            if (Entity != null)
            {
                _boardContext.Set<TEntity>().Add(Entity);
                this.SaveChanges();
            }
        }

        public void Edit(TEntity Entity, int Id)
        {
            if (Entity != null)
            {
                var entry = _boardContext.Entry(Entity);
                if (entry.State == EntityState.Detached)
                {
                    var set = _boardContext.Set<TEntity>();
                    TEntity attachedEntity = set.Find(Id);

                    if (attachedEntity != null)
                    {
                        var attachedEntry = _boardContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(Entity);
                    }
                    else
                    {
                        entry.State = EntityState.Modified;
                    }
                }
                this.SaveChanges();
            }
        }

        public bool Delete(TEntity Entity, int Id)
        {
            if (Entity != null)
            {
                var entry = _boardContext.Entry(Entity);
                if (entry.State == EntityState.Detached)
                {
                    var set = _boardContext.Set<TEntity>();
                    TEntity attachedEntity = set.Find(Id);

                    if (attachedEntity != null)
                    {
                        var attachedEntry = _boardContext.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(Entity);
                    }
                    else
                    {
                        entry.State = EntityState.Modified;
                    }
                }
                this.SaveChanges();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._boardContext != null)
                {
                    this._boardContext.Dispose();
                }
            }
        }
        
        public void SaveChanges()
        {
            this._boardContext.SaveChanges();
        }
    }
}