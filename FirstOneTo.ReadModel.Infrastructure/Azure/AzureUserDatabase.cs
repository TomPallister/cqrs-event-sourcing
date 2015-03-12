using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FirstOneTo.Authentication;
using FirstOneTo.Azure.Entities;
using FirstOneTo.Helpers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace FirstOneTo.ReadModel.Infrastructure.Azure
{
    public class AzureUserDatabase : IReadModelUserDatabase
    {
        private readonly CloudTable _cloudTable;

        public AzureUserDatabase(CloudStorageAccount cloudStorageAccount, string tableReference)
        {
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            _cloudTable = cloudTableClient.GetTableReference(tableReference);
            _cloudTable.CreateIfNotExists();
        }

        public async Task<UserIdentity> GetUserAsync(string userName, string password)
        {
            return await Task.Run(() => GetUser(userName, password));
        }

        public async Task<UserIdentity> GetUserAsync(string userName)
        {
            return await Task.Run(() => GetUser(userName));
        }

        public async Task<UserIdentity> GetUserByEmailAsync(string email)
        {
            return await Task.Run(() => GetUserByEmail(email));
        }

        public async Task<UserIdentity> GetUserAsync(long aggregateId)
        {
            return await Task.Run(() => GetUser(aggregateId));
        }

        public async Task<UserIdentity> GetUserByFacebookIdAsync(string facebookId)
        {
            return await Task.Run(() => GetUserByFacebookId(facebookId));
        }

        public async Task AddUserAsync(string userName, string password, long aggregateId, List<string> claims,
            string email)
        {
            var submittedChallengeEntity = new UserIdentityEntity(aggregateId, userName, password,
                AzureListStringHelpers.CreateDelimitedString(claims), email);
            TableOperation insertOperation = TableOperation.Insert(submittedChallengeEntity);
            await _cloudTable.ExecuteAsync(insertOperation);
        }

        public async Task AddUserAsync(string userName, string password, long aggregateId, List<string> claims,
            string email,
            string facebookId)
        {
            var submittedChallengeEntity = new UserIdentityEntity(aggregateId, userName, password,
                AzureListStringHelpers.CreateDelimitedString(claims), email, facebookId);
            TableOperation insertOperation = TableOperation.Insert(submittedChallengeEntity);
            await _cloudTable.ExecuteAsync(insertOperation);
        }

        public void AddUser(string userName, string password, long aggregateId, List<string> claims,
            string email)
        {
            var submittedChallengeEntity = new UserIdentityEntity(aggregateId, userName, password,
                AzureListStringHelpers.CreateDelimitedString(claims), email);
            TableOperation insertOperation = TableOperation.Insert(submittedChallengeEntity);
            _cloudTable.Execute(insertOperation);
        }

        public void AddUser(string userName, string password, long aggregateId, List<string> claims,
            string email,
            string facebookId)
        {
            var submittedChallengeEntity = new UserIdentityEntity(aggregateId, userName, password,
                AzureListStringHelpers.CreateDelimitedString(claims), email, facebookId);
            TableOperation insertOperation = TableOperation.Insert(submittedChallengeEntity);
            _cloudTable.Execute(insertOperation);
        }

        public UserIdentity GetUser(string userName, string password)
        {
            UserIdentity user = GetUser(userName);
            if (user != null && BCryptHelpers.DoesPasswordMatch(user.Password, password))
            {
                return user;
            }
            return null;
        }

        public async Task ResetPasswordAsync(long aggregateId, string password)
        {
            UserIdentityEntity userIdentity = await GetUserWithETagAsync(aggregateId);
            userIdentity.Password = password;
            TableOperation insertOperation = TableOperation.Replace(userIdentity);
            await _cloudTable.ExecuteAsync(insertOperation);
        }

        public void ResetPassword(long aggregateId, string password)
        {
            UserIdentityEntity userIdentity = GetUserWithETag(aggregateId);
            userIdentity.Password = password;
            TableOperation insertOperation = TableOperation.Replace(userIdentity);
            _cloudTable.Execute(insertOperation);
        }

        public UserIdentity GetUser(string userName)
        {
            IQueryable<UserIdentity> query =
                from entity in _cloudTable.CreateQuery<UserIdentityEntity>()
                where entity.UserName == userName
                select
                    new UserIdentity(entity.AggregateId, entity.UserName, entity.Password, entity.Email,
                        AzureListStringHelpers.GetItemsFromDelimitedString(entity.Claims));
            return query.FirstOrDefault();
        }

        public UserIdentity GetUserByEmail(string email)
        {
            IQueryable<UserIdentity> query =
                from entity in _cloudTable.CreateQuery<UserIdentityEntity>()
                where entity.Email == email
                select
                    new UserIdentity(entity.AggregateId, entity.UserName, entity.Password, entity.Email,
                        AzureListStringHelpers.GetItemsFromDelimitedString(entity.Claims));
            return query.FirstOrDefault();
        }

        public UserIdentity GetUserByFacebookId(string facebookId)
        {
            IQueryable<UserIdentity> query =
                from entity in _cloudTable.CreateQuery<UserIdentityEntity>()
                where entity.FacebookId == facebookId
                select
                    new UserIdentity(entity.AggregateId, entity.UserName, entity.Password, entity.Email,
                        AzureListStringHelpers.GetItemsFromDelimitedString(entity.Claims));
            return query.FirstOrDefault();
        }

        public UserIdentity GetUser(long aggregateId)
        {
            IQueryable<UserIdentity> query =
                from entity in _cloudTable.CreateQuery<UserIdentityEntity>()
                where entity.PartitionKey == aggregateId.ToString(CultureInfo.InvariantCulture)
                select
                    new UserIdentity(entity.AggregateId, entity.UserName, entity.Password, entity.Email,
                        AzureListStringHelpers.GetItemsFromDelimitedString(entity.Claims));
            return query.FirstOrDefault();
        }

        public async Task<UserIdentityEntity> GetUserWithETagAsync(long aggregateId)
        {
            return await Task.Run(() => GetUserWithETag(aggregateId));
        }

        private UserIdentityEntity GetUserWithETag(long aggregateId)
        {
            IQueryable<UserIdentityEntity> query =
                from entity in _cloudTable.CreateQuery<UserIdentityEntity>()
                where entity.PartitionKey == aggregateId.ToString(CultureInfo.InvariantCulture)
                select entity;
            return query.FirstOrDefault();
        }
    }
}