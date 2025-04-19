namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Float</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JFloatObject : JNumberObject<JFloat, JFloatObject>, IPrimitiveWrapperType<JFloatObject, JFloat>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JFloatObject> typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JFloatObject>(IPrimitiveType.GetMetadata<JFloat>(),
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

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IPrimitiveWrapperType<JFloatObject>.SetPrimitiveValue(IPrimitiveType value)
		=> base.SetPrimitiveValue(value.ToSingle(CultureInfo.InvariantCulture));

	static JFloatObject? IPrimitiveWrapperType<JFloatObject, JFloat>.Create(IEnvironment env, JFloat? value)
		=> value is not null ? (JFloatObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JFloatObject IClassType<JFloatObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFloatObject IClassType<JFloatObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFloatObject IClassType<JFloatObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}