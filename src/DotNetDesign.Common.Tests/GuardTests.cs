using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;

namespace DotNetDesign.Common.Tests
{
    public class GuardTests
    {
        [Fact]
        public void ArgumentNotNullGuardTests()
        {
            string nullString = null;
            string nullStringName = "nullString";
            string notNullString = "not null";
            string notNullStringName = "notNullStringName";
            IList<int> nullObject = null;
            string nullObjectName = "nullObjectName";

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
            string nullString = null;
            string nullStringName = "nullString";
            string emptyString = string.Empty;
            string emptyStringName = "emptyString";
            string spacesString = "  ";
            string spacesStringName = "spacesString";
            string tabsString = "\t\t";
            string tabsStringName = "tabsString";
            string whitespaceString = "\t\t  \r\n\t  \t  \r\n";
            string whitespaceStringName = "whitespaceString";
            string notNullString = "not null";
            string notNullStringName = "notNullStringName";

            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(nullString, nullStringName))
                  .Message.Should().Contain(nullStringName);
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(nullString, nullStringName, true))
                  .Message.Should().Contain(nullStringName);
            Guard.ArgumentNotNullOrEmpty(nullString, nullStringName, false, false).Should().BeFalse();
            Guard.ArgumentNotNullOrEmpty(nullString, nullStringName, true, false).Should().BeFalse();

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
