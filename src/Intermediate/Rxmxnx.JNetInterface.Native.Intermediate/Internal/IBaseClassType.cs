namespace Rxmxnx.JNetInterface.Internal;

/// <inheritdoc cref="IClassType"/>
/// <remarks>
/// This interface was created only for exclude <see cref="JLocalObject"/> metadata on
/// <see cref="JLocalObject"/> derived types.
/// </remarks>
internal interface IBaseClassType : IClassType
{
	/// <inheritdoc cref="IReferenceType.ExcludingTypes"/>
	private static readonly ImmutableHashSet<Type> excludingTypes = ImmutableHashSet.Create(
		typeof(IDisposable), typeof(IObject), typeof(IEquatable<JObject>), typeof(IDataType), typeof(IPrimitiveType),
		typeof(IReferenceType), typeof(IInterfaceType), typeof(IClassType), typeof(IBaseClassType),
		typeof(JReferenceObject), typeof(JInterfaceObject), typeof(IDataType<JLocalObject>),
		typeof(IReferenceType<JLocalObject>), typeof(IBaseClassType<JLocalObject>));

	static IImmutableSet<Type> IReferenceType.ExcludingTypes => IBaseClassType.excludingTypes;
}

/// <inheritdoc cref="IClassType{TClass}"/>
/// <remarks>
/// This interface was created only for force the implementation of <see cref="IDataType.Metadata"/> on
/// <see cref="JLocalObject"/> derived types.
/// </remarks>
internal interface IBaseClassType<out TClass> : IBaseClassType, IClassType<TClass>
	where TClass : JReferenceObject, IBaseClassType<TClass>
{
	static JDataTypeMetadata IDataType.Metadata => JLocalObject.JObjectClassMetadata;
}