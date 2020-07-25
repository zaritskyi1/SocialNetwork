using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.Helpers;
using SocialNetwork.DAL.Models;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialNetworkContext _context;

        public UserRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public Task<PagedList<User>> GetUsers(QueryOptions queryOptions)
        {
            var query = _context.Users.AsQueryable();

            return PagedList<User>.CreateAsync(query, queryOptions);
        }

        public Task<bool> IsUserWithIdExists(string id)
        {
            return _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}