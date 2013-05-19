using System;

namespace DotNetDesign.Common
{
	/// <summary>
	/// Event Handler Extension Methods
	/// </summary>
	public static class EventHandlerExtensions
	{
		/// <summary>
		/// Makes the weak.
		/// </summary>
		/// <typeparam name="TEventArgs">The type of the event args.</typeparam>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="unregister">The unregister.</param>
		/// <returns></returns>
		public static EventHandler<TEventArgs> MakeWeak<TEventArgs>(this EventHandler<TEventArgs> eventHandler, UnregisterCallback<TEventArgs> unregister)
			where TEventArgs : EventArgs
		{
			using (Logger.Assembly.Scope())
			{
				Guard.ArgumentNotNull(eventHandler, "eventHandler");

				if (eventHandler.Method.IsStatic || eventHandler.Target == null)
				{
					throw new ArgumentException("Only instance methods are supported.", "eventHandler");
				}

				var weakEventType = typeof (WeakEventHandler<,>).MakeGenericType(eventHandler.Method.DeclaringType, typeof (TEventArgs));

				var weakEventHandlerConstructor = weakEventType.GetConstructor(
					new[]
						{
							typeof (EventHandler<TEventArgs>),
							typeof (UnregisterCallback<TEventArgs>)
						});

				var weakEventHandler =
					(IWeakEventHandler<TEventArgs>) weakEventHandlerConstructor.Invoke(new object[] {eventHandler, unregister});

				return weakEventHandler.Handler;
			}
		}
	}
}