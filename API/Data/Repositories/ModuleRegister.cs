namespace API.Data.Repositories;

public static class ModuleRegister
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterDataRepositories(this IServiceCollection services)
    {
        services.AddIdentity<IUserRepo, UserRepo>();
    }
}