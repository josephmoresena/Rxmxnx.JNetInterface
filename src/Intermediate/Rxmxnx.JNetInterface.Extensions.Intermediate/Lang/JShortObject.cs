namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Short</c> instance.
/// </summary>
public sealed class JShortObject : JNumberObject<JShort, JShortObject>, IPrimitiveWrapperType<JShortObject, JShort>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JShortObject>(IClassType.GetMetadata<JNumberObject>());

	/// <inheritdoc/>
	internal JShortObject(JClassObject jClass, JObjectLocalRef localRef, JShort value) :
		base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JShortObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JShortObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JShortObject(IReferenceType.ObjectInitializer initializer) : base(initializer.WithClass<JShortObject>()) { }

	static JShortObject? IPrimitiveWrapperType<JShortObject, JShort>.Create(IEnvironment env, JShort? value)
		=> value is not null ? (JShortObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JShortObject IReferenceType<JShortObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JShortObject IReferenceType<JShortObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JShortObject IReferenceType<JShortObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}