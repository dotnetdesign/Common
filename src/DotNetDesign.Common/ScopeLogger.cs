using System;
using System.Diagnostics;
using Common.Logging;

namespace DotNetDesign.Common
{
	/// <summary>
	/// Disposable object that will write a trace message when created and when disposed.
	/// </summary>
	public class ScopeLogger : IDisposable
	{
		private readonly ILog _logger;
		private readonly string _methodName;
		private readonly bool _shouldLog;

		/// <summary>
		/// Initializes a new instance of the <see cref="ScopeLogger"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		public ScopeLogger(ILog logger)
		{
			if (!logger.IsTraceEnabled) return;

			_logger = logger;
			_methodName = new StackFrame(2).GetMethod().Name;
			_logger.Trace("Enter " + _methodName);
			_shouldLog = true;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (!_shouldLog) return;

			_logger.Trace("Exit " + _methodName);
		}
	}
}