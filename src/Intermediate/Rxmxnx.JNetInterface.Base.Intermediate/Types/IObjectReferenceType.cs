namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IObjectReferenceType : IFixedPointer, IWrapper<JObjectLocalRef>, INativeType
{
	/// <summary>
	/// Determines whether current reference is default.
	/// </summary>
	Boolean IsDefault { get; }
}

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
/// <typeparam name="TObject">Type of <see cref="IArrayReferenceType"/>.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IObjectReferenceType<TObject> : IObjectReferenceType, INativeType<TObject>
	where TObject : unmanaged, IObjectReferenceType<TObject>
{
	/// <summary>
	/// Converts a given <see cref="JObjectLocalRef"/> to <typeparamref name="TObject"/> instance.
	/// </summary>
	/// <param name="objRef">A <see cref="JObjectLocalRef"/> to convert.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static abstract TObject FromReference(in JObjectLocalRef objRef);
}