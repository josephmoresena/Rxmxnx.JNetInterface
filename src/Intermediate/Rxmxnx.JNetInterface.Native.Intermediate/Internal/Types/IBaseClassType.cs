namespace Rxmxnx.JNetInterface.Internal.Types;

/// <inheritdoc cref="IClassType"/>
/// <remarks>
/// This interface was created only for exclude <see cref="JLocalObject"/> metadata on
/// <see cref="JLocalObject"/> derived types.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IBaseClassType : IClassType
{
	/// <inheritdoc cref="IDataType.ExcludingTypes"/>
	internal static readonly ImmutableHashSet<Type> ExcludingReferenceBaseTypes =
		IDataType.ExcludingBasicTypes.Union(new[]
		{
			typeof(IBaseClassType), typeof(JInterfaceObject),
			typeof(IDataType<JLocalObject>),
			typeof(IReferenceType<JLocalObject>),
			typeof(IBaseClassType<JLocalObject>),
		});

	static IImmutableSet<Type> IDataType.ExcludingTypes => IBaseClassType.ExcludingReferenceBaseTypes;
}

/// <inheritdoc cref="IClassType{TClass}"/>
/// <remarks>
/// This interface was created only for force the implementation of <see cref="IDataType.Metadata"/> on
/// <see cref="JLocalObject"/> derived types.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IBaseClassType<out TClass> : IBaseClassType, IClassType<TClass>
	where TClass : JReferenceObject, IBaseClassType<TClass>
{
	static JDataTypeMetadata IDataType.Metadata => JLocalObject.JObjectClassMetadata;
}