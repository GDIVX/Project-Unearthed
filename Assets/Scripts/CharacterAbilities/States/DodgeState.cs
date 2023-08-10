using CharacterAbilities;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.CharacterAbilities.States
{
    public class DodgeState : State
    {
        DodgeMovement dodgeMovement;
        IMovementInput movementInput;
        public DodgeState(DodgeMovement dodgeMovement, IMovementInput movementInput, StateMachine parent) : base(parent)
        {
            this.dodgeMovement = dodgeMovement;
            this.movementInput = movementInput;
        }

        public override void Enter()
        {
            if (dodgeMovement == null || !dodgeMovement.CanDodge)
            {
                return;
            }
            dodgeMovement.Dodge(movementInput.GetMovementVector());

            base.Enter();

        }
    }
}