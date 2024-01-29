namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class represents an uninstantiable java type instance.
	/// </summary>
	public class Uninstantiable<TUninstantiable> : JLocalObject
		where TUninstantiable : Uninstantiable<TUninstantiable>, IUninstantiableType<TUninstantiable>, new()
	{
		/// <inheritdoc/>
		public override CString ObjectClassName => IReferenceType.GetMetadata<TUninstantiable>().ClassName;
		/// <inheritdoc/>
		public override CString ObjectSignature => IReferenceType.GetMetadata<TUninstantiable>().Signature;

		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		internal Uninstantiable() : base(IUninstantiableType.ThrowInstantiation<TUninstantiable>()) { }
	}
}