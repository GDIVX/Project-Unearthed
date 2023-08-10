using Assets.Scripts.CharacterAbilities;

public class MovementState : State
{
    private Movement movementComponent;
    private bool enabledAtStart;

    public MovementState(Movement movementComponent, StateMachine parent) : base(parent)
    {
        this.movementComponent = movementComponent;
        this.enabledAtStart = movementComponent.enabled;
    }

    public override void Enter()
    {
        movementComponent.enabled = true;
        base.Enter();

    }

    public override void Exit()
    {

        movementComponent.enabled = enabledAtStart;
        base.Exit();
    }

    public override void Execute()
    {
        base.Execute();

        // Additional logic here if needed
    }
}
