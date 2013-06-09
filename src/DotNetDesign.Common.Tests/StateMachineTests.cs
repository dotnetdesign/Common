using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace DotNetDesign.Common.Tests
{
	public class StateMachineTests
	{
		private readonly StateMachine<int> _stateMachine;

        public StateMachineTests()
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

	    [Fact]
	    public void ValidStateMachine_InvalidChange_ShouldThrowException()
	    {
	        var ex = Assert.Throws<InvalidStateException<int>>(() => _stateMachine.ChangeState(3));

	        ex.CurrentState.Should().Be(0);
	        ex.TargetState.Should().Be(3);
	        ex.AllowedStates.Count().Should().Be(1);
	        ex.AllowedStates.First().Should().Be(1);
	    }

	    [Fact]
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

            _stateMachine.CurrentState.Should().Be(1);
            _stateMachine.GetValidNextStates().Count().Should().Be(1);
            _stateMachine.GetValidNextStates().First().Should().Be(2);
            numberOfTimesChangingWasCalled.Should().Be(1);
            numberOfTimesChangedWasCalled.Should().Be(1);
            changingOriginalValue.Should().Be(0);
            changingNewValue.Should().Be(1);
            changedOriginalValue.Should().Be(0);
            changedNewValue.Should().Be(1);
		}
	}
}