using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bit.Core.Models.Table;
using Fido2NetLib.Objects;

namespace Bit.Core.Repositories
{
    public interface IFido2KeyRepository : IRepository<Fido2Key, Guid>
    {
        Task<Fido2Key> GetOneByIdAsync(Guid id);
        Task<ICollection<Fido2Key>> GetManyByUserIdAsync(Guid userId);
        Task CreateFido2Key(Guid userId, string name, string credentialId, string publicKey, long signatureCounter, PublicKeyCredentialType credentialType, AuthenticatorAttachment authenticatorType, string transports);
        Task UpdateSignatureCounterOfFido2Key(Guid fido2KeyId, long signatureCounter);
        Task DeleteManyByUserIdAsync(Guid userId);
        Task DeleteFido2KeyByIdAsync(Guid id);
    }
}
