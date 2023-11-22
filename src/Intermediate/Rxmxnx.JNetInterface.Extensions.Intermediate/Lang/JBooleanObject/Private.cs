namespace Rxmxnx.JNetInterface.Lang;

public sealed partial class JBooleanObject
{
	/// <summary>
	/// Function name for retrieve primitive value.
	/// </summary>
	private static readonly CString getValueName = new(() => "booleanValue"u8);

	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => IPrimitiveType.GetMetadata<JBoolean>();
	static JDataTypeMetadata IDataType.Metadata => new JPrimitiveWrapperTypeMetadata<JBooleanObject>();
	static CString IPrimitiveWrapperType.ArraySignature
		=> UnicodeWrapperObjectArraySignatures.JBooleanObjectArraySignature;

	/// <inheritdoc cref="JBooleanObject.Value"/>
	private JBoolean? _value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JBooleanObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.BooleanClassObject)
	{
		if (jLocal is JBooleanObject wrapper)
			this._value = wrapper._value;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="value">Instance value.</param>
	private JBooleanObject(JLocalObject jLocal, JBoolean? value) : base(
		jLocal, jLocal.Environment.ClassProvider.BooleanClassObject)
	{
		this._value = value;
		jLocal.Dispose();
	}
	/// <inheritdoc/>
	private JBooleanObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }

	/// <summary>
	/// Retrieves the primitive value for current instance.
	/// </summary>
	/// <returns>Primitive instance value.</returns>
	private JBoolean GetValue()
	{
		JFunctionDefinition<JBoolean> definition = new(JBooleanObject.getValueName);
		return definition.Invoke(this);
	}
}