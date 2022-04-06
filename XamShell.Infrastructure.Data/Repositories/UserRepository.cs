using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using XamShell.Domain.Models.Data;
using XamShell.Domain.Repositories;
using XamShell.Infrastructure.Data.ExternalServices;
using XamShell.Infrastructure.Data.Helpers;

namespace XamShell.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<UserRepository> Instance = new AsyncLazy<UserRepository>(async () =>
        {
            var instance = new UserRepository();
            CreateTableResult result = await Database.CreateTableAsync<User>();
            return instance;
        });

        public UserRepository()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<User>> GetItemsAsync()
        {
            return Database.Table<User>().ToListAsync();
        }

        public Task<int> SaveItemAsync(User item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
        
    }
}