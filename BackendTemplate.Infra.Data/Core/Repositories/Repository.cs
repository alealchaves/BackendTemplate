using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTemplate.Infra.Data.Core.Repositories
{
    public abstract class Repository<T> : 
        IRepositoryInsert<T>, IRepositoryUpdate<T>,
        IRepositoryDisable<T>, IRepositoryDelete<T>,
        IRepositoryDeleteRange<T> where T : Entity
    {
        protected readonly IMapper _mapper;
        protected readonly MyAppContext _context;
        protected readonly DbSet<T> dbSet = null;

        protected Repository(MyAppContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

            dbSet = _context.Set<T>();
        }

        protected IQueryable<T> Queryable(bool stateless = true)
        {
            var result = Queryable<T>(stateless);
            return result;
        }

        protected IQueryable<TEntity> Queryable<TEntity>(bool stateless = true)
             where TEntity : Entity
        {
            var result = stateless
                ? _context.Set<TEntity>().AsNoTracking()
                : _context.Set<TEntity>().AsQueryable();

            return result;
        }

        public async Task<T> SelectById(object id)
        {
            var result = await Queryable()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<TDTO> SelectById<TDTO>(object id)
        {
            var result = await Queryable()
                .Where(x => x.Id.Equals(id))
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> Delete(object id)
        {
            T existing = dbSet.Find(id);
            dbSet.Remove(existing);

            var task = await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteRange(object[] ids)
        {
            var existing = dbSet.Where(x => ids.Contains(x.Id));
            dbSet.RemoveRange(existing);

            var task = await _context.SaveChangesAsync();
        }

        public async Task<T> Insert(T entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> Insert(List<T> entities)
        {
            var result = new List<T>();

            foreach (var entity in entities)
                result.Add(await Insert(entity));

            return result;
        }

        public async Task<T> Update(T entity)
        {
            var local = dbSet.Local.FirstOrDefault(p => p.Id == entity.Id);

            if (local != null)
                _context.Entry(local).State = EntityState.Detached;

            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> Update(List<T> entities)
        {
            var result = new List<T>();

            foreach (var entity in entities)
                result.Add(await Update(entity));

            return result;
        }

        public async Task<T> Disable(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> Disable(List<T> entities)
        {
            var result = new List<T>();

            foreach (var entity in entities)
                result.Add(await Disable(entity));

            return result;
        }
    }
}
