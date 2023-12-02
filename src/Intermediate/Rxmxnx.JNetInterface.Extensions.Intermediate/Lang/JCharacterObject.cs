namespace Rxmxnx.JNetInterface.Lang;

#pragma warning disable CS0659
/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
public sealed partial class JCharacterObject : JLocalObject, IPrimitiveEquatable,
	IPrimitiveWrapperType<JCharacterObject, JChar>
{
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => this.Value.Equals(other);
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => this.Value.Equals(other);
	/// <summary>
	/// Internal value.
	/// </summary>
	public new JChar Value => this._value ??= this.GetValue();

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other) => base.Equals(other) || this.Value.Equals(other);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => base.Equals(obj) || this.Value.Equals(obj);
	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JPrimitiveWrapperObjectMetadata<JChar>(base.CreateMetadata()) { Value = this.Value, };
	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is JPrimitiveWrapperObjectMetadata<JChar> wrapperMetadata)
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
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value.Value), value) : default;
	/// <inheritdoc/>
	public static JCharacterObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JCharacterObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JCharacterObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JCharacterObject>(jGlobal, env)) :
			default;
}
#pragma warning restore CS0659