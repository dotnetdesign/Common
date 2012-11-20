using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Common extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Removes the nulls.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> values) where T : class
        {
            using (Logger.Assembly.Scope())
            {
                Guard.ArgumentNotNull(values, "values");
                return values.Where(x => x != null);
            }
        }

        /// <summary>
        /// Wraps this region of code between the scope creation and disposal in trace output messages.
        /// </summary>
        /// <param name="logger">The logger object to trace out to.</param>
        /// <returns>The disposable ScopeLogger</returns>
        public static IDisposable Scope(this ILog logger)
        {
            return new ScopeLogger(logger);
        }
    }
}