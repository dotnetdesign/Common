﻿using System;
using Common.Logging;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Base Logger Class
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    [Serializable]
    public abstract class BaseLogger<TType>
    {
        [NonSerialized]
        private Lazy<ILog> _lazyLogger;

        /// <summary>
        /// Shared logger.
        /// </summary>
        protected ILog Logger
        {
            get
            {
                if (_lazyLogger == null)
                {
                    _lazyLogger = new Lazy<ILog>(InitializeLazyLogger);
                }

                return _lazyLogger.Value;
            }
        }

        private static ILog InitializeLazyLogger()
        {
            return LogManager.GetLogger(typeof(TType));
        }
    }
}