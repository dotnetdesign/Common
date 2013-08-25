using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetDesign.Common
{
    /// <summary>
    /// Implementation of IObservableCollection with a backing collection of same type.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    public class ObservableCollection<TItem> : IObservableCollection<TItem>
    {
        private ICollection<TItem> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollection{TItem}"/> class.
        /// </summary>
        public ObservableCollection() : this(0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollection{TItem}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public ObservableCollection(int capacity) : this(capacity, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollection{TItem}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ObservableCollection(IEnumerable<TItem> items) : this(0, items) { }

        /// <summary>
        /// Prevents a default instance of the <see cref="ObservableCollection{TItem}"/> class from being created.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="items">The items.</param>
        private ObservableCollection(int capacity, IEnumerable<TItem> items)
        {
            using (Logger.Assembly.Scope())
            {
                if (items == null)
                {
                    _collection = new List<TItem>(capacity);
                }
                else
                {
                    _collection = new List<TItem>(items);
                }
            }
        }

        /// <summary>
        /// Occurs before collection is changed.
        /// </summary>
        public event EventHandler<ObservableCollectionChangingEventArgs<TItem>> Changing = delegate { };

        /// <summary>
        /// Called when collection is changing.
        /// </summary>
        /// <param name="eventArgs">The event arguments.</param>
        /// <returns></returns>
        protected virtual ObservableCollectionChangingEventArgs<TItem> OnChanging(ObservableCollectionChangingEventArgs<TItem> eventArgs)
        {
            using (Logger.Assembly.Scope())
            {
                Guard.ArgumentNotNull(eventArgs, "eventArgs");

                Changing.Invoke(this, eventArgs);

                return eventArgs;
            }
        }

        /// <summary>
        /// Occurs when collection has changed.
        /// </summary>
        public event EventHandler<ObservableCollectionChangedEventArgs<TItem>> Changed = delegate { };

        /// <summary>
        /// Called when collection has changed.
        /// </summary>
        /// <param name="eventArgs">The event arguments.</param>
        /// <returns></returns>
        protected virtual void OnChanged(ObservableCollectionChangedEventArgs<TItem> eventArgs)
        {
            using (Logger.Assembly.Scope())
            {
                Guard.ArgumentNotNull(eventArgs, "eventArgs");

                Changed.Invoke(this, eventArgs);
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public void Add(TItem item)
        {
            using (Logger.Assembly.Scope())
            {
                if (item == null) return;

                if (OnChanging(new ObservableCollectionChangingEventArgs<TItem>(ObservableCollectionChangeTypes.ItemsAdded, item)).CancelOperation)
                {
                    Logger.Assembly.Info(m => m("Change operation canceled by event handler. Item [{0}] not added to collection.", item));
                    return;
                }

                this._collection.Add(item);
                OnChanged(new ObservableCollectionChangedEventArgs<TItem>(ObservableCollectionChangeTypes.ItemsAdded, item));
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
        {
            using (Logger.Assembly.Scope())
            {
                if (this._collection.Count == 0) return;

                var items = new TItem[this._collection.Count];
                
                this._collection.CopyTo(items, 0);

                if (OnChanging(new ObservableCollectionChangingEventArgs<TItem>(ObservableCollectionChangeTypes.ItemsRemoved, items)).CancelOperation)
                {
                    Logger.Assembly.Info(m => m("Change operation canceled by event handler. Item [{0}] not removed from collection.", items));
                    return;
                }

                this._collection.Clear();
                OnChanged(new ObservableCollectionChangedEventArgs<TItem>(ObservableCollectionChangeTypes.ItemsRemoved, items));
            }
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        public bool Contains(TItem item)
        {
            using (Logger.Assembly.Scope())
            {
                return this._collection.Contains(item);
            }
        }

        /// <summary>
        /// Copies the automatic.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            using (Logger.Assembly.Scope())
            {
                this._collection.CopyTo(array, arrayIndex);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public int Count
        {
            get
            {
                using (Logger.Assembly.Scope())
                {
                    return this._collection.Count;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get
            {
                using (Logger.Assembly.Scope())
                {
                    return this._collection.IsReadOnly;
                }
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(TItem item)
        {
            using (Logger.Assembly.Scope())
            {
                if (item == null) return false;

                if (OnChanging(new ObservableCollectionChangingEventArgs<TItem>(ObservableCollectionChangeTypes.ItemsRemoved, item)).CancelOperation)
                {
                    Logger.Assembly.Info(m => m("Change operation canceled by event handler. Item [{0}] not removed from collection.", item));
                    return false;
                }

                var removedFlag = this._collection.Remove(item);
                
                if (removedFlag)
                {
                    OnChanged(new ObservableCollectionChangedEventArgs<TItem>(ObservableCollectionChangeTypes.ItemsRemoved, item));
                }

                return removedFlag;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            using (Logger.Assembly.Scope())
            {
                return this._collection.GetEnumerator();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            using (Logger.Assembly.Scope())
            {
                return this._collection.GetEnumerator();
            }
        }
    }
}
