namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
public sealed class JByteObject : JNumberObject<JByte, JByteObject>, IPrimitiveWrapperType<JByteObject, JByte>
{
	static JDataTypeMetadata IDataType.Metadata
		=> new JPrimitiveWrapperTypeMetadata<JByteObject>(IClassType.GetMetadata<JNumberObject>());

	/// <inheritdoc/>
	internal JByteObject(JClassObject jClass, JObjectLocalRef localRef, JByte value) : base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JByteObject(IReferenceType.ClassInitializer initializer) : base(initializer.ToInternal()) { }
	/// <inheritdoc/>
	private JByteObject(IReferenceType.GlobalInitializer initializer) : base(initializer.ToInternal()) { }
	/// <inheritdoc/>
	private JByteObject(IReferenceType.ObjectInitializer initializer) : base(initializer.ToInternal<JByteObject>()) { }

	static JByteObject? IPrimitiveWrapperType<JByteObject, JByte>.Create(IEnvironment env, JByte? value)
		=> value is not null ? (JByteObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JByteObject IReferenceType<JByteObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JByteObject IReferenceType<JByteObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JByteObject IReferenceType<JByteObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}