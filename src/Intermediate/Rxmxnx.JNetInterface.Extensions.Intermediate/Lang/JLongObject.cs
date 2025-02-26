namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Long</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JLongObject : JNumberObject<JLong, JLongObject>, IPrimitiveWrapperType<JLongObject, JLong>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JLongObject> typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JLongObject>(IPrimitiveType.GetMetadata<JLong>(),
		                                                    IClassType.GetMetadata<JNumberObject>()));

	static JPrimitiveWrapperTypeMetadata<JLongObject> IPrimitiveWrapperType<JLongObject>.Metadata
		=> JLongObject.typeMetadata;

	/// <inheritdoc/>
	internal JLongObject(JClassObject jClass, JObjectLocalRef localRef, JLong value) : base(jClass, localRef, value) { }

	/// <inheritdoc/>
	private JLongObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JLongObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JLongObject(IReferenceType.ObjectInitializer initializer) : base(initializer.WithClass<JLongObject>()) { }

	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IPrimitiveWrapperType<JLongObject>.SetPrimitiveValue(IPrimitiveType value)
		=> base.SetPrimitiveValue(value.ToInt64(CultureInfo.InvariantCulture));

	static JLongObject? IPrimitiveWrapperType<JLongObject, JLong>.Create(IEnvironment env, JLong? value)
		=> value is not null ? (JLongObject)env.ReferenceFeature.CreateWrapper(value.Value) : default;
	static JLongObject IClassType<JLongObject>.Create(IReferenceType.ClassInitializer initializer) => new(initializer);
	static JLongObject IClassType<JLongObject>.Create(IReferenceType.ObjectInitializer initializer) => new(initializer);
	static JLongObject IClassType<JLongObject>.Create(IReferenceType.GlobalInitializer initializer) => new(initializer);
}