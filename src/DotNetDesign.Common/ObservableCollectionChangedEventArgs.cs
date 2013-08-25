using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Observable collection changed event args.
    /// </summary>
    public class ObservableCollectionChangedEventArgs<TItem> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionChangedEventArgs{TItem}"/> class.
        /// </summary>
        /// <param name="changeType">Type of the change.</param>
        /// <param name="item">The item.</param>
        public ObservableCollectionChangedEventArgs(ObservableCollectionChangeTypes changeType, TItem item)
            : this(changeType, new TItem[] { item })
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionChangedEventArgs{TItem}"/> class.
        /// </summary>
        /// <param name="changeType">Type of the change.</param>
        /// <param name="items">The items.</param>
        public ObservableCollectionChangedEventArgs(ObservableCollectionChangeTypes changeType, IEnumerable<TItem> items)
        {
            using (Logger.Assembly.Scope())
            {
                this.ChangeType = changeType;
                this.Items = items;
            }
        }

        /// <summary>
        /// Gets the type of the change.
        /// </summary>
        /// <value>
        /// The type of the change.
        /// </value>
        public ObservableCollectionChangeTypes ChangeType { get; private set; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable<TItem> Items { get; private set; }
    }
}
