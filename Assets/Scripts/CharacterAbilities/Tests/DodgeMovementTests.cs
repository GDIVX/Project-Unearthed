using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Assets.Scripts.CharacterAbilities;
using System.Collections;

public class DodgeMovementTests
{
    private DodgeMovement dodgeMovement;
    private Health health;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = new GameObject();
        dodgeMovement = gameObject.AddComponent<DodgeMovement>();

        // Initialize values for testing
        dodgeMovement.DodgeSpeed = 10f;
        dodgeMovement.DodgeDuration = 0.5f;
        dodgeMovement.DodgeCooldown = 2f;
        dodgeMovement.InvisibilityDuration = 0.5f;

        health = gameObject.AddComponent<Health>();
        dodgeMovement.Health = health;
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(dodgeMovement.gameObject);
    }

    [UnityTest]
    public IEnumerator DodgeStartAndEndEventsFired()
    {
        bool dodgeStartFired = false;
        bool dodgeEndFired = false;

        dodgeMovement.OnDodgeStart += () => dodgeStartFired = true;
        dodgeMovement.OnDodgeEnd += () => dodgeEndFired = true;

        dodgeMovement.Dodge(Vector3.forward);

        yield return new WaitForSeconds(dodgeMovement.DodgeDuration);

        Assert.IsTrue(dodgeStartFired, "OnDodgeStart event was not fired.");
        Assert.IsTrue(dodgeEndFired, "OnDodgeEnd event was not fired.");
    }

    [UnityTest]
    public IEnumerator DodgeCooldownRespected()
    {
        dodgeMovement.Dodge(Vector3.forward);
        yield return null; // Wait for one frame
        bool canDodge = dodgeMovement.CanDodge;
        Assert.IsFalse(canDodge, "CanDodge should be false within the dodge cooldown period.");
    }
}
