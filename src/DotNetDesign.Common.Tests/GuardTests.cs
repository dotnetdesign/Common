using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetDesign.Common.Tests
{
	[TestClass]
	public class GuardTests
	{
		[TestMethod]
		public void ArgumentNotNullGuardTests()
		{
			string nullString = null;
			const string nullStringName = "nullString";
			const string notNullString = "not null";
			const string notNullStringName = "notNullStringName";
			IList<int> nullObject = null;
			const string nullObjectName = "nullObjectName";

			TestHelpers.TestException<ArgumentNullException>(() => Guard.ArgumentNotNull(nullString, nullStringName), nullStringName);
			TestHelpers.TestException<ArgumentNullException>(() => Guard.ArgumentNotNull(nullObject, nullObjectName), nullObjectName);

			Assert.IsFalse(Guard.ArgumentNotNull(nullString, nullStringName, false));
			Assert.IsFalse(Guard.ArgumentNotNull(nullObject, nullObjectName, false));

			Assert.IsTrue(Guard.ArgumentNotNull(notNullString, notNullStringName));
			Assert.IsTrue(Guard.ArgumentNotNull(notNullString, notNullStringName, false));
		}

		[TestMethod]
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

			TestHelpers.TestException<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(null, nullStringName), nullStringName);
			TestHelpers.TestException<ArgumentNullException>(
				() => Guard.ArgumentNotNullOrEmpty(null, nullStringName, true),
				nullStringName);
			Assert.IsFalse(Guard.ArgumentNotNullOrEmpty(null, nullStringName, false, false));
			Assert.IsFalse(Guard.ArgumentNotNullOrEmpty(null, nullStringName, true, false));

			TestHelpers.TestException<ArgumentNullException>(
				() => Guard.ArgumentNotNullOrEmpty(emptyString, emptyStringName),
				emptyStringName);

			TestHelpers.TestException<ArgumentNullException>(
				() => Guard.ArgumentNotNullOrEmpty(emptyString, emptyStringName, true),
				emptyStringName);

			Assert.IsFalse(Guard.ArgumentNotNullOrEmpty(emptyString, emptyStringName, false, false));
			Assert.IsFalse(Guard.ArgumentNotNullOrEmpty(emptyString, emptyStringName, true, false));

			TestHelpers.TestException<ArgumentNullException>(
				() => Guard.ArgumentNotNullOrEmpty(spacesString, spacesStringName),
				spacesStringName);
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(spacesString, spacesStringName, true));
			Assert.IsFalse(Guard.ArgumentNotNullOrEmpty(spacesString, spacesStringName, false, false));
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(spacesString, spacesStringName, true, false));

			TestHelpers.TestException<ArgumentNullException>(
				() => Guard.ArgumentNotNullOrEmpty(tabsString, tabsStringName),
				tabsStringName);
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(tabsString, tabsStringName, true));
			Assert.IsFalse(Guard.ArgumentNotNullOrEmpty(tabsString, tabsStringName, false, false));
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(tabsString, tabsStringName, true, false));

			TestHelpers.TestException<ArgumentNullException>(
				() => Guard.ArgumentNotNullOrEmpty(whitespaceString, whitespaceStringName),
				whitespaceStringName);
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(whitespaceString, whitespaceStringName, true));
			Assert.IsFalse(Guard.ArgumentNotNullOrEmpty(whitespaceString, whitespaceStringName, false, false));
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(whitespaceString, whitespaceStringName, true, false));
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(notNullString, notNullStringName));
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(notNullString, notNullStringName, true));
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(notNullString, notNullStringName, false, false));
			Assert.IsTrue(Guard.ArgumentNotNullOrEmpty(notNullString, notNullStringName, true, false));
		}
	}
}