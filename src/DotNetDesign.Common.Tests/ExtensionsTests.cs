using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using System;

namespace DotNetDesign.Common.Tests
{
    public class ExtensionsTests
    {
         [Fact]
         public void NullEnumerablePassedToRemoveNullsShouldThrownArgumentNullException()
         {
             List<object> nullList = null;
             Assert.Throws<ArgumentNullException>(() => nullList.RemoveNulls())
                 .Message.Should().Contain("values");
         }

        [Fact]
        public void RemoveNullsShouldRemoveNulls()
        {
            var listWithNulls = new List<object>() {1, 2, null, 3, 4, null, 5};
            var listWithoutNulls = new List<object>() {1, 2, 3, 4, 5};
            var lengthOfListWithNulls = listWithNulls.Count;
            var numberOfNullsInListWithNulls = listWithNulls.Count(x => x == null);
            var numberOfNonNullsInListWithNulls = lengthOfListWithNulls - numberOfNullsInListWithNulls;
            var lengthOfListWithoutNulls = listWithoutNulls.Count;
            var numberOfNullsInListWithoutNulls = listWithoutNulls.Count(x => x == null);
            var numberOfNonNullsInListWithoutNulls = lengthOfListWithoutNulls - numberOfNullsInListWithoutNulls;

            var listWithNullsAfterNullsRemoved = listWithNulls.RemoveNulls();
            listWithNullsAfterNullsRemoved.Count().Should().Be(numberOfNonNullsInListWithNulls);

            var listWithoutNullsAfterNullsRemoved = listWithoutNulls.RemoveNulls();
            listWithoutNullsAfterNullsRemoved.Count().Should().Be(numberOfNonNullsInListWithoutNulls);
        }
    }
}