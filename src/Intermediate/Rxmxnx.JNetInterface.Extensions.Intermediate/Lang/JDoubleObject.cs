namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Double</c> instance.
/// </summary>
public sealed class JDoubleObject : JNumberObject<JDouble, JDoubleObject>, IPrimitiveWrapperType<JDoubleObject, JDouble>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JDoubleObject>(IClassType.GetMetadata<JNumberObject>());

	/// <inheritdoc/>
	internal JDoubleObject(JClassObject jClass, JObjectLocalRef localRef, JDouble value) :
		base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JDoubleObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JDoubleObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JDoubleObject(IReferenceType.ObjectInitializer initializer) :
		base(initializer.WithClass<JDoubleObject>()) { }

	static JDoubleObject? IPrimitiveWrapperType<JDoubleObject, JDouble>.Create(IEnvironment env, JDouble? value)
		=> value is not null ? (JDoubleObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JDoubleObject IReferenceType<JDoubleObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDoubleObject IReferenceType<JDoubleObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDoubleObject IReferenceType<JDoubleObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}