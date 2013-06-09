using System;
using Xunit;
using FluentAssertions;

namespace DotNetDesign.Common.Tests
{
    public class WeakEventHandlerTests
    {
        [Fact]
        public void PassingNullEventHandlerToConstructorShouldTriggerArgumentNullException()
        {
            EventHandler<EventArgs> nullEventHandler = null;
            Assert.Throws<ArgumentNullException>(() => new WeakEventHandler<object, EventArgs>(nullEventHandler, null))
                .Message.Should().Contain("eventHandler");
        }

        [Fact]
        public void PassingNullUnregisterEventHandlerToConstructorShouldTriggerArgumentNullException()
        {
            EventHandler<EventArgs> eventHandler = (sender, args) => { };
            Assert.Throws<ArgumentNullException>(() => new WeakEventHandler<object, EventArgs>(eventHandler, null))
                .Message.Should().Contain("unregister");
        }
    }
}