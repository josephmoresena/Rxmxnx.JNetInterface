namespace Rxmxnx.JNetInterface.Lang;

public sealed partial class JCharacterObject
{
	/// <summary>
	/// Function name for retrieve primitive value.
	/// </summary>
	private static readonly CString getValueName = new(() => "booleanValue"u8);

	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => IPrimitiveType.GetMetadata<JChar>();
	static JDataTypeMetadata IDataType.Metadata => new JPrimitiveWrapperTypeMetadata<JCharacterObject>();
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JCharacterObjectArraySignature;

	/// <inheritdoc cref="JCharacterObject.Value"/>
	private JChar? _value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JCharacterObject(JLocalObject jLocal) : base(
		jLocal, jLocal.Environment.ClassProvider.CharacterClassObject())
	{
		if (jLocal is JCharacterObject wrapper)
			this._value = wrapper._value;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="value">Instance value.</param>
	private JCharacterObject(JLocalObject jLocal, JChar? value) : base(
		jLocal, jLocal.Environment.ClassProvider.CharacterClassObject())
	{
		this._value = value;
		jLocal.Dispose();
	}
	/// <inheritdoc/>
	private JCharacterObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	/// <summary>
	/// Retrieves the primitive value for current instance.
	/// </summary>
	/// <returns>Primitive instance value.</returns>
	private JChar GetValue()
	{
		JFunctionDefinition<JChar> definition = new(JCharacterObject.getValueName);
		return definition.Invoke(this);
	}
}