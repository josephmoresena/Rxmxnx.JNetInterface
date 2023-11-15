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
	public new TValue Value => this._value ??= this.GetValue<TValue>();

	/// <inheritdoc/>
	public override TPrimitive GetValue<TPrimitive>()
	{
		if (typeof(TPrimitive) == typeof(TValue) && this._value is not null)
			return NativeUtilities.Transform<TValue, TPrimitive>(this._value.Value);
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
}

/// <summary>
/// This class represents a local <c>java.lang.Number</c> instance.
/// </summary>
/// <typeparam name="TValue">Number <see cref="IPrimitiveType"/> type.</typeparam>
/// <typeparam name="TNumber"><see cref="JNumberObject"/> type.</typeparam>
public abstract class
	JNumberObject<TValue, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TNumber> :
		JNumberObject<TValue>, IPrimitiveWrapperType, IInterfaceImplementation<TNumber, JSerializableObject>,
		IInterfaceImplementation<TNumber, JComparableObject>
	where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>, ISignedNumber<TValue>
	where TNumber : JNumberObject<TValue, TNumber>, IPrimitiveWrapperType<TNumber>
{
	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => IPrimitiveType.GetMetadata<TValue>();

	/// <inheritdoc/>
	internal JNumberObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	internal JNumberObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.GetClass<TNumber>()) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="value">Instance value.</param>
	internal JNumberObject(JLocalObject jLocal, TValue? value) : base(jLocal, value,
	                                                                  jLocal.Environment.ClassProvider
	                                                                        .GetClass<TNumber>()) { }

	/// <inheritdoc/>
	public new static TNumber? Create(JObject? jObject) => TNumber.Create(jObject);
}