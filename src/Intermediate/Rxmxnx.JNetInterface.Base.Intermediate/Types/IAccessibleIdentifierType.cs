namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
internal interface IAccessibleIdentifierType : IFixedPointer, INativeType;

/// <summary>
/// This interface exposes a java accessible object identifier.
/// </summary>
/// <typeparam name="TIdentifier">A <see cref="IAccessibleIdentifierType"/> type.</typeparam>
internal interface IAccessibleIdentifierType<out TIdentifier> : IAccessibleIdentifierType
	where TIdentifier : unmanaged, IAccessibleIdentifierType<TIdentifier>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	static abstract TIdentifier New(IntPtr value);
}