namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IAccessibleIdentifierType : IFixedPointer, INativeType;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
/// <typeparam name="TAccessible">Type of <see cref="IAccessibleIdentifierType{TAccessible}"/>.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IAccessibleIdentifierType<TAccessible> : IAccessibleIdentifierType, INativeType<TAccessible>
	where TAccessible : unmanaged, IAccessibleIdentifierType<TAccessible>;