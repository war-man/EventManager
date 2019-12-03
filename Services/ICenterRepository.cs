using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EventManager.API.Domain.Entities;

namespace EventManager.API.Services
{
    public interface ICenterRepository
    {
        Task<IEnumerable<Center>> GetCentersAsync ();
        void AddCenter(Center center);
        Task<Center> GetCenterByIdAsync(Guid centerId);
        Task<Center> UpdateCenterAsync(Center center);
        Task<bool> DeleteCenterAsync(Center center);
        bool CenterExists(string centerName);
        Task<bool> SaveChangesAsync();
    }
}