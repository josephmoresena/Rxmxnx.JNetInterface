namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Double</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JDoubleObject : JNumberObject<JDouble, JDoubleObject>, IPrimitiveWrapperType<JDoubleObject, JDouble>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JDoubleObject> typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JDoubleObject>(IPrimitiveType.GetMetadata<JDouble>(),
		                                                      IClassType.GetMetadata<JNumberObject>()));

	static JPrimitiveWrapperTypeMetadata<JDoubleObject> IPrimitiveWrapperType<JDoubleObject>.Metadata
		=> JDoubleObject.typeMetadata;

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

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IPrimitiveWrapperType<JDoubleObject>.SetPrimitiveValue(IPrimitiveType value)
		=> base.SetPrimitiveValue(value.ToDouble(CultureInfo.InvariantCulture));

	static JDoubleObject? IPrimitiveWrapperType<JDoubleObject, JDouble>.Create(IEnvironment env, JDouble? value)
		=> value is not null ? (JDoubleObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JDoubleObject IClassType<JDoubleObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDoubleObject IClassType<JDoubleObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDoubleObject IClassType<JDoubleObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}