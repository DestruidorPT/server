using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bit.Core.Models.Table;
using System.Data;
using Dapper;
using Bit.Core.Utilities;
using Fido2NetLib.Objects;

namespace Bit.Core.Repositories.SqlServer
{
    /// <summary>
    /// Class that handles the FIDO2 Keys on the database.
    /// </summary>
    public class Fido2KeyRepository : Repository<Fido2Key, Guid>, IFido2KeyRepository
    {
        public Fido2KeyRepository(GlobalSettings globalSettings)
            : this(globalSettings.SqlServer.ConnectionString, globalSettings.SqlServer.ReadOnlyConnectionString)
        { }

        public Fido2KeyRepository(string connectionString, string readOnlyConnectionString)
            : base(connectionString, readOnlyConnectionString)
        { }

        /// <summary>
        /// Get from the database one FIDO2 Key by id.
        /// </summary>
        public async Task<Fido2Key> GetOneByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var results = await connection.QueryAsync<Fido2Key>(
                    $"[{Schema}].[Fido2Key_ReadById]",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);

                return results.First();
            }
        }

        /// <summary>
        /// Get from the database all FIDO2 Keys belong to the user.
        /// </summary>
        public async Task<ICollection<Fido2Key>> GetManyByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var results = await connection.QueryAsync<Fido2Key>(
                    $"[{Schema}].[Fido2Key_ReadByUserId]",
                    new { UserId = userId },
                    commandType: CommandType.StoredProcedure);

                return results.ToList();
            }
        }

        /// <summary>
        /// Create on database the new FIDO2 Key for the user.
        /// </summary>
        public async Task CreateFido2Key(Guid userId, string name, string credentialId, string publicKey, long signatureCounter, PublicKeyCredentialType credentialType, AuthenticatorAttachment authenticatorType, string transports)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    $"[{Schema}].[Fido2Key_Create]",
                    new
                    {
                        Id = CoreHelpers.GenerateComb(),
                        UserId = userId,
                        Name = name,
                        CredentialId = credentialId,
                        PublicKey = publicKey,
                        SignatureCounter = signatureCounter,
                        CredentialType = credentialType,
                        AuthenticatorType = authenticatorType,
                        Transports = transports,
                        CreationDate = DateTime.UtcNow
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Update the signature counter of one FIDO2 Key
        /// </summary>
        public async Task UpdateSignatureCounterOfFido2Key(Guid id, long signatureCounter)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    $"[{Schema}].[Fido2Key_UpdateSignatureCounter]",
                    new 
                    { 
                        Id = id, 
                        SignatureCounter = signatureCounter
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete all FIDO2 Keys that belongs to the user
        /// </summary>
        public async Task DeleteManyByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    $"[{Schema}].[Fido2Key_DeleteByUserId]",
                    new { UserId = userId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete one FIDO2 Key
        /// </summary>
        public async Task DeleteFido2KeyByIdAsync(Guid id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    $"[{Schema}].[Fido2Key_DeleteById]",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override Task<Fido2Key> GetByIdAsync(Guid id)
        {
            throw new NotSupportedException();
        }

        public override Task ReplaceAsync(Fido2Key obj)
        {
            throw new NotSupportedException();
        }

        public override Task UpsertAsync(Fido2Key obj)
        {
            throw new NotSupportedException();
        }

        public override Task DeleteAsync(Fido2Key obj)
        {
            throw new NotSupportedException();
        }
    }
}
