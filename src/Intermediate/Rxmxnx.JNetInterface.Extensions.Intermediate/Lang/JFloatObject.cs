namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Float</c> instance.
/// </summary>
public sealed class JFloatObject : JNumberObject<JFloat, JFloatObject>, IPrimitiveWrapperType<JFloatObject, JFloat>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JFloatObject>(IClassType.GetMetadata<JNumberObject>());

	/// <inheritdoc/>
	internal JFloatObject(JClassObject jClass, JObjectLocalRef localRef, JFloat value) :
		base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JFloatObject(IReferenceType.ClassInitializer initializer) : base(initializer.ToInternal()) { }
	/// <inheritdoc/>
	private JFloatObject(IReferenceType.GlobalInitializer initializer) : base(initializer.ToInternal()) { }
	/// <inheritdoc/>
	private JFloatObject(IReferenceType.ObjectInitializer initializer) :
		base(initializer.ToInternal<JFloatObject>()) { }

	static JFloatObject? IPrimitiveWrapperType<JFloatObject, JFloat>.Create(IEnvironment env, JFloat? value)
		=> value is not null ? (JFloatObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JFloatObject IReferenceType<JFloatObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFloatObject IReferenceType<JFloatObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFloatObject IReferenceType<JFloatObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}