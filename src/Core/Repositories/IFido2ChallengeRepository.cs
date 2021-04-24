using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bit.Core.Enums;
using Bit.Core.Models.Table;
using Fido2NetLib.Objects;

namespace Bit.Core.Repositories
{
    public interface IFido2ChallengeRepository : IRepository<Fido2Challenge, int>
    {
        Task<Fido2Challenge> GetLastCreatedByUserIdAsync(Guid userId);
        Task<ICollection<Fido2Challenge>> GetManyNotExpiredByUserIdAsync(Guid userId);
        Task CreateFido2Challenge(Guid userId, string origin, string options, Fido2ActionType actions, DateTime timeOutDate);
        Task DeleteManyExpiredAsync();
        Task DeleteFido2ChallengeByIdAsync(int id);
    }
}
