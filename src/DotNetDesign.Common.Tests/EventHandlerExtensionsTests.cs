using System;
using FluentAssertions;
using Xunit;

namespace DotNetDesign.Common.Tests
{
    public class EventHandlerExtensionsTests
    {
        public static void StaticEventHandler(object sender, EventArgs args)
        {
            
        }

        public EventHandlerExtensionsTests()
        {
            
        }

        [Fact]
        public void NullEventHandlerShouldThrowArgumentNullException()
        {
            EventHandler<EventArgs> nullEventHandler = null;
            Assert.Throws<ArgumentNullException>(() => nullEventHandler.MakeWeak(e => { }))
                .Message.Should().Contain("eventHandler");
        }

        [Fact]
        public void EventHandlerWithNullTargetShouldThrowArgumentException()
        {
            EventHandler<EventArgs> nullEventHandler = null;
            Assert.Throws<ArgumentNullException>(() => nullEventHandler.MakeWeak(e => { }))
                .Message.Should().Contain("eventHandler");
        }
    }
}