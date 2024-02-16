namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Float</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JFloatObject : JNumberObject<JFloat, JFloatObject>, IPrimitiveWrapperType<JFloatObject, JFloat>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JFloatObject> typeMetadata =
		new(JTypeMetadataBuilder<JFloatObject>.Build(IPrimitiveType.GetMetadata<JFloat>(),
		                                             IClassType.GetMetadata<JNumberObject>()));

	static JPrimitiveWrapperTypeMetadata<JFloatObject> IPrimitiveWrapperType<JFloatObject>.Metadata
		=> JFloatObject.typeMetadata;

	/// <inheritdoc/>
	internal JFloatObject(JClassObject jClass, JObjectLocalRef localRef, JFloat value) :
		base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JFloatObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JFloatObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JFloatObject(IReferenceType.ObjectInitializer initializer) : base(initializer.WithClass<JFloatObject>()) { }

	static JFloatObject? IPrimitiveWrapperType<JFloatObject, JFloat>.Create(IEnvironment env, JFloat? value)
		=> value is not null ? (JFloatObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JFloatObject IReferenceType<JFloatObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFloatObject IReferenceType<JFloatObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFloatObject IReferenceType<JFloatObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}