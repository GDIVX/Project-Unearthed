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

    public Movement Movement { get => movement; set => movement = value; }
    public DodgeMovement DodgeMovement { get => dodgeMovement; set => dodgeMovement = value; }
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
    public IMovementInput MovementInput { get => movementInput; set => movementInput = value; }

    private void Awake()
    {
        Movement ??= GetComponent<Movement>();
        DodgeMovement ??= GetComponent<DodgeMovement>();
        movementInput ??= GetComponent<IMovementInput>();

        if (Movement == null)
        {
            Debug.LogError("Movement component not found");
        }

        if (DodgeMovement == null)
        {
            Debug.LogError("DodgeMovement component not found");
        }

        if (MovementInput == null)
        {
            Debug.LogError("MovementInput component not found");
        }

        BuildStateMachine();
    }

    private void BuildStateMachine()
    {
        StateMachine = new();

        //Movement state
        MovementState movementState = new(Movement, MovementInput, StateMachine);
        StateMachine.AddState(movementState, "Move");

        //set movement as the opening state
        StateMachine.SetState(movementState);

        //Dodge state
        DodgeState dodgeState = new(DodgeMovement, MovementInput, StateMachine);
        StateMachine.AddState(dodgeState, "Dodge");

        //when the player presses the dodge button, transition to the dodge state
        MovementInput.OnDodge += () => isDodging = true;
        StateMachine.AddTransition("Move", "Dodge", () => isDodging);

        //when the dodge state is finished, transition back to the move state
        DodgeMovement.OnDodgeEnd += () => isDodging = false;
        StateMachine.AddTransition("Dodge", "Move", () => !isDodging);

    }

    private void Update()
    {
        if (StateMachine == null) return;

        StateMachine.ExecuteState();
    }
}
