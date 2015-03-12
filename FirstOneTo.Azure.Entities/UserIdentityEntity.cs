using System;
using System.Globalization;
using Microsoft.WindowsAzure.Storage.Table;

namespace FirstOneTo.Azure.Entities
{
    public class UserIdentityEntity : TableEntity
    {
        public UserIdentityEntity()
        {
        }

        public UserIdentityEntity(long aggregateId,
            string userName,
            string password,
            string claims,
            string email)
        {
            PartitionKey = aggregateId.ToString(CultureInfo.InvariantCulture);
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            AggregateId = aggregateId;
            Password = password;
            UserName = userName;
            Claims = claims;
            Email = email;
        }


        public UserIdentityEntity(long aggregateId,
            string userName,
            string password,
            string claims,
            string email,
            string facebookId)
        {
            PartitionKey = aggregateId.ToString(CultureInfo.InvariantCulture);
            RowKey = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            AggregateId = aggregateId;
            Password = password;
            UserName = userName;
            Claims = claims;
            FacebookId = facebookId;
            Email = email;
        }


        public string Email { get; set; }
        public string Password { get; set; }
        public string FacebookId { get; set; }
        public long AggregateId { get; set; }
        public string UserName { get; set; }
        public string Claims { get; set; }
    }
}