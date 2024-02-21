namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Number</c> instance.
/// </summary>
public partial class JNumberObject : JLocalObject, IClassType<JNumberObject>, IInterfaceObject<JSerializableObject>
{
	/// <inheritdoc/>
	internal JNumberObject(JClassObject jClass, JObjectLocalRef jLocalRef) : base(jClass, jLocalRef) { }

	/// <inheritdoc/>
	protected JNumberObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNumberObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNumberObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	/// <summary>
	/// Returns the value of the specified number as a <typeparamref name="TPrimitive"/>, which may
	/// involve rounding or truncation.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType"/> numeric type.</typeparam>
	/// <returns>A <typeparamref name="TPrimitive"/> numeric value.</returns>
	public virtual TPrimitive GetValue<TPrimitive>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
		=> this.Environment.FunctionSet.GetPrimitiveValue<TPrimitive>(this);
}