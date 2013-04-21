using System;
using System.Linq.Expressions;

namespace DotNetDesign.Common
{
	/// <summary>
	/// Static methods to help return the string representation of a type's member.
	/// </summary>
	public static class MemberName
	{
		/// <summary>
		/// Get's the property name
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static string For<T>(T instance, Expression<Action<T>> expression)
		{
			using (Logger.Assembly.Scope())
			{
				Guard.ArgumentNotNull(expression, "expression");
				return FindMemberName(expression.Body);
			}
		}

		/// <summary>
		/// Get's the property name
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static string For<T>(Expression<Action<T>> expression)
		{
			using (Logger.Assembly.Scope())
			{
				Guard.ArgumentNotNull(expression, "expression");
				return FindMemberName(expression.Body);
			}
		}

		/// <summary>
		/// Get's the property name
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static string For(Expression<Action> expression)
		{
			using (Logger.Assembly.Scope())
			{
				Guard.ArgumentNotNull(expression, "expression");
				return FindMemberName(expression.Body);
			}
		}

		/// <summary>
		/// Get's the property name
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static string For<T, TResult>(T instance, Expression<Func<T, TResult>> expression)
		{
			using (Logger.Assembly.Scope())
			{
				Guard.ArgumentNotNull(expression, "expression");
				return FindMemberName(expression.Body);
			}
		}

		/// <summary>
		/// Get's the property name
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static string For<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			using (Logger.Assembly.Scope())
			{
				Guard.ArgumentNotNull(expression, "expression");
				return FindMemberName(expression.Body);
			}
		}

		/// <summary>
		/// Get's the property name
		/// </summary>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static string For<TResult>(Expression<Func<TResult>> expression)
		{
			using (Logger.Assembly.Scope())
			{
				Guard.ArgumentNotNull(expression, "expression");
				return FindMemberName(expression.Body);
			}
		}

		private static string FindMemberName(Expression expression)
		{
			using (Logger.Assembly.Scope())
			{
				Guard.ArgumentNotNull(expression, "expression");

				if (expression is MethodCallExpression)
				{
					return (expression as MethodCallExpression).Method.Name;
				}

				if (expression is MemberExpression)
				{
					return (expression as MemberExpression).Member.Name;
				}

				var invalidExpression = new ArgumentException("Invalid expression [" + expression + "]");
				Logger.Assembly.Error(m => m(invalidExpression.Message), invalidExpression);

				throw invalidExpression;
			}
		}
	}
}