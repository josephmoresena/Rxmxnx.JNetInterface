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
	internal JNumberObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	internal JNumberObject(JClassObject jClass, JObjectLocalRef localRef, TValue value) : base(jClass, localRef)
		=> this._value = value;
	/// <inheritdoc/>
	internal JNumberObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass)
	{
		if (jLocal is JNumberObject number)
			this._value = number.GetValue<TValue>();
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="value">Instance value.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JNumberObject(JLocalObject jLocal, TValue? value, JClassObject jClass) : base(jLocal, jClass)
	{
		this._value = value;
		jLocal.Dispose();
	}

	/// <summary>
	/// Internal value.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public TValue Value => this._value ??= this.GetValue<TValue>();

	/// <inheritdoc/>
	public override TPrimitive GetValue<TPrimitive>()
	{
		if (typeof(TPrimitive) == typeof(TValue) && this._value is not null)
		{
			TValue result = this._value.Value;
			return NativeUtilities.Transform<TValue, TPrimitive>(in result);
		}
		return base.GetValue<TPrimitive>();
	}

	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JPrimitiveWrapperObjectMetadata<TValue>(base.CreateMetadata()) { Value = this.Value, };
	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is JPrimitiveWrapperObjectMetadata<TValue> wrapperMetadata)
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
	internal JNumberObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	internal JNumberObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassFeature.GetClass<TNumber>()) { }
	/// <inheritdoc/>
	internal JNumberObject(JClassObject jClass, JObjectLocalRef localRef, TValue value) :
		base(jClass, localRef, value) { }

	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Value.Equals(other);
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Value.Equals(other);

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other) => base.Equals(other) || this.Value.Equals(other);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => base.Equals(obj) || this.Value.Equals(obj);

	/// <inheritdoc cref="IPrimitiveWrapperType{TNumber, TValue}.Create(IEnvironment, Nullable{TValue})"/>
	[return: NotNullIfNotNull(nameof(value))]
	public static TNumber? Create(IEnvironment env, TValue? value) => TNumber.Create(env, value);
	/// <inheritdoc cref="IReferenceType{TNumber}.Create(JLocalObject?)"/>
	public new static TNumber? Create(JLocalObject? jLocal) => TNumber.Create(jLocal);
	/// <inheritdoc cref="IReferenceType{TNumber}.Create(IEnvironment, JGlobalBase?)"/>
	public new static TNumber? Create(IEnvironment env, JGlobalBase? jGlobal) => TNumber.Create(env, jGlobal);
}
#pragma warning restore CS0659