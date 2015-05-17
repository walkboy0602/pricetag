using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceTag.Models;
using PriceTag.Entities.Models;
using PriceTag.DAL;

namespace PriceTag.Services
{
    public interface IUserProfileService
    {
        void Insert(UserProfile userProfile);
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class UserProfileService : IUserProfileService
    {
        private AccountDBContext db;

        public UserProfileService()
        {
            db = new AccountDBContext();    
        }

        void IUserProfileService.Insert(UserProfile userProfile)
        {
            db.UserProfile.Add(userProfile);
        }
    }
}
