namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Number</c> instance.
/// </summary>
/// <typeparam name="TValue">Number <see cref="IPrimitiveType"/> type.</typeparam>
public abstract class JNumberObject<TValue> : JNumberObject, IWrapper<TValue>
	where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>, ISignedNumber<TValue>
{
	/// <inheritdoc cref="JNumberObject{TPrimitive}.Value"/>
	private TValue? _value;

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
		JLocalObject jLocal = initializer.Instance;
		if (jLocal is JNumberObject number)
			this._value = number.GetValue<TValue>();
	}

	/// <summary>
	/// Internal value.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public TValue Value => this._value ??= this.GetValue<TValue>();

	/// <inheritdoc/>
	public override TPrimitive GetValue<TPrimitive>()
	{
		if (typeof(TPrimitive) != typeof(TValue) || this._value is null) return base.GetValue<TPrimitive>();
		TValue result = this._value.Value;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
		=> new PrimitiveWrapperObjectMetadata<TValue>(base.CreateMetadata()) { Value = this.Value, };
	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is PrimitiveWrapperObjectMetadata<TValue> wrapperMetadata)
			this._value = wrapperMetadata.Value;
	}

	/// <summary>
	/// Sets current value.
	/// </summary>
	/// <param name="value">A <typeparamref name="TValue"/>.</param>
	internal void SetValue(TValue value) => this._value ??= value;
}

#pragma warning disable CS0659
/// <summary>
/// This class represents a local <c>java.lang.Number</c> instance.
/// </summary>
/// <typeparam name="TValue">Number <see cref="IPrimitiveType"/> type.</typeparam>
/// <typeparam name="TNumber"><see cref="JNumberObject"/> type.</typeparam>
public abstract class JNumberObject<TValue, TNumber> : JNumberObject<TValue>, IPrimitiveWrapperType,
	IPrimitiveEquatable, IInterfaceObject<JComparableObject>
	where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>, ISignedNumber<TValue>
	where TNumber : JNumberObject<TValue, TNumber>, IPrimitiveWrapperType<TNumber, TValue>
{
	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => IPrimitiveType.GetMetadata<TValue>();

	/// <inheritdoc/>
	internal JNumberObject(JClassObject jClass, JObjectLocalRef localRef, TValue value) :
		base(jClass, localRef, value) { }
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private protected JNumberObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Value.Equals(other);
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Value.Equals(other);

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other) => base.Equals(other) || this.Value.Equals(other);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => Object.ReferenceEquals(this, obj) || this.Value.Equals(obj);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.Value.GetHashCode();

	/// <inheritdoc cref="IPrimitiveWrapperType{TNumber, TValue}.Create(IEnvironment, Nullable{TValue})"/>
	[return: NotNullIfNotNull(nameof(value))]
	public static TNumber? Create(IEnvironment env, TValue? value) => TNumber.Create(env, value);
}
#pragma warning restore CS0659