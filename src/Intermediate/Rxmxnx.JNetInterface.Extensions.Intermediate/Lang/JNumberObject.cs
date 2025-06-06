namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Number</c> instance.
/// </summary>
/// <typeparam name="TValue">Number <see cref="IPrimitiveType"/> type.</typeparam>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public abstract partial class JNumberObject<TValue> : JNumberObject, IWrapper<TValue>
	where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>, ISignedNumber<TValue>
{
	/// <inheritdoc cref="JNumberObject{TPrimitive}.Value"/>
	private TValue? _value;

	/// <summary>
	/// Internal value.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public TValue Value => this._value ??= base.GetValue<TValue>();

	/// <inheritdoc/>
	public sealed override TPrimitive GetValue<TPrimitive>()
	{
		if (typeof(TPrimitive) != typeof(TValue)) return base.GetValue<TPrimitive>();
		TValue result = this.Value;
		return NativeUtilities.Transform<TValue, TPrimitive>(in result);
	}

	/// <inheritdoc/>
	protected sealed override ObjectMetadata CreateMetadata()
		=> new PrimitiveWrapperObjectMetadata<TValue>(base.CreateMetadata()) { Value = this.Value, };
	/// <inheritdoc/>
	protected sealed override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is PrimitiveWrapperObjectMetadata<TValue> wrapperMetadata)
			this._value = wrapperMetadata.Value;
	}

	/// <inheritdoc cref="IPrimitiveWrapperType{TWrapper}.SetPrimitiveValue"/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private protected void SetPrimitiveValue(TValue value) => this._value = value;
}

#pragma warning disable CS0659
/// <summary>
/// This class represents a local <c>java.lang.Number</c> instance.
/// </summary>
/// <typeparam name="TValue">Number <see cref="IPrimitiveType"/> type.</typeparam>
/// <typeparam name="TNumber"><see cref="JNumberObject"/> type.</typeparam>
public abstract partial class
	JNumberObject<TValue, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TNumber> :
	JNumberObject<TValue>, IPrimitiveEquatable,
	IInterfaceObject<JComparableObject>
	where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>, ISignedNumber<TValue>
	where TNumber : JNumberObject<TValue, TNumber>, IPrimitiveWrapperType<TNumber, TValue>
{
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Value.Equals(other);
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Value.Equals(other);

	/// <inheritdoc/>
	public sealed override Boolean Equals(JObject? other) => base.Equals(other) || this.Value.Equals(other);
	/// <inheritdoc/>
	public sealed override Boolean Equals(Object? obj) => Object.ReferenceEquals(this, obj) || this.Value.Equals(obj);
	/// <inheritdoc/>
	public sealed override Int32 GetHashCode() => this.Value.GetHashCode();
	/// <inheritdoc/>
	public sealed override String ToString() => this.Value.ToString()!;
	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public sealed override String ToTraceText()
		=> $"{JObject.GetObjectIdentifier(this.Class.ClassSignature, this.Reference)} {IPrimitiveType.GetMetadata<TValue>().Signature}: {this.Value}";

	/// <inheritdoc cref="IPrimitiveWrapperType{TNumber, TValue}.Create(IEnvironment, Nullable{TValue})"/>
	[return: NotNullIfNotNull(nameof(value))]
	public static TNumber? Create(IEnvironment env, TValue? value) => TNumber.Create(env, value);
}
#pragma warning restore CS0659