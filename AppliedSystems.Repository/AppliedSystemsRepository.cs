using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppliedSystems.Common.Exceptions;
using AppliedSystems.Domain;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Repository
{
    public class AppliedSystemsRepository<TEntity> /*: IAppliedSystemsRepository<TEntity>*/ where TEntity : IEntity
    {
        //protected readonly NIPFContext Context;
        //protected readonly IAppSettings AppSettings;
        //protected readonly RetryPolicy RetryPolicy;
        //protected readonly RetryPolicy AsyncRetryPolicy;

        //public BaseNipfRepository(NIPFContext context, IAppSettings appSettings, ISqlExceptionRetryPolicy sqlExceptionRetryPolicy)
        //{
        //    if (context == null)
        //    {
        //        throw new ArgumentNullException("context");
        //    }

        //    if (appSettings == null)
        //    {
        //        throw new ArgumentNullException("appSettings");
        //    }

        //    if (sqlExceptionRetryPolicy == null)
        //    {
        //        throw new ArgumentNullException("sqlExceptionRetryPolicy");
        //    }

        //    AppSettings = appSettings;
        //    Context = context;

        //    Context.Database.CommandTimeout = 120;

        //    RetryPolicy = Policy
        //        .Handle<SqlException>(sqlExceptionRetryPolicy.IsTransientException)
        //        .Retry(appSettings.SqlExceptionRetryCount, (exception, retryCount) =>
        //        {
        //            // TODO: Log the exception and we are retrying - event log?
        //        });

        //    AsyncRetryPolicy = Policy
        //        .Handle<SqlException>(sqlExceptionRetryPolicy.IsTransientException)
        //        .RetryAsync(appSettings.SqlExceptionRetryCount, (exception, retryCount) =>
        //        {
        //            // TODO: Log the exception and we are retrying - event log?
        //        });
        //}

        //public ICollection<TEntity> GetAll()
        //{
        //    return RetryPolicy.Execute(() => Context.Set<TEntity>().ToList());
        //}

        //public async Task<ICollection<TEntity>> GetAllAsync()
        //{
        //    return await AsyncRetryPolicy.ExecuteAsync(() => Context.Set<TEntity>().ToListAsync());
        //}

        //public TEntity Get(int id)
        //{
        //    return RetryPolicy.Execute(() => Context.Set<TEntity>().Find(id));
        //}

        //public async Task<TEntity> GetAsync(int id)
        //{
        //    return await AsyncRetryPolicy.ExecuteAsync(async () => await Context.Set<TEntity>().FindAsync(id));
        //}

        //public TEntity Find(Expression<Func<TEntity, bool>> match)
        //{
        //    return RetryPolicy.Execute(() => Context.Set<TEntity>().FirstOrDefault(match));
        //}

        //public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        //{
        //    return await AsyncRetryPolicy.ExecuteAsync(async () => await Context.Set<TEntity>().SingleOrDefaultAsync(match));
        //}

        //public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        //{
        //    return RetryPolicy.Execute(() => Context.Set<TEntity>().Where(match.Compile()).ToList());
        //}

        //public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        //{
        //    return await AsyncRetryPolicy.ExecuteAsync(() => Task.FromResult(Context.Set<TEntity>().Where(match.Compile()).ToList()));
        //}

        //public TEntity Add(TEntity t)
        //{
        //    return RetryPolicy.Execute(() =>
        //    {
        //        Context.Set<TEntity>().Add(t);
        //        SaveChanges();
        //        return t;
        //    });
        //}

        //public async Task<TEntity> AddAsync(TEntity t)
        //{
        //    return await AsyncRetryPolicy.ExecuteAsync(async () =>
        //    {
        //        Context.Set<TEntity>().Add(t);
        //        await SaveChangesAsync();
        //        return t;
        //    });
        //}

        //public TEntity Update(TEntity updatedEntity, int key)
        //{
        //    if (updatedEntity == null)
        //    {
        //        throw new ArgumentNullException("updatedEntity");
        //    }

        //    TEntity existingEntity = RetryPolicy.Execute(() => Context.Set<TEntity>().Find(key));

        //    if (existingEntity == null)
        //    {
        //        throw new EntityNotFoundException<TEntity>(updatedEntity);
        //    }

        //    try
        //    {
        //        RetryPolicy.Execute(() =>
        //        {
        //            Context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
        //            SaveChanges();
        //        });
        //    }
        //    catch (DbUpdateConcurrencyException concurrencyException)
        //    {
        //        throw new RepositoryException(RepositoryExceptionType.Update, concurrencyException);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new RepositoryException(RepositoryExceptionType.Update, exception);
        //    }

        //    return existingEntity;
        //}

        //public async Task<TEntity> UpdateAsync(TEntity updatedEntity, int key)
        //{
        //    if (updatedEntity == null)
        //    {
        //        throw new ArgumentNullException("updatedEntity");
        //    }

        //    TEntity existingEntity = await Context.Set<TEntity>().FindAsync(key);

        //    if (existingEntity == null)
        //    {
        //        throw new EntityNotFoundException<TEntity>(updatedEntity);
        //    }

        //    try
        //    {
        //        await AsyncRetryPolicy.ExecuteAsync(async () =>
        //        {
        //            Context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
        //            await SaveChangesAsync();
        //        });
        //    }
        //    catch (DbUpdateConcurrencyException concurrencyException)
        //    {
        //        throw new RepositoryException(RepositoryExceptionType.Update, concurrencyException);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new RepositoryException(RepositoryExceptionType.Update, exception);
        //    }

        //    return existingEntity;
        //}

        //public void Delete(TEntity t)
        //{
        //    try
        //    {
        //        RetryPolicy.Execute(() =>
        //        {
        //            Context.Set<TEntity>().Remove(t);
        //            SaveChanges();
        //        });
        //    }
        //    catch (DBConcurrencyException concurrencyException)
        //    {
        //        throw new RepositoryException(RepositoryExceptionType.Delete, concurrencyException);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new RepositoryException(RepositoryExceptionType.Delete, exception);
        //    }
        //}

        //public async Task<int> DeleteAsync(TEntity t)
        //{
        //    try
        //    {
        //        return await AsyncRetryPolicy.ExecuteAsync(async () =>
        //        {
        //            Context.Set<TEntity>().Remove(t);
        //            return await SaveChangesAsync();
        //        });
        //    }
        //    catch (DBConcurrencyException concurrencyException)
        //    {
        //        throw new RepositoryException(RepositoryExceptionType.Delete, concurrencyException);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new RepositoryException(RepositoryExceptionType.Delete, exception);
        //    }
        //}

        //public int Count()
        //{
        //    return RetryPolicy.Execute(() => Context.Set<TEntity>().Count());
        //}

        //public async Task<int> CountAsync()
        //{
        //    return await AsyncRetryPolicy.ExecuteAsync(() => Context.Set<TEntity>().CountAsync());
        //}

        //protected int SaveChanges()
        //{
        //    return AppSettings.AutoSaveChanges ? RetryPolicy.Execute(() => Context.SaveChanges()) : 0;
        //}

        //protected async Task<int> SaveChangesAsync()
        //{
        //    if (AppSettings.AutoSaveChanges)
        //    {
        //        return await AsyncRetryPolicy.ExecuteAsync(async () => await Context.SaveChangesAsync());
        //    }

        //    return 0;
        //}
    }
}
