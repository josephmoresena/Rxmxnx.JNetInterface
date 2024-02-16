namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Byte</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JByteObject : JNumberObject<JByte, JByteObject>, IPrimitiveWrapperType<JByteObject, JByte>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JByteObject> typeMetadata =
		new(JTypeMetadataBuilder<JByteObject>.Build(IPrimitiveType.GetMetadata<JByte>(),
		                                            IClassType.GetMetadata<JNumberObject>()));

	static JPrimitiveWrapperTypeMetadata<JByteObject> IPrimitiveWrapperType<JByteObject>.Metadata
		=> JByteObject.typeMetadata;

	/// <inheritdoc/>
	internal JByteObject(JClassObject jClass, JObjectLocalRef localRef, JByte value) : base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JByteObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JByteObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JByteObject(IReferenceType.ObjectInitializer initializer) : base(initializer.WithClass<JByteObject>()) { }

	static JByteObject? IPrimitiveWrapperType<JByteObject, JByte>.Create(IEnvironment env, JByte? value)
		=> value is not null ? (JByteObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JByteObject IReferenceType<JByteObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JByteObject IReferenceType<JByteObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JByteObject IReferenceType<JByteObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}