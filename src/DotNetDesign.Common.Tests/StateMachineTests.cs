using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetDesign.Common.Tests
{
	[TestClass]
	public class StateMachineTests
	{
		private StateMachine<int> _stateMachine;

		[TestInitialize]
		public void Setup()
		{
			_stateMachine =
				new StateMachine<int>(new Dictionary<int, IEnumerable<int>>
                                          {
                                              {0, new[] {1}},
                                              {1, new[] {2}},
                                              {2, new[] {3}},
                                              {3, new[] {5}}
                                          });
		}

		[TestMethod]
		public void ValidStateMachine_InvalidChange_ShouldThrowException()
		{
			TestHelpers.TestException<InvalidStateException<int>>(
				() => _stateMachine.ChangeState(3),
				ex =>
					{
						Assert.AreEqual(0, ex.CurrentState);
						Assert.AreEqual(3, ex.TargetState);
						Assert.AreEqual(1, ex.AllowedStates.Count());
						Assert.AreEqual(1, ex.AllowedStates.First());
					}
				);
		}

		[TestMethod]
		public void ValidStateMachine_ValidChange_ShouldTriggerChangingAndChangedEvents()
		{
			var numberOfTimesChangingWasCalled = 0;
			var numberOfTimesChangedWasCalled = 0;
			var changingOriginalValue = -1;
			var changedOriginalValue = -1;
			var changingNewValue = -1;
			var changedNewValue = -1;

			_stateMachine.StateChanging += delegate(object sender, StateChangeEventArgs<int> e)
			{
				numberOfTimesChangingWasCalled++;
				changingOriginalValue = e.OriginalState;
				changingNewValue = e.NewState;
			};
			_stateMachine.StateChanged += delegate(object sender, StateChangeEventArgs<int> e)
			{
				numberOfTimesChangedWasCalled++;
				changedOriginalValue = e.OriginalState;
				changedNewValue = e.NewState;
			};

			_stateMachine.ChangeState(1);
			Assert.AreEqual(1, _stateMachine.CurrentState);
			Assert.AreEqual(1, _stateMachine.GetValidNextStates().Count());
			Assert.AreEqual(2, _stateMachine.GetValidNextStates().First());
			Assert.AreEqual(1, numberOfTimesChangingWasCalled);
			Assert.AreEqual(1, numberOfTimesChangedWasCalled);
			Assert.AreEqual(0, changingOriginalValue);
			Assert.AreEqual(1, changingNewValue);
			Assert.AreEqual(0, changedOriginalValue);
			Assert.AreEqual(1, changedNewValue);
		}
	}
}