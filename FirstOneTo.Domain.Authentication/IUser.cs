using System.Collections.Generic;
using FirstOneTo.Readmodel.Models;
using Nancy.Security;

namespace FirstOneTo.Authentication
{
    public interface IUser : IUserIdentity
    {
        long AggregateId { get; set; }
        string Email { get; set; }
    }
}