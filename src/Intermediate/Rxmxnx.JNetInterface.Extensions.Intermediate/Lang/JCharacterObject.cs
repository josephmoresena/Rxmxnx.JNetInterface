namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
public sealed partial class JCharacterObject : JLocalObject, IPrimitiveWrapperType<JCharacterObject, JChar>
{
	/// <summary>
	/// Internal value.
	/// </summary>
	public new JChar Value => this._value ??= this.GetValue();

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
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value), value) : default;
	/// <inheritdoc/>
	public static JCharacterObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JCharacterObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JCharacterObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JCharacterObject>(jGlobal, env)) :
			default;
}