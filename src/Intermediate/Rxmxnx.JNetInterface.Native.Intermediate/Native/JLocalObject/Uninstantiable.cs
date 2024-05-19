namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class represents an uninstantiable java type instance.
	/// </summary>
	public abstract class Uninstantiable<TUninstantiable> : JLocalObject
		where TUninstantiable : Uninstantiable<TUninstantiable>, IUninstantiableType<TUninstantiable>, new()
	{
		/// <inheritdoc/>
		public override CString ObjectClassName => IReferenceType.GetMetadata<TUninstantiable>().ClassName;
		/// <inheritdoc/>
		public override CString ObjectSignature => IReferenceType.GetMetadata<TUninstantiable>().Signature;

		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		protected Uninstantiable() : base(CommonValidationUtilities.ThrowInvalidInstantiation<TUninstantiable>()) { }
	}
}