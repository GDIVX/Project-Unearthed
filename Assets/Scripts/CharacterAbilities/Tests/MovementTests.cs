using Assets.Scripts.CharacterAbilities;
using CharacterAbilities;
using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovementTests
    {
        private Movement movementScript;
        private FakeController fakeController;
        private GameObject gameObject;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();

            // Create a fake controller and inject it into the Movement script
            fakeController = new FakeController();
            movementScript = gameObject.AddComponent<Movement>();

            // Set initial values
            movementScript.MovementSpeed = 8f;
            movementScript.AccelerationScalar = 0.5f;
            movementScript.AccelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            movementScript.DecelerationScalar = 0.1f;
            movementScript.DecelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            movementScript.MaxVelocity = 10f;
        }

        [TearDown]
        public void Teardown()
        {
            GameObject.DestroyImmediate(gameObject);
        }

        [UnityTest]
        public IEnumerator Movement_ReceivedInput_PositionChanged()
        {
            // Set up the initial values
            movementScript.MovementSpeed = 5f;
            movementScript.AccelerationScalar = 1f;
            movementScript.AccelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            movementScript.MaxVelocity = 10f;

            // Set the input vector to simulate movement
            fakeController.MovementVector = new Vector2(1f, 0f);

            // Calculate the time it would take to move a distance of 1 unit
            float distance = 1f;
            float timeToMove = distance / movementScript.MovementSpeed;

            Vector3 startPosition = movementScript.transform.localPosition;

            yield return new WaitForSeconds(timeToMove);

            // Assert that the position has changed
            Vector3 currentPosition = movementScript.transform.localPosition;
            Assert.AreNotEqual(startPosition, currentPosition);
        }

        [UnityTest]
        public IEnumerator Movement_StopsWhenNoInputReceived()
        {
            // Set up the initial values
            movementScript.MovementSpeed = 5f;
            movementScript.AccelerationScalar = 1f;
            movementScript.AccelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            movementScript.MaxVelocity = 10f;

            // Set the input vector to simulate no movement
            fakeController.MovementVector = new Vector2(0f, 0f);

            // Calculate the time it would take to move a distance of 1 unit
            float distance = 1f;
            float timeToMove = distance / movementScript.MovementSpeed;

            Vector3 startPosition = movementScript.transform.localPosition;

            yield return new WaitForSeconds(timeToMove);

            // Assert that the position remains the same
            Vector3 currentPosition = movementScript.transform.localPosition;
            Assert.AreEqual(startPosition, currentPosition);
        }


        [UnityTest]
        public IEnumerator Movement_MaxVelocityReached()
        {
            // Set up the initial values
            movementScript.MovementSpeed = 5f;
            movementScript.AccelerationScalar = 1f;
            movementScript.AccelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            movementScript.MaxVelocity = 10f;

            // Set the input vector to simulate movement
            fakeController.MovementVector = new Vector2(1f, 0f);

            // Calculate the time it would take to reach the maximum velocity
            float timeToMaxVelocity = movementScript.MaxVelocity / movementScript.MovementSpeed;

            yield return new WaitForSeconds(timeToMaxVelocity);

            // Assert that the speed does not exceed the maximum velocity
            Assert.LessOrEqual(movementScript.CurrentSpeed, movementScript.MaxVelocity);
        }
    }

    // Fake implementation of the IController interface for testing
    public class FakeController : IMovementInput
    {
        private Vector2 movementVector;

        public GameObject GameObject { get; set; }
        public Vector2 MovementVector { get => movementVector; set => movementVector = value; }

        public event Action onFire;
        public event Action onReload;
        public event Action OnDodge;

        public Vector3 GetAimPoint()
        {
            return Vector3.zero;
        }

        public Vector2 GetMovementVector()
        {
            return MovementVector;
        }
    }
}
