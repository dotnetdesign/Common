using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Common.Logging;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
    /// 
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class ObjectCopier
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            using (Logger.Assembly.Scope())
            {
                Logger.Assembly.Trace(m => m("Cloning {0}.", source));
                if (!typeof(T).IsSerializable)
                {
                    var ex = new ArgumentException("The type must be serializable.", "source");
                    Logger.Assembly.Error(ex.Message, ex);
                    throw ex;
                }

                // Don't serialize a null object, simply return the default for that object
                if (ReferenceEquals(source, null))
                {
                    Logger.Assembly.Trace("Object is null, returning default.");
                    return default(T);
                }

                Logger.Assembly.Trace("Object is not null. Serializing and returning deserialized clone.");
                var formatter = new BinaryFormatter();
                var stream = new MemoryStream();
                using (stream)
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(stream);
                }
            }
        }
    } 
}