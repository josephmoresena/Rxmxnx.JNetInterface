namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Short</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JShortObject : JNumberObject<JShort, JShortObject>, IPrimitiveWrapperType<JShortObject, JShort>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JShortObject> typeMetadata =
		new(TypeMetadataBuilder<JShortObject>.Build(IPrimitiveType.GetMetadata<JShort>(),
		                                            IClassType.GetMetadata<JNumberObject>()));

	static JPrimitiveWrapperTypeMetadata<JShortObject> IPrimitiveWrapperType<JShortObject>.Metadata
		=> JShortObject.typeMetadata;

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
	static JShortObject IClassType<JShortObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JShortObject IClassType<JShortObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JShortObject IClassType<JShortObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}