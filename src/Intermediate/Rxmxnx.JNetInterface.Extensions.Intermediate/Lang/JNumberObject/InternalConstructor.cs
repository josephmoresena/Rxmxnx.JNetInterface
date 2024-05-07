namespace Rxmxnx.JNetInterface.Lang;

public abstract partial class JNumberObject<TValue>
{
	/// <inheritdoc/>
	private protected JNumberObject(JClassObject jClass, JObjectLocalRef localRef, TValue value) :
		base(jClass, localRef)
		=> this._value = value;
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.ObjectInitializer initializer) : base(initializer)
	{
		JNumberObject? jBooleanObject = initializer.Instance as JNumberObject;
		this._value = jBooleanObject?.GetValue<TValue>();
	}
}

public abstract partial class JNumberObject<TValue, TNumber>
{
	/// <inheritdoc/>
	private protected JNumberObject(JClassObject jClass, JObjectLocalRef localRef, TValue value) : base(
		jClass, localRef, value) { }
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
}