using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Regular expressions
        /// </summary>
        public static class RegExs
        {
            /// <summary>
            /// The email address regular expression.
            /// </summary>
            public const string Email = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            /// <summary>
            /// Regular Expression used to enforce a strong password.
            /// </summary>
            public const string StrongPassword = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[@#$%^&+=]|.*\d)^.{8,32}$";
        }
    }
}
