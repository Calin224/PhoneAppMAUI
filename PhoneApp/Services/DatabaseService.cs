using PhoneApp.Model;
using SQLite;

namespace PhoneApp.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection database;
    private static DatabaseService instance;
    private const string DB_NAME = "phone.db3";

    public DatabaseService()
    {
        database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
        database.CreateTableAsync<PhoneItem>();
    }

    public async Task<List<PhoneItem>> GetContacts()
    {
        return await database.Table<PhoneItem>().ToListAsync();
    }

    public async Task<PhoneItem> GetContact(int id)
    {
        return await database.Table<PhoneItem>().Where(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task Create(PhoneItem item)
    {
        await database.InsertAsync(item);
    }

    public async Task Update(PhoneItem item)
    {
        await database.UpdateAsync(item);
    }

    public async Task Delete(PhoneItem item)
    {
        await database.DeleteAsync(item);
    }
}