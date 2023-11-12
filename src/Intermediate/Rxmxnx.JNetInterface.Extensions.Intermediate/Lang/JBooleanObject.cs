namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
public sealed partial class JBooleanObject : JLocalObject, IPrimitiveWrapperType<JBooleanObject>
{
	/// <summary>
	/// Internal value.
	/// </summary>
	public new JBoolean Value => this._value ??= this.GetValue();

	/// <inheritdoc/>
	public JBooleanObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JPrimitiveWrapperObjectMetadata<JBoolean>(base.CreateMetadata()) { Value = this.Value, };
	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is JPrimitiveWrapperObjectMetadata<JBoolean> wrapperMetadata)
			this._value = wrapperMetadata.Value;
	}

	/// <summary>
	/// Creates a <see cref="JBooleanObject"/> instance initialized with <paramref name="value"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="value"><see cref="JBoolean"/> value.</param>
	/// <returns>A new <see cref="JByteObject"/> instance.</returns>
	public static JBooleanObject? Create(IEnvironment env, JBoolean? value)
		=> value is not null ? new(env.ReferenceProvider.CreateWrapper(value), value) : default;
	/// <inheritdoc/>
	public static JBooleanObject? Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JBooleanObject>(jLocal)) : default;
}