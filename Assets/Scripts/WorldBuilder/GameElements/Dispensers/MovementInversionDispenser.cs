using F = Utils.FictracController;

public class MovementInversionDispenser : Dispenser {

	public MovementInversionDispenser(string dispenserName) : base(dispenserName) {}

	public override void Dispense(string callingGameObjectName = null) {
		F.movementInversionToggle *= -1;
	}
}
