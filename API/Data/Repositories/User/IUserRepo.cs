using System.Linq.Expressions;
using API.Data.Models;

namespace API.Data.Repositories;

/// <summary>
/// 
/// </summary>
public interface IUserRepo 
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    IQueryable<User> GetUsers(Expression<Func<User, bool>>? predicate = null,
        Expression<Func<User, object>>[]? includeProperties = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    Task<User?> GetUserById(Guid id, Expression<Func<User, object>>[]? includeProperties = null);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="tempSave"></param>
    /// <returns></returns>
    Task<User> CreateUser(User user, bool tempSave = false);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="tempSave"></param>
    /// <returns></returns>
    Task<User> UpdateUser(User user, bool tempSave = false);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="tempSave"></param>
    /// <returns></returns>
    Task<User> DeleteUser(User user, bool tempSave = false);
}