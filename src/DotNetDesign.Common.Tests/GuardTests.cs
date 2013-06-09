using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace DotNetDesign.Common.Tests
{
    public class GuardTests
    {
        [Fact]
        public void ArgumentNotNullGuardTests()
        {
            string nullString = null;
            const string nullStringName = "nullString";
            const string notNullString = "not null";
            const string notNullStringName = "notNullStringName";
            IList<int> nullObject = null;
            const string nullObjectName = "nullObjectName";

            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull(nullString, nullStringName))
                  .Message.Should().Contain(nullStringName);
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull(nullObject, nullObjectName))
                  .Message.Should().Contain(nullObjectName);

            Guard.ArgumentNotNull(nullString, nullStringName, false).Should().BeFalse();
            Guard.ArgumentNotNull(nullObject, nullObjectName, false).Should().BeFalse();

            Guard.ArgumentNotNull(notNullString, notNullStringName).Should().BeTrue();
            Guard.ArgumentNotNull(notNullString, notNullStringName, false).Should().BeTrue();
        }

        [Fact]
        public void ArgumentNotNullOrEmptyTests()
        {
            var emptyString = string.Empty;
            const string nullStringName = "nullString";
            const string emptyStringName = "emptyString";
            const string spacesString = "  ";
            const string spacesStringName = "spacesString";
            const string tabsString = "\t\t";
            const string tabsStringName = "tabsString";
            const string whitespaceString = "\t\t  \r\n\t  \t  \r\n";
            const string whitespaceStringName = "whitespaceString";
            const string notNullString = "not null";
            const string notNullStringName = "notNullStringName";

            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(null, nullStringName))
                  .Message.Should().Contain(nullStringName);
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(null, nullStringName, true))
                  .Message.Should().Contain(nullStringName);
            Guard.ArgumentNotNullOrEmpty(null, nullStringName, false, false).Should().BeFalse();
            Guard.ArgumentNotNullOrEmpty(null, nullStringName, true, false).Should().BeFalse();

            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(emptyString, emptyStringName))
                  .Message.Should().Contain(emptyStringName);
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(emptyString, emptyStringName, true))
                  .Message.Should().Contain(emptyStringName);

            Guard.ArgumentNotNullOrEmpty(emptyString, emptyStringName, false, false).Should().BeFalse();
            Guard.ArgumentNotNullOrEmpty(emptyString, emptyStringName, true, false).Should().BeFalse();

            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(spacesString, spacesStringName))
                  .Message.Should().Contain(spacesStringName);
            Guard.ArgumentNotNullOrEmpty(spacesString, spacesStringName, true).Should().BeTrue();
            Guard.ArgumentNotNullOrEmpty(spacesString, spacesStringName, false, false).Should().BeFalse();
            Guard.ArgumentNotNullOrEmpty(spacesString, spacesStringName, true, false).Should().BeTrue();

            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(tabsString, tabsStringName))
                  .Message.Should().Contain(tabsStringName);
            Guard.ArgumentNotNullOrEmpty(tabsString, tabsStringName, true).Should().BeTrue();
            Guard.ArgumentNotNullOrEmpty(tabsString, tabsStringName, false, false).Should().BeFalse();
            Guard.ArgumentNotNullOrEmpty(tabsString, tabsStringName, true, false).Should().BeTrue();

            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(whitespaceString, whitespaceStringName))
                  .Message.Should().Contain(whitespaceStringName);
            Guard.ArgumentNotNullOrEmpty(whitespaceString, whitespaceStringName, true).Should().BeTrue();
            Guard.ArgumentNotNullOrEmpty(whitespaceString, whitespaceStringName, false, false).Should().BeFalse();
            Guard.ArgumentNotNullOrEmpty(whitespaceString, whitespaceStringName, true, false).Should().BeTrue();
            Guard.ArgumentNotNullOrEmpty(notNullString, notNullStringName).Should().BeTrue();
            Guard.ArgumentNotNullOrEmpty(notNullString, notNullStringName, true).Should().BeTrue();
            Guard.ArgumentNotNullOrEmpty(notNullString, notNullStringName, false, false).Should().BeTrue();
            Guard.ArgumentNotNullOrEmpty(notNullString, notNullStringName, true, false).Should().BeTrue();
        }
    }
}