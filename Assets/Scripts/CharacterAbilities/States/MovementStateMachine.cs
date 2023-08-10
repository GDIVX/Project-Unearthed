using Assets.Scripts.CharacterAbilities;
using Assets.Scripts.CharacterAbilities.States;
using CharacterAbilities;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// This class is responsible for building the state machine for movement behaviour.
/// </summary>
public class MovementStateMachine : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] DodgeMovement dodgeMovement;

    [ShowInInspector] StateMachine stateMachine;

    [Inject] IMovementInput movementInput;

    bool isDodging = false;

    private void Awake()
    {
        movement ??= GetComponent<Movement>();

        if (movement == null)
        {
            Debug.LogError("Movement component not found");
        }

        BuildStateMachine();
    }

    private void BuildStateMachine()
    {
        stateMachine = new();

        //Movement state
        MovementState movementState = new(movement, stateMachine);
        stateMachine.AddState(movementState, "Move");

        //set movement as the opening state
        stateMachine.SetState(movementState);

        //Dodge state
        DodgeState dodgeState = new(dodgeMovement, movementInput, stateMachine);
        stateMachine.AddState(dodgeState, "Dodge");

        //when the player presses the dodge button, transition to the dodge state
        movementInput.OnDodge += () => isDodging = true;
        stateMachine.AddTransition("Move", "Dodge", () => isDodging);

        //when the dodge state is finished, transition back to the move state
        dodgeMovement.OnDodgeEnd += () => isDodging = false;
        stateMachine.AddTransition("Dodge", "Move", () => !isDodging);

    }

    private void Update()
    {
        if (stateMachine == null) return;

        stateMachine.ExecuteState();
    }
}
