using NUnit.Framework;
using UnityEngine.TestTools;
using System;

public class StateMachineTests
{
    private StateMachine stateMachine;
    private State stateA;
    private State stateB;

    [SetUp]
    public void SetUp()
    {
        stateMachine = new StateMachine();
        stateA = new ConcreteState(stateMachine);
        stateB = new ConcreteState(stateMachine);
        stateMachine.AddState(stateA, "StateA");
        stateMachine.AddState(stateB, "StateB");
    }

    [Test]
    public void TestAddState()
    {
        Assert.AreEqual(stateA, stateMachine.GetState("StateA"));
        Assert.AreEqual(stateB, stateMachine.GetState("StateB"));
    }

    [Test]
    public void TestSetState()
    {
        stateMachine.SetState("StateA");
        Assert.AreEqual(stateA, stateMachine.CurrentState);

        stateMachine.SetState("StateB");
        Assert.AreEqual(stateB, stateMachine.CurrentState);
    }

    [Test]
    public void TestExecuteState()
    {
        bool executed = false;
        stateA.OnEnter += () => executed = true;
        stateMachine.SetState("StateA");
        stateMachine.ExecuteState();
        Assert.IsTrue(executed);
    }

    [Test]
    public void TestAddTransition()
    {
        stateMachine.AddTransition("StateA", "StateB", () => true);
        stateMachine.SetState("StateA");
        stateMachine.ExecuteState();
        Assert.AreEqual(stateB, stateMachine.CurrentState);
    }
    [Test]
    public void TestSetNonExistentState()
    {
        LogAssert.Expect(UnityEngine.LogType.Error, "State not found - name: NonExistentState");
        stateMachine.SetState("NonExistentState");
        Assert.IsNull(stateMachine.CurrentState);
    }

    [Test]
    public void TestAddTransitionWithNonExistentStates()
    {
        LogAssert.Expect(UnityEngine.LogType.Error, "State not found - name: NonExistentStateA");
        stateMachine.AddTransition("NonExistentStateA", "NonExistentStateB", () => true);
    }



    [Test]
    public void TestAddStateWithDuplicateName()
    {
        LogAssert.Expect(UnityEngine.LogType.Error, "State with the name: StateA already exists. Cannot add duplicate state.");
        stateMachine.AddState(new ConcreteState(stateMachine), "StateA"); // Duplicate name
        Assert.AreEqual(stateA, stateMachine.GetState("StateA")); // Should remain the original state
    }


    [Test]
    public void TestExecuteStateWithNoCurrentState()
    {
        LogAssert.Expect(UnityEngine.LogType.Error, "Current state is null");
        stateMachine.ExecuteState(); // No state has been set
    }

    // Concrete state class for testing
    private class ConcreteState : State
    {
        public ConcreteState(StateMachine parent) : base(parent) { }
    }
}
