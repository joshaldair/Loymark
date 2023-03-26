using Application.Contracts;
using Domain.Common;
using Infrastructure.Persistance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
    private Hashtable _repositories;//referencia servicios repositorio
    private readonly Context _context;

    public Context Context => _context;
    public UnitOfWorkRepository(Context context)
    {
        _context = context;
    }

    private IUserRepository _userRepository;
    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

    private IActivityRepository _activityRepository;
    public IActivityRepository ActivityRepository => _activityRepository ?? new ActivityRepository(_context);   

    public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryBase<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IAsyncRepository<TEntity>)_repositories[type];
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
