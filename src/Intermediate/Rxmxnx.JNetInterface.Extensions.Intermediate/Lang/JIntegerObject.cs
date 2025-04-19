namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Integer</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JIntegerObject : JNumberObject<JInt, JIntegerObject>, IPrimitiveWrapperType<JIntegerObject, JInt>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JIntegerObject> typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JIntegerObject>(IPrimitiveType.GetMetadata<JInt>(),
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

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IPrimitiveWrapperType<JIntegerObject>.SetPrimitiveValue(IPrimitiveType value)
		=> base.SetPrimitiveValue(value.ToInt32(CultureInfo.InvariantCulture));

	static JIntegerObject? IPrimitiveWrapperType<JIntegerObject, JInt>.Create(IEnvironment env, JInt? value)
		=> value is not null ? (JIntegerObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JIntegerObject IClassType<JIntegerObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JIntegerObject IClassType<JIntegerObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JIntegerObject IClassType<JIntegerObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}