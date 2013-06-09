using System;
using System.Linq.Expressions;
using DotNetDesign.Common;
using FluentAssertions;
using Xunit;

namespace DotNetDesign.Common.Tests
{
    public class MemberNameTests
    {
        [Fact]
        public void MemberNameForTests()
        {
            var person = new Person() { Name = "Adam" };

            Assert.Throws<ArgumentException>(() => MemberName.For(() => 123))
                  .Message.Should().Be("Invalid expression [123]");

            MemberName.For(person, p => p.Name).Should().Be("Name");
            MemberName.For(person, p => p.GetName()).Should().Be("GetName");

            Expression<Func<Person, string>> expressionForPersonName = p => p.Name;
            Expression<Func<Person, string>> expressionForPersonGetName = p => p.GetName();
            MemberName.For(expressionForPersonName).Should().Be("Name");
            MemberName.For(expressionForPersonGetName).Should().Be("GetName");

            Expression<Func<string>> expressionForName = () => person.Name;
            Expression<Func<string>> expressionForGetName = () => person.GetName();
            MemberName.For(expressionForName).Should().Be("Name");
            MemberName.For(expressionForGetName).Should().Be("GetName");

            Expression<Action> expressionActionForName = () => person.GetName();
            MemberName.For(expressionActionForName).Should().Be("GetName");

            Expression<Action<Person>> expressionActionGenericForName = p => p.GetName();
            MemberName.For(expressionActionGenericForName).Should().Be("GetName");
            MemberName.For(person, expressionActionGenericForName).Should().Be("GetName");
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string GetName()
        {
            return Name;
        }
    }
}