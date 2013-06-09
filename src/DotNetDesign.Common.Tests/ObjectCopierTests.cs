using System;
using FluentAssertions;
using Xunit;

namespace DotNetDesign.Common.Tests
{
    public class ObjectCopierTests
    {
        private readonly ObjectToClone _objectToClone;

        public ObjectCopierTests()
        {
            _objectToClone = new ObjectToClone() {Property = "PropertyValue"};
        }

        [Fact]
        public void NonSerializableObjectShouldThrowArgumentException()
        {
            var nonSerializableObject = (IObjectToClone) _objectToClone;
            Assert.Throws<ArgumentException>(() => ObjectCopier.Clone(nonSerializableObject))
                  .Message.Should().Be("The type must be serializable.\r\nParameter name: source");
        }

        [Fact]
        public void SerializableObjectShouldBeClonedAndEqualButNotSame()
        {
            var clone = ObjectCopier.Clone(_objectToClone);

            clone.Should().Be(_objectToClone);
            clone.Should().NotBeSameAs(_objectToClone);
        }

        [Fact]
        public void NullObjectShouldJustReturnNull()
        {
            ObjectToClone objectToClone = null;
            var clone = ObjectCopier.Clone(objectToClone);

            clone.Should().BeNull();
        }
    }

    [Serializable]
    public class ObjectToClone : IObjectToClone
    {
        public string Property { get; set; }

        public override string ToString()
        {
            return string.Format("{0}::{1}", GetType(), Property);
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }
    }

    public interface IObjectToClone
    {
        string Property { get; set; }
    }
}