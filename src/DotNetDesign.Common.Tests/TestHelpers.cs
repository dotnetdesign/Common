using System;

namespace DotNetDesign.Common.Tests
{
	public static class TestHelpers
	{
		public static void TestException<TException>(Action tryAction, Action<TException> catchAsserts)
			where TException : Exception
		{
			try
			{
				tryAction();
			}
			catch (TException ex)
			{
				catchAsserts(ex);
			}
		}

		public static void TestException<TException>(Action tryAction, string messageShouldContain)
			where TException : Exception
		{
			TestException<TException>(tryAction, ex => ex.Message.Contains(messageShouldContain));
		}
	}
}