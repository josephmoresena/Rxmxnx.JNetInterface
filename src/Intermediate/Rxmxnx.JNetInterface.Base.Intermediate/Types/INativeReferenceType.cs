namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a native java reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface INativeReferenceType : IFixedPointer, INativeType;

/// <summary>
/// This interface exposes a native java reference.
/// </summary>
/// <typeparam name="TSelf">Type of <see cref="INativeReferenceType{TSelf}"/>.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface INativeReferenceType<TSelf> : INativeReferenceType, INativeType<TSelf>
	where TSelf : unmanaged, INativeReferenceType<TSelf>;

/// <summary>
/// This interface exposes a native java reference.
/// </summary>
/// <typeparam name="TReference">Type of <see cref="INativeReferenceType{TReference,TValue}"/>.</typeparam>
/// <typeparam name="TValue">Type of referenced value.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface
	INativeReferenceType<TReference, TValue> : INativeReferenceType<TReference>, IReadOnlyReferenceable<TValue>
	where TReference : unmanaged, INativeReferenceType<TReference, TValue> where TValue : unmanaged;