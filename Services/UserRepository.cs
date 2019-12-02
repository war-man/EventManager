using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EventManager.API.Data;
using EventManager.API.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventManager.API.Services
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private DataContext _dataContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository (DataContext dataContext, ILogger<UserRepository> logger)
        {
            _dataContext = dataContext ??
                throw new ArgumentNullException (nameof (dataContext));
            _logger = logger ??
                throw new ArgumentNullException (nameof (logger));
        }
        public void AddUser (User userToAdd)
        {
            if (userToAdd == null)
            {
                throw new ArgumentNullException (nameof (userToAdd));
            }

            _dataContext.Add (userToAdd);
        }

        public async Task<IEnumerable<User>> GetUsersAsync ()
        {
            return await _dataContext.Users.ToListAsync ();
        }

        public async Task<bool> SaveChangesAsync ()
        {
            return (await _dataContext.SaveChangesAsync () > 0);
        }

        public bool UserExists(string username, string email)
        {
            return _dataContext.Users.Any(u => u.Username == username || u.Email == email);
        }

        public void Dispose ()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
        }

        protected virtual void Dispose (bool disposing)
        {
            if (disposing)
            {
                if (_dataContext != null)
                {
                    _dataContext.Dispose ();
                    _dataContext = null;
                }
            }
        }
    }
}
