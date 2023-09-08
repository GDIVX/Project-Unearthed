using Assets.Scripts.CharacterAbilities;
using CharacterAbilities;
using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementStateMachineTests
{
    private MovementStateMachine movementStateMachine;
    private Movement movement;
    private DodgeMovement dodgeMovement;
    private TestMovementInput movementInput;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject with the MovementStateMachine component
        GameObject gameObject = new GameObject();

        // Add required components
        movement = gameObject.AddComponent<Movement>();
        dodgeMovement = gameObject.AddComponent<DodgeMovement>();

        //Set movement initial Values
        movement.MovementSpeed = 8f;
        movement.AccelerationScalar = 0.5f;
        movement.AccelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        movement.DecelerationScalar = 0.1f;
        movement.DecelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        movement.MaxVelocity = 10f;

        // Set Dodge initial values
        dodgeMovement.DodgeSpeed = 10f;
        dodgeMovement.DodgeDuration = 0.5f;
        dodgeMovement.DodgeCooldown = 2f;
        dodgeMovement.InvisibilityDuration = 1f;
        dodgeMovement.DamageHandler = gameObject.AddComponent<DamageHandler>(); // Assuming Health component is required

        //Set Dodge initial values


        // Create a mock or stub implementation of IMovementInput
        movementInput = gameObject.AddComponent<TestMovementInput>();

        movementStateMachine = gameObject.AddComponent<MovementStateMachine>();
    }

    [UnityTest]
    public IEnumerator TestTransitionToDodgeState()
    {
        // Arrange
        movementInput.MovementInput = new Vector2(1, 0);

        // Act
        movementInput.SimulateDodgeInput();

        // Wait for one frame to allow the Update method to be called
        yield return null;

        // Assert
        Assert.AreEqual("Dodge", movementStateMachine.StateMachine.CurrentStateName);
    }

    [UnityTest]
    public IEnumerator TestTransitionBackToMoveState()
    {
        // Arrange
        movementInput.MovementInput = new Vector2(1, 0);
        movementInput.SimulateDodgeInput();
        yield return null; // Wait for transition to Dodge state

        // Act
        // Simulate the conditions that would cause the dodge to end by advancing the game's time
        float dodgeDuration = movementStateMachine.DodgeMovement.DodgeDuration;
        yield return new WaitForSeconds(dodgeDuration + 0.1f); // Wait for dodge duration plus a small buffer

        // Assert
        Assert.AreEqual("Move", movementStateMachine.StateMachine.CurrentStateName);
    }

    [UnityTest]
    public IEnumerator TestMovementStateExecution()
    {
        // Arrange
        movementInput.MovementInput = new Vector2(1, 0);
        Vector3 initialPosition = movementStateMachine.Movement.transform.position;

        // Act
        yield return null; // Wait for one frame to allow the Update method to be called

        // Assert
        Assert.AreNotEqual(initialPosition, movementStateMachine.Movement.transform.position);
    }

    [UnityTest]
    public IEnumerator TestNoTransitionWhenCantDodge()
    {
        // Arrange
        movementStateMachine.DodgeMovement.DodgeCooldown = float.MaxValue; // Set a high cooldown to prevent dodging
        movementInput.MovementInput = new Vector2(1, 0);

        // Act
        movementInput.SimulateDodgeInput();
        yield return null; // Wait for one frame
        Assert.AreEqual("Dodge", movementStateMachine.StateMachine.CurrentStateName);


        //try to dodge again
        movementInput.SimulateDodgeInput();
        yield return null; // Wait for one frame


        // Assert
        Assert.AreEqual("Move", movementStateMachine.StateMachine.CurrentStateName);
    }



    private class TestMovementInput : MonoBehaviour, IMovementInput
    {
        public GameObject GameObject { get; set; }

        public event Action OnDodge;

        public Vector2 MovementInput { get; set; }

        public void SimulateDodgeInput()
        {
            OnDodge?.Invoke();
        }

        public Vector2 GetMovementVector()
        {
            return MovementInput;
        }
    }
}
