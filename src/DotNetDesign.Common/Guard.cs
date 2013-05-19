using System;

namespace DotNetDesign.Common
{
	/// <summary>
	/// Guard methods to provide checking of provided arguments.
	/// </summary>
	public static class Guard
	{
		/// <summary>
		/// Checks that the argument is not null.
		/// </summary>
		/// <typeparam name="TArgument">The type of the argument.</typeparam>
		/// <param name="argument">The argument.</param>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="throwIfFails">if set to <c>true</c> throws an exception if the check fails.</param>
		/// <returns>Whether or not the argument passes the check.</returns>
		public static bool ArgumentNotNull<TArgument>(TArgument argument, string argumentName, bool throwIfFails = true)
			where TArgument : class
		{
			using (Logger.Assembly.Scope())
			{
				if (argument != null) return true;

				var ex = new ArgumentNullException(string.Format("Argument {0} cannot be null.", argumentName));
				Logger.Assembly.Error(ex.Message, ex);
				if (throwIfFails) throw ex;

				return false;
			}
		}

		/// <summary>
		/// Checks that the argument is not null or empty. Also checks for whitespace if specified.
		/// </summary>
		/// <param name="argument">The argument.</param>
		/// <param name="argumentName">Name of the argument.</param>
		/// <param name="allowWhitespace">if set to <c>true</c> will pass if only whitespace.</param>
		/// <param name="throwIfFalse">if set to <c>true</c> throws an exception if the check fails.</param>
		/// <returns>Whether or not the argument passes the check.</returns>
		public static bool ArgumentNotNullOrEmpty(string argument, string argumentName, bool allowWhitespace = false, bool throwIfFalse = true)
		{
			using (Logger.Assembly.Scope())
			{
				Exception ex;
				if (allowWhitespace)
				{
					if (!string.IsNullOrEmpty(argument)) return true;
					ex = new ArgumentNullException(
						string.Format("Argument {0} cannot be null, empty, or just contain whitespace.", argumentName));
				}
				else
				{
					if (!string.IsNullOrWhiteSpace(argument)) return true;
					ex = new ArgumentNullException(string.Format("Argument {0} cannot be null or empty.", argumentName));
				}

				Logger.Assembly.Error(ex.Message, ex);
				if (throwIfFalse) throw ex;

				return false;
			}
		}
	}
}