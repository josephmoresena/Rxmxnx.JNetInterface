namespace Rxmxnx.JNetInterface.Lang;

public sealed partial class JBooleanObject
{
	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => IPrimitiveType.GetMetadata<JBoolean>();
	static JDataTypeMetadata IDataType.Metadata => new JPrimitiveWrapperTypeMetadata<JBooleanObject>();

	/// <inheritdoc cref="JBooleanObject.Value"/>
	private JBoolean? _value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JBooleanObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassFeature.BooleanObject)
	{
		if (jLocal is JBooleanObject wrapper)
			this._value = wrapper._value;
	}
	/// <inheritdoc/>
	private JBooleanObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}