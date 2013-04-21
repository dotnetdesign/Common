using Common.Logging;

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
