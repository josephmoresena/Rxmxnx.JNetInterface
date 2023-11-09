namespace Rxmxnx.JNetInterface.Internal.Types;

/// <inheritdoc cref="IReferenceType"/>
/// <remarks>
/// This interface was created only for exclude <see cref="JLocalObject"/> metadata on
/// <see cref="JLocalObject"/> derived types.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IBaseReferenceType : IReferenceType
{
	/// <inheritdoc cref="IDataType.ExcludingTypes"/>
	internal static readonly ImmutableHashSet<Type> ExcludingReferenceBaseTypes =
		IDataType.ExcludingBasicTypes.Union(new[]
		{
			typeof(IBaseReferenceType), typeof(JInterfaceObject),
			typeof(IDataType<JLocalObject>),
			typeof(IReferenceType<JLocalObject>),
			typeof(ISuperClassType<JLocalObject>),
		});

	static IImmutableSet<Type> IDataType.ExcludingTypes => IBaseReferenceType.ExcludingReferenceBaseTypes;
}