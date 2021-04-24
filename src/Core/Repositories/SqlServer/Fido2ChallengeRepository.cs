using System;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bit.Core.Models.Table;
using System.Data;
using Dapper;
using Bit.Core.Enums;
using System.Collections.Generic;

namespace Bit.Core.Repositories.SqlServer
{
    /// <summary>
    /// Class that handles the challenges on the database.
    /// </summary>
    public class Fido2ChallengeRepository : Repository<Fido2Challenge, int>, IFido2ChallengeRepository
    {
        public Fido2ChallengeRepository(GlobalSettings globalSettings)
            : this(globalSettings.SqlServer.ConnectionString, globalSettings.SqlServer.ReadOnlyConnectionString)
        { }

        public Fido2ChallengeRepository(string connectionString, string readOnlyConnectionString)
            : base(connectionString, readOnlyConnectionString)
        { }

        /// <summary>
        /// Get from the database the last challenge created for the user
        /// </summary>
        public async Task<Fido2Challenge> GetLastCreatedByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var results = await connection.QueryAsync<Fido2Challenge>(
                    $"[{Schema}].[Fido2Challenge_ReadLastCreatedByUserId]",
                    new { UserId = userId },
                    commandType: CommandType.StoredProcedure);

                return results.Single();
            }
        }

        /// <summary>
        /// Get from the database all challenges that aren’t expired, and are from the user.
        /// </summary>
        public async Task<ICollection<Fido2Challenge>> GetManyNotExpiredByUserIdAsync(Guid userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var results = await connection.QueryAsync<Fido2Challenge>(
                    $"[{Schema}].[Fido2Challenge_ReadManyNotExpired]",
                    new { UserId = userId },
                    commandType: CommandType.StoredProcedure);

                return results.ToList();
            }
        }

        /// <summary>
        /// Create on database the new challenge for the user.
        /// </summary>
        public async Task CreateFido2Challenge(Guid userId, string origin, string options, Fido2ActionType action, DateTime timeOutDate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    $"[{Schema}].[Fido2Challenge_Create]",
                    new
                    {
                        UserId = userId,
                        Origin = origin,
                        Options = options,
                        Action = action,
                        CreationDate = DateTime.UtcNow,
                        TimeOutDate = timeOutDate
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete the first 100 challenges from the database where they are expired.
        /// </summary>
        public async Task DeleteManyExpiredAsync()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    $"[{Schema}].[Fido2Challenge_DeleteManyExpired]",
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Delete the specific challenge.
        /// </summary>
        public async Task DeleteFido2ChallengeByIdAsync(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    $"[{Schema}].[Fido2Challenge_DeleteById]",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public override Task<Fido2Challenge> GetByIdAsync(int id)
        {
            throw new NotSupportedException();
        }

        public override Task ReplaceAsync(Fido2Challenge obj)
        {
            throw new NotSupportedException();
        }

        public override Task UpsertAsync(Fido2Challenge obj)
        {
            throw new NotSupportedException();
        }

        public override Task DeleteAsync(Fido2Challenge obj)
        {
            throw new NotSupportedException();
        }
    }
}
