namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Integer</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JIntegerObject : JNumberObject<JInt, JIntegerObject>, IPrimitiveWrapperType<JIntegerObject, JInt>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JIntegerObject> typeMetadata =
		new(TypeMetadataBuilder<JIntegerObject>.Build(IPrimitiveType.GetMetadata<JInt>(),
		                                               IClassType.GetMetadata<JNumberObject>()));

	static JPrimitiveWrapperTypeMetadata<JIntegerObject> IPrimitiveWrapperType<JIntegerObject>.Metadata
		=> JIntegerObject.typeMetadata;

	/// <inheritdoc/>
	internal JIntegerObject(JClassObject jClass, JObjectLocalRef localRef, JInt value) :
		base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JIntegerObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JIntegerObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JIntegerObject(IReferenceType.ObjectInitializer initializer) :
		base(initializer.WithClass<JIntegerObject>()) { }

	static JIntegerObject? IPrimitiveWrapperType<JIntegerObject, JInt>.Create(IEnvironment env, JInt? value)
		=> value is not null ? (JIntegerObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JIntegerObject IReferenceType<JIntegerObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIntegerObject IReferenceType<JIntegerObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIntegerObject IReferenceType<JIntegerObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}