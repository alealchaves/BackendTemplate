using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.Core.Entities;
using BackendTemplate.Domain.Core.Interfaces;
using BackendTemplate.Infra.CrossCode;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackendTemplate.Infra.Data.Core.Repositories
{
    public abstract class GeneralRepository<T> : Repository<T>, IRepository<T> where T : GeneralEntity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected GeneralRepository(MyAppContext context, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(context, mapper)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected IQueryable<T> QueryableAtivos(bool stateless = true)
        {
            var result = QueryableAtivos<T>(stateless);
            return result;
        }

        protected IQueryable<TEntity> QueryableAtivos<TEntity>(bool stateless = true)
             where TEntity : GeneralEntity

        {
            var result = Queryable<TEntity>(stateless);
            
            if (HasEntityControl())
                result = result.Where(p => p.EntityControl.Ativo.Equals(true));

            return result;
        }

        protected IQueryable<T> QueryableInativos(bool stateless = true)
        {
            var result = QueryableInativos<T>(stateless);
            return result;
        }

        protected IQueryable<TEntity> QueryableInativos<TEntity>(bool stateless = true)
            where TEntity : GeneralEntity

        {
            var result = Queryable<TEntity>(stateless);

            if (HasEntityControl())
                result = result.Where(p => p.EntityControl != null ?
                p.EntityControl.Ativo.Equals(false) : true);

            return result;
        }

        public async Task<List<T>> Select()
        {
            var result = await QueryableAtivos()
                .ToListAsync();
            return result;
        }

        public async Task<List<T>> Select(Expression<Func<T, bool>> predicate)
        {
            var result = await QueryableAtivos()
                .Where(predicate)
                .ToListAsync();
            return result;
        }

        public async Task<List<TDTO>> Select<TDTO>()
        {
            var result = await QueryableAtivos()
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public async Task<List<TDTO>> Select<TDTO>(Expression<Func<T, bool>> predicate)
        {
            var result = await QueryableAtivos()
                .Where(predicate)
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .Distinct()
                .ToListAsync();
            return result;
        }

        public async Task<T> SelectFirst()
        {
            var result = await QueryableAtivos()
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<T> SelectFirst(Expression<Func<T, bool>> predicate)
        {
            var result = await QueryableAtivos()
                .Where(predicate)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<TDTO> SelectFirst<TDTO>()
        {
            var result = await QueryableAtivos()
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<TDTO> SelectFirst<TDTO>(Expression<Func<T, bool>> predicate)
        {
            var result = await QueryableAtivos()
                .Where(predicate)
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<PagedListResponse<TDTO>> SelectPaged<TDTO>(int page = 1, int pageSize = 10, bool countTotal = false)
        {
            var result = await GetPrivatePaged<TDTO>(null, page, pageSize, countTotal);
            return result;
        }

        public async Task<PagedListResponse<TDTO>> SelectPaged<TDTO>(Expression<Func<T, bool>> predicate, int page = 1, int pageSize = 10, bool countTotal = false)
        {
            var result = await GetPrivatePaged<TDTO>(predicate, page, pageSize, countTotal);
            return result;
        }

        public async Task<T> SelectByHash(Guid hash)
        {
            var result = await Queryable()
                .Where(x => x.Hash.Equals(hash))
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<TDTO> SelectByHash<TDTO>(Guid hash)
        {
            var result = await Queryable()
                .Where(x => x.Hash.Equals(hash))
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SelectIdByHash(Guid hash)
        {
            var result = await Queryable()
                .Where(x => x.Hash.Equals(hash))
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<int>> SelectIdByHash(List<Guid> hashs)
        {
            var result = await Queryable()
                .Where(x => hashs.Contains(x.Hash))
                .Select(x => x.Id)
                .ToListAsync();
            return result;
        }

        public new async Task<T> Insert(T entity)
        {
            if (this._httpContextAccessor != null)
            {
                entity.EntityControl = new EntityControl();
                var user = this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                entity.EntityControl.RegistrarInclusao(user);
            }

            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public new async Task<List<T>> Insert(List<T> entities)
        {
            var result = new List<T>();

            foreach (var entity in entities)
                result.Add(await Insert(entity));

            return result;
        }

        public new async Task<T> Update(T entity)
        {
            var local = dbSet.Local.FirstOrDefault(p => p.Id == entity.Id);

            if (local != null)
                _context.Entry(local).State = EntityState.Detached;

            if (entity.EntityControl == null)
                entity.EntityControl = new EntityControl();

            if (this._httpContextAccessor != null)
            {
                var user = this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                entity.EntityControl.RegistrarAlteracao(user);
            }

            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }

        public new async Task<List<T>> Update(List<T> entities)
        {
            var result = new List<T>();

            foreach (var entity in entities)
                result.Add(await Update(entity));

            return result;
        }

        public new async Task<T> Disable(T entity)
        {
            if (this._httpContextAccessor != null)
            {
                entity.EntityControl = new EntityControl();
                var user = this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                entity.EntityControl.RegistrarInativacao(user);
            }

            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }

        public new async Task<List<T>> Disable(List<T> entities)
        {
            var result = new List<T>();

            foreach (var entity in entities)
                result.Add(await Disable(entity));

            return result;
        }

        public new async Task<int> Delete(object id)
        {
            T existing = dbSet.Find(id);
            dbSet.Remove(existing);

            var task = await _context.SaveChangesAsync();
            return task;
        }

        public new async Task DeleteRange(object[] ids)
        {
            var existing = dbSet.Where(x => ids.Contains(x.Id));
            dbSet.RemoveRange(existing);

            await _context.SaveChangesAsync();
        }

        private Task<PagedListResponse<TDTO>> GetPrivatePaged<TDTO>(
            Expression<Func<T, bool>> predicate, int page = 1,
            int pageSize = 10, bool countTotal = false)
        {
            var queryable = QueryableAtivos();

            if (predicate != null)
                queryable = queryable.Where(predicate);

            var result = queryable
                .ProjectTo<TDTO>(_mapper.ConfigurationProvider)
                .ToPagedListAsync(page, pageSize, countTotal);

            return result;
        }

        private bool HasEntityControl()
        {
            var tipo = typeof(T);
            var entityControlNotMapped = tipo.GetProperty("EntityControl")
                .HasAttribute<NotMappedAttribute>();

            return !entityControlNotMapped;
        }
    }
}
