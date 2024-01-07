namespace Rxmxnx.JNetInterface.Lang;

public sealed partial class JCharacterObject
{
	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => IPrimitiveType.GetMetadata<JChar>();
	static JDataTypeMetadata IDataType.Metadata => new JPrimitiveWrapperTypeMetadata<JCharacterObject>();

	/// <inheritdoc cref="JCharacterObject.Value"/>
	private JChar? _value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JCharacterObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassFeature.CharacterObject)
	{
		if (jLocal is JCharacterObject wrapper)
			this._value = wrapper._value;
	}
	/// <inheritdoc/>
	private JCharacterObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}