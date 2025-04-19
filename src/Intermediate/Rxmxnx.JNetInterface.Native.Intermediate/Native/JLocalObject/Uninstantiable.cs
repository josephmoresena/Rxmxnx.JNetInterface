namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class represents an uninstantiable java type instance.
	/// </summary>
	public abstract class
		Uninstantiable<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TUninstantiable> : JLocalObject
		where TUninstantiable : Uninstantiable<TUninstantiable>, IUninstantiableType<TUninstantiable>
	{
		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		protected Uninstantiable() : base(CommonValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>()) { }
	}
}