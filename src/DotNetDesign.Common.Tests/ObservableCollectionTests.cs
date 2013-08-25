using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DotNetDesign.Common.Tests
{
    public class ObservableCollectionTests
    {
        [Fact]
        public void DefaultConstructorShouldCreateEmptyCollection()
        {
            var instance = new ObservableCollection<string>();
            instance.Should().BeEmpty();

            TestEvents(instance);
        }

        [Fact]
        public void InitialCapacityShouldBeSupported()
        {
            var instance = new ObservableCollection<string>(5);
            instance.Should().BeEmpty();

            TestEvents(instance);
        }

        [Fact]
        public void InitialCollectionValuesShouldBeSupported()
        {
            var instance = new ObservableCollection<string>(new string[] { "a", "b" });
            instance.Should().NotBeEmpty();
            instance.Count.Should().Be(2);

            TestEvents(instance);
        }

        private void TestEvents(IObservableCollection<string> instance)
        {
            var stringToAdd = "Adam is awesome";
            var addingCallCount = 0;
            var addedCallCount = 0;
            var removingCallCount = 0;
            var removedCallCount = 0;
            var clearingCallCount = 0;
            var clearedCallCount = 0;

            EventHandler<ObservableCollectionChangingEventArgs<string>> cancelAddHandler = (sender, eventArgs) =>
            {
                addingCallCount++;
                eventArgs.Items.Should().Contain(stringToAdd);
                eventArgs.Items.Count().Should().Be(1);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsAdded);
                eventArgs.CancelOperation.Should().BeFalse();
                eventArgs.CancelOperation = true;
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().NotContain(x => eventArgs.Items.Contains(x));
            };

            EventHandler<ObservableCollectionChangingEventArgs<string>> allowAddHandler = (sender, eventArgs) =>
            {
                addingCallCount++;
                eventArgs.Items.Should().Contain(stringToAdd);
                eventArgs.Items.Count().Should().Be(1);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsAdded);
                eventArgs.CancelOperation.Should().BeFalse();
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().NotContain(x => eventArgs.Items.Contains(x));
            };

            EventHandler<ObservableCollectionChangedEventArgs<string>> addChangedHandler = (sender, eventArgs) =>
            {
                addedCallCount++;
                eventArgs.Items.Should().Contain(stringToAdd);
                eventArgs.Items.Count().Should().Be(1);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsAdded);
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().Contain(x => eventArgs.Items.Contains(x));
            };


            EventHandler<ObservableCollectionChangingEventArgs<string>> cancelRemoveHandler = (sender, eventArgs) =>
            {
                removingCallCount++;
                eventArgs.Items.Should().Contain(stringToAdd);
                eventArgs.Items.Count().Should().Be(1);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsRemoved);
                eventArgs.CancelOperation.Should().BeFalse();
                eventArgs.CancelOperation = true;
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().Contain(x => eventArgs.Items.Contains(x));
            };

            EventHandler<ObservableCollectionChangingEventArgs<string>> allowRemoveHandler = (sender, eventArgs) =>
            {
                removingCallCount++;
                eventArgs.Items.Should().Contain(stringToAdd);
                eventArgs.Items.Count().Should().Be(1);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsRemoved);
                eventArgs.CancelOperation.Should().BeFalse();
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().Contain(x => eventArgs.Items.Contains(x));
            };

            EventHandler<ObservableCollectionChangedEventArgs<string>> removeChangedHandler = (sender, eventArgs) =>
            {
                removedCallCount++;
                eventArgs.Items.Should().Contain(stringToAdd);
                eventArgs.Items.Count().Should().Be(1);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsRemoved);
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().NotContain(x => eventArgs.Items.Contains(x));
            };


            EventHandler<ObservableCollectionChangingEventArgs<string>> cancelClearHandler = (sender, eventArgs) =>
            {
                clearingCallCount++;
                eventArgs.Items.Count().Should().Be(2);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsRemoved);
                eventArgs.CancelOperation.Should().BeFalse();
                eventArgs.CancelOperation = true;
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().Contain(x => eventArgs.Items.Contains(x));
            };

            EventHandler<ObservableCollectionChangingEventArgs<string>> allowClearHandler = (sender, eventArgs) =>
            {
                clearingCallCount++;
                eventArgs.Items.Count().Should().Be(2);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsRemoved);
                eventArgs.CancelOperation.Should().BeFalse();
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().Contain(x => eventArgs.Items.Contains(x));
            };

            EventHandler<ObservableCollectionChangedEventArgs<string>> clearChangedHandler = (sender, eventArgs) =>
            {
                clearedCallCount++;
                eventArgs.Items.Count().Should().Be(2);
                eventArgs.ChangeType.Should().Be(ObservableCollectionChangeTypes.ItemsRemoved);
                var collection = sender as IEnumerable<string>;
                collection.Should().NotBeNull();
                collection.Should().NotContain(x => eventArgs.Items.Contains(x));
            };

            // try to add with cancelAddHandler first, then with allowAddHandler, then remove with cancelRemoveHandler, then allowRemoveHandler

            instance.Clear();
            instance.Should().BeEmpty();
            instance.Changing += cancelAddHandler;
            instance.Changed += addChangedHandler;

            instance.Add(null);
            instance.Should().BeEmpty();
            addingCallCount.Should().Be(0);
            addedCallCount.Should().Be(0);

            instance.Add(stringToAdd);
            instance.Should().BeEmpty();
            addingCallCount.Should().Be(1);
            addedCallCount.Should().Be(0);

            instance.Changing -= cancelAddHandler;
            instance.Changing += allowAddHandler;

            instance.Add(stringToAdd);
            instance.Should().Contain(stringToAdd);
            instance.Count.Should().Be(1);
            addingCallCount.Should().Be(2);
            addedCallCount.Should().Be(1);

            instance.Changing -= allowAddHandler;
            instance.Changed -= addChangedHandler;
            instance.Changing += cancelRemoveHandler;
            instance.Changed += removeChangedHandler;

            instance.Remove(null);
            instance.Count.Should().Be(1);
            removingCallCount.Should().Be(0);
            removedCallCount.Should().Be(0);

            instance.Remove(stringToAdd);
            instance.Should().Contain(stringToAdd);
            instance.Count.Should().Be(1);
            removingCallCount.Should().Be(1);
            removedCallCount.Should().Be(0);

            instance.Changing -= cancelRemoveHandler;
            instance.Changing += allowRemoveHandler;
            instance.Remove(stringToAdd);
            instance.Should().BeEmpty();
            removingCallCount.Should().Be(2);
            removedCallCount.Should().Be(1);

            instance.Changing -= allowRemoveHandler;
            instance.Changed -= removeChangedHandler;

            instance.Changing += cancelClearHandler;
            instance.Changed += clearChangedHandler;
            instance.Clear();
            clearingCallCount.Should().Be(0);
            clearedCallCount.Should().Be(0);

            instance.Changing -= cancelClearHandler;
            instance.Changed -= clearChangedHandler;
            instance.Add("a");
            instance.Add("b");
            instance.Count.Should().Be(2);
            instance.Changing += cancelClearHandler;
            instance.Changed += clearChangedHandler;
            instance.Clear();
            instance.Count.Should().Be(2);
            clearingCallCount.Should().Be(1);
            clearedCallCount.Should().Be(0);
            instance.Changing -= cancelClearHandler;
            instance.Changing += allowClearHandler;
            instance.Clear();
            instance.Should().BeEmpty();
            clearingCallCount.Should().Be(2);
            clearedCallCount.Should().Be(1);
            instance.Changing -= allowClearHandler;
            instance.Changed -= clearChangedHandler;

        }
    }
}
