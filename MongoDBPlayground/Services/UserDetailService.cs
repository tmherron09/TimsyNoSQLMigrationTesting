using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBPlayground.Models;

namespace MongoDBPlayground.Services
{
    public interface IUserDetailService
    {
        Task<List<UserDetail>> GetAsync();
        Task<UserDetail?> GetAsync(string id);
        Task CreateAsync(UserDetail newUserDetail);
        Task UpdateAsync(string id, UserDetail updatedUser);
        Task RemoveAsync(string id);
    }

    public class UserDetailService : IUserDetailService
    {
        private readonly IMongoCollection<UserDetail> _userCollection;

        public UserDetailService(
            IOptions<UserDetailDatabaseSettings> userDetailDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                userDetailDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                userDetailDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<UserDetail>(
                userDetailDatabaseSettings.Value.UserDetailCollectionName);
        }

        public async Task<List<UserDetail>> GetAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();

        public async Task<UserDetail?> GetAsync(string id) =>
            await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(UserDetail newUserDetail) =>
            await _userCollection.InsertOneAsync(newUserDetail);

        public async Task UpdateAsync(string id, UserDetail updatedUser) =>
            await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.Id == id);
    }
}
