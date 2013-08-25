using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Defines functionality for an observable collection.
    /// </summary>
    /// <typeparam name="TItem">The type of the items stored in the collection.</typeparam>
    public interface IObservableCollection<TItem> : ICollection<TItem>
    {
        /// <summary>
        /// Occurs before collection is changed.
        /// </summary>
        event EventHandler<ObservableCollectionChangingEventArgs<TItem>> Changing;

        /// <summary>
        /// Occurs when collection has changed.
        /// </summary>
        event EventHandler<ObservableCollectionChangedEventArgs<TItem>> Changed;
    }
}
