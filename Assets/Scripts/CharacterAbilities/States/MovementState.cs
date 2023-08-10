using Assets.Scripts.CharacterAbilities;
using CharacterAbilities;

public class MovementState : State
{
    private Movement movementComponent;
    private bool enabledAtStart;

    IMovementInput movementInput;
    public MovementState(Movement movementComponent, IMovementInput movementInput, StateMachine parent) : base(parent)
    {
        this.movementComponent = movementComponent;
        this.enabledAtStart = movementComponent.enabled;
        this.movementInput = movementInput;
    }

    public override void Execute()
    {
        base.Execute();

        movementComponent.Move(movementInput.GetMovementVector());


    }
}
