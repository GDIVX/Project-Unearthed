using Assets.Scripts.AI;
using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TestTools;

public class UtilityAgentTests : MonoBehaviour
{
    private UtilityAgent _agent;
    private GameObject _agentObject;
    NeedMock need;
    ActionMock action1;
    ActionMock action2;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        _agentObject = new GameObject();
        _agent = _agentObject.AddComponent<UtilityAgent>();

        _agent.Needs = new();
        _agent.Actions = new();

        need = NeedMock.Create(0.6f);
        action1 = ActionMock.Create(need, 0.3f,gameObject);
        action2 = ActionMock.Create(need, 0.7f, gameObject);

        _agent.Needs.Add(need);
        _agent.Actions.Add(action1);
        _agent.Actions.Add(action2);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(_agentObject);
        yield return null;
    }

    [UnityTest]
    public IEnumerator ExecuteAction_ExecutesActionWithHighestUtilityScore()
    {



        // Act
        yield return new WaitForSeconds(0.1f);

        // Assert
        Assert.IsTrue(action2.isExecuted);
        Assert.IsFalse(action1.isExecuted);

        yield return null;
    }

    [UnityTest]
    public IEnumerator Agent_CanFunctionWithMultipleNeedsAndActions()
    {
        var need1 = NeedMock.Create(0.4f);
        var action1 = ActionMock.Create(need1, 0.3f, gameObject);
        var need2 = NeedMock.Create(0.6f);
        var action2 = ActionMock.Create(need2, 0.1f, gameObject);

        _agent.Needs.Add(need1);
        _agent.Actions.Add(action1);
        _agent.Needs.Add(need2);
        _agent.Actions.Add(action2);

        yield return new WaitForSeconds(0.1f);

        // Check that the agent selected and executed the correct action
        Assert.IsTrue(action2.isExecuted);
        Assert.IsFalse(action1.isExecuted);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ExecuteAction_ExecutesActionWhenNeedUtilityScoresAreEqual()
    {
        var need1 = NeedMock.Create(0.6f);
        var action1 = ActionMock.Create(need1, 0.3f, gameObject);
        var need2 = NeedMock.Create(0.6f);
        var action2 = ActionMock.Create(need2, 0.1f, gameObject);

        _agent.Needs.Add(need1);
        _agent.Actions.Add(action1);
        _agent.Needs.Add(need2);
        _agent.Actions.Add(action2);

        yield return new WaitForSeconds(0.1f);

        // Check that the agent selected and executed the correct action
        Assert.IsTrue(action2.isExecuted);
        Assert.IsFalse(action1.isExecuted);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ExecuteAction_ExecutesActionWhenUtilityScoresAreEqual()
    {
        var need = NeedMock.Create(0.6f);
        var action1 = ActionMock.Create(need, 0.5f, gameObject);
        var action2 = ActionMock.Create(need, 0.5f, gameObject);

        _agent.Needs.Add(need);
        _agent.Actions.Add(action1);
        _agent.Actions.Add(action2);

        yield return new WaitForSeconds(0.1f);

        // Assert that at least one action is executed
        Assert.IsTrue(action2.isExecuted);
        Assert.IsFalse(action1.isExecuted);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ExecuteAction_DoesNotExecuteActionWhenNoNeeds()
    {
        var need = NeedMock.Create(0.6f);
        var action = ActionMock.Create(need, 0.5f, gameObject);

        _agent.Actions.Add(action);

        yield return new WaitForSeconds(0.1f);

        Assert.IsFalse(action.isExecuted);

        yield return null;
    }

    [UnityTest]
    public IEnumerator ExecuteAction_DoesNotExecuteActionWhenDeletingNeeds()
    {
        var need = NeedMock.Create(0.6f);
        var action = ActionMock.Create(need, 0.5f, gameObject);

        _agent.Needs.Remove(need);
        _agent.Actions.Add(action);

        yield return new WaitForSeconds(0.1f);

        Assert.IsFalse(action.isExecuted);

        yield return null;
    }





    

    public class NeedMock : Need
    {
        public float utilityScore = 0;

        public static NeedMock Create(float UtilityScore)
        {
            NeedMock mock = ScriptableObject.CreateInstance<NeedMock>();
            mock.utilityScore = UtilityScore;
            return mock;
        }

    }
}
