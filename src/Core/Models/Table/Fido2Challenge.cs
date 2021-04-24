using System;

namespace Bit.Core.Models.Table
{
    /// <summary>
    /// Class of FIDO2 Challenge, here is data saved on the DB and the information 
    /// necessary to use on registry or authentication using FIDO2.
    /// </summary>
    public class Fido2Challenge : ITableObject<int>
    {
        // ID unique to the Challenge table on DB.
        public int Id { get; set; }
        // ID of the user, to say that this challenge belongs to this user id.
        public Guid UserId { get; set; }
        // Origin of the request, is used to check if the origin of the requests 
        // and responses from the user comes from the same origin.
        public string Origin { get; set; }
        // Options created for the user to use FIDO2.
        public string Options { get; set; }
        // Type of action, registration or authentication.
        public Enums.Fido2ActionType Action { get; set; }
        // Date and time when the challenge was created.
        public DateTime CreationDate { get; internal set; } = DateTime.UtcNow;
        // Date and time when the challenge will go expire.
        public DateTime TimeOutDate { get; set; }
        public void SetNewId() {}
    }
}
