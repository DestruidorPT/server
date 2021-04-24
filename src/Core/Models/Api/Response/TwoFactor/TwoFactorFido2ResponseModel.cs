using System;
using Bit.Core.Models.Table;
using Bit.Core.Models.Business;
using Bit.Core.Enums;
using System.Collections.Generic;
using System.Linq;
using Bit.Core.Repositories;
using Fido2NetLib;
using Bit.Core.Utilities;
using Fido2NetLib.Objects;

namespace Bit.Core.Models.Api
{
    /// <summary>
    /// Class that handles the FIDO2 Response Models to the client, to show the keys and if the FIDO2 two-factor is enable.
    /// </summary>
    public class TwoFactorFido2ResponseModel : ResponseModel
    {
        public TwoFactorFido2ResponseModel(User user, List<Fido2Key> fido2Keys) : base("twoFactorFido2")
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (fido2Keys == null)
            {
                throw new ArgumentNullException(nameof(fido2Keys));
            }

            var provider = user.GetTwoFactorProvider(TwoFactorProviderType.Fido2);
            Enabled = provider?.Enabled ?? false;
            if (fido2Keys == null)
            {
                Fido2Keys = new List<Fido2KeySimplieModel>();
            } else
            {
                Fido2Keys = fido2Keys.Select(k => new Fido2KeySimplieModel(k.Id, k.Name, k.getCredentialTypeString(), k.getAuthenticatorTypeString(), k.getTransportsEnum(), k.Compromised, k.CreationDate)).ToList<Fido2KeySimplieModel>();
            }
        }
        // Tell us if FIDO2 is enable or disable
        public bool Enabled { get; set; }
        // List of FIDO2 key on mode simplify
        public IEnumerable<Fido2KeySimplieModel> Fido2Keys { get; set; }

        /// <summary>
        /// Class is used to simplify the information of the Fido2 key, for not to send information not necessary.
        /// </summary>
        public class Fido2KeySimplieModel
        {
            public Fido2KeySimplieModel(Guid id, string name, string credentialType, string authenticatorType, List<AuthenticatorTransport> transports, bool compromised, DateTime creationDate)
            {
                Id = id.ToString();
                Name = name;
                CredentialType = credentialType;
                AuthenticatorType = authenticatorType;
                Transports = transports;
                Compromised = compromised;
                CreationDate = creationDate;
            }

            public string Id { get; set; }
            public string Name { get; set; }
            public string CredentialType { get; set; }
            public string AuthenticatorType { get; set; }
            public IEnumerable<AuthenticatorTransport> Transports { get; set; }
            public bool Compromised { get; set; }
            public DateTime CreationDate { get; set; }
        }
    }
}
