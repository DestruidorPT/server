using System;
using System.Collections.Generic;
using System.Text;

namespace Bit.Core.Enums
{
    /// <summary>
    /// Enum class where enumerates the type of actions the FIDO2 has, used on the challenge to specify the type of challenge.
    /// </summary>
    public enum Fido2ActionType : byte
    {
        Registration = 0,
        Authentication = 1
    }
}
