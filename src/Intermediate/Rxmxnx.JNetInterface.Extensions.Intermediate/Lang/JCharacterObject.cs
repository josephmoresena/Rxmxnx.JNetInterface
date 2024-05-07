namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
public sealed partial class JCharacterObject : JLocalObject, IPrimitiveEquatable,
	IPrimitiveWrapperType<JCharacterObject, JChar>, IInterfaceObject<JSerializableObject>,
	IInterfaceObject<JComparableObject>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="value">Instance value.</param>
	internal JCharacterObject(JClassObject jClass, JObjectLocalRef localRef, JChar? value) : base(jClass, localRef)
		=> this._value = value;
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Value.Equals(other);
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Value.Equals(other);
	/// <summary>
	/// Internal value.
	/// </summary>
	public JChar Value => this._value ??= JFunctionDefinition.Invoke(NativeFunctionSetImpl.CharValueDefinition, this);

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other) => base.Equals(other) || this.Value.Equals(other);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => Object.ReferenceEquals(this, obj) || this.Value.Equals(obj);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.Value.GetHashCode();
	/// <inheritdoc/>
	public override String ToString() => this.Value.ToString()!;

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
		=> new PrimitiveWrapperObjectMetadata<JChar>(base.CreateMetadata()) { Value = this.Value, };
	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is PrimitiveWrapperObjectMetadata<JChar> wrapperMetadata)
			this._value = wrapperMetadata.Value;
	}

	/// <summary>
	/// Creates a <see cref="JCharacterObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JChar"/> value.</param>
	/// <returns>A new <see cref="JByteObject"/> instance.</returns>
	[return: NotNullIfNotNull(nameof(value))]
	public static JCharacterObject? Create(IEnvironment env, JChar? value)
		=> value is not null ? (JCharacterObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
}