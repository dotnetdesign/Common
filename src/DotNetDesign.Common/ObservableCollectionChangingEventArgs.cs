using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Observable collection changing event args.
    /// </summary>
    public class ObservableCollectionChangingEventArgs<TItem> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionChangingEventArgs{TItem}"/> class.
        /// </summary>
        /// <param name="changeType">Type of the change.</param>
        /// <param name="item">The item.</param>
        public ObservableCollectionChangingEventArgs(ObservableCollectionChangeTypes changeType, TItem item)
            : this(changeType, new TItem[] { item })
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionChangingEventArgs{TItem}"/> class.
        /// </summary>
        /// <param name="changeType">Type of the change.</param>
        /// <param name="items">The items.</param>
        public ObservableCollectionChangingEventArgs(ObservableCollectionChangeTypes changeType, IEnumerable<TItem> items)
        {
            using (Logger.Assembly.Scope())
            {
                this.ChangeType = changeType;
                this.Items = items;
                this.CancelOperation = false;
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

        /// <summary>
        /// Gets or sets a value indicating whether to cancel the operation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if you should cancel the operation; otherwise, <c>false</c>.
        /// </value>
        public bool CancelOperation { get; set; }
    }
}
