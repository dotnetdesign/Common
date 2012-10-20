using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Static class containing all assembly specific loggers.
    /// </summary>
    internal static class Logger
    {
        /// <summary>
        /// DotNetDesign.Common Assembly Logger
        /// </summary>
        internal static ILog Assembly = LogManager.GetLogger("DotNetDesign.Common");
    }
}
