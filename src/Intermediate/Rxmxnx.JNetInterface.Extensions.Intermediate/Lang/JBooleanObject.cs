namespace Rxmxnx.JNetInterface.Lang;

#pragma warning disable CS0659
/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
public sealed partial class JBooleanObject : JLocalObject, IPrimitiveEquatable,
	IPrimitiveWrapperType<JBooleanObject, JBoolean>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="value">Instance value.</param>
	internal JBooleanObject(JClassObject jClass, JObjectLocalRef localRef, JBoolean value) : base(jClass, localRef)
		=> this._value = value;
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Value.Equals(other);
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Value.Equals(other);
	/// <summary>
	/// Internal value.
	/// </summary>
	public JBoolean Value
		=> this._value ??= JFunctionDefinition<JBoolean>.Invoke(InternalFunctionCache.BooleanValueDefinition, this);

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other) => base.Equals(other) || this.Value.Equals(other);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => Object.ReferenceEquals(this, obj) || this.Value.Equals(obj);

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
		=> new PrimitiveWrapperObjectMetadata<JBoolean>(base.CreateMetadata()) { Value = this.Value, };
	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is PrimitiveWrapperObjectMetadata<JBoolean> wrapperMetadata)
			this._value = wrapperMetadata.Value;
	}

	/// <summary>
	/// Creates a <see cref="JBooleanObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JBoolean"/> value.</param>
	/// <returns>A new <see cref="JByteObject"/> instance.</returns>
	[return: NotNullIfNotNull(nameof(value))]
	public static JBooleanObject? Create(IEnvironment env, JBoolean? value)
		=> value is not null ? (JBooleanObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
}
#pragma warning restore CS0659