namespace Adidas.Framework.Repository.EntityFramework
{
    using System.Data.Entity;

    using Adidas.Framework.Repository.Context;

    public abstract class DbContextBase : DbContext
    {
        protected DbContextBase(IContextConnectionStringProvider provider)
            : base(provider.ConnectionString)
        {
        }

        protected DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
    }
}
