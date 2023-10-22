using System.Linq.Expressions;
using API.Data.Models;
using H.Core.Data.EFCore.DbContextFactory;
using H.Core.Data.EFCore.Repository;

namespace API.Data.Repositories;

public class UserRepo : IUserRepo
{
    private readonly IDbContextFactory _dbContextFactory;
    private readonly IGenericRepository<User> _repo;

    /// <summary>
    /// Init
    /// </summary>
    /// <param name="dbContextFactory"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public UserRepo(IDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _repo = _dbContextFactory.UnitOfWork<DxudevContext, User>().Repository ??
                throw new InvalidOperationException();
    }

    /// <inheritdoc />
    public IQueryable<User> GetUsers(Expression<Func<User, bool>>? predicate = null,
        Expression<Func<User, object>>[]? includeProperties = null)
    {
        return _repo.Get(predicate, includeProperties);
    }

    /// <inheritdoc />
    public async Task<User> CreateUser(User user, bool tempSave = false)
    {
        await _repo.InsertAsync(user);

        _ = tempSave
            ? await _dbContextFactory.UnitOfWork<DxudevContext, User>().SaveTempChangesAsync()
            : await _dbContextFactory.SaveAllAsync();

        return user;
    }

    /// <inheritdoc />
    public async Task<User> UpdateUser(User user, bool tempSave = false)
    {
        await _repo.UpdateAsync(user);

        _ = tempSave
            ? await _dbContextFactory.UnitOfWork<DxudevContext, User>().SaveTempChangesAsync()
            : await _dbContextFactory.SaveAllAsync();

        return user;
    }

    /// <inheritdoc />
    public async Task<User> DeleteUser(User user, bool tempSave = false)
    {
        await _repo.DeleteAsync(user);

        _ = tempSave
            ? await _dbContextFactory.UnitOfWork<DxudevContext, User>().SaveTempChangesAsync()
            : await _dbContextFactory.SaveAllAsync();

        return user;
    }

    /// <inheritdoc />
    public async Task<User?> GetUserById(Guid id,
        Expression<Func<User, object>>[]? includeProperties = null)
    {
        var user = await _repo.GetByIdAsync(id, includeProperties);
        return user;
    }
}