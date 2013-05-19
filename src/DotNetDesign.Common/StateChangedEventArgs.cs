using System;

namespace DotNetDesign.Common
{
	/// <summary>
	/// State Change Event Args
	/// </summary>
	public class StateChangeEventArgs<TState> : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StateChangeEventArgs&lt;TState&gt;"/> class.
		/// </summary>
		/// <param name="originalState">State of the original.</param>
		/// <param name="newState">The new state.</param>
		public StateChangeEventArgs(TState originalState, TState newState)
		{
			using (Logger.Assembly.Scope())
			{
				OriginalState = originalState;
				NewState = newState;
			}
		}

		/// <summary>
		/// Gets the state of the original.
		/// </summary>
		/// <value>
		/// The state of the original.
		/// </value>
		public TState OriginalState { get; private set; }

		/// <summary>
		/// Gets the new state.
		/// </summary>
		public TState NewState { get; private set; }
	}
}