namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
internal interface IArrayReferenceType : IObjectReferenceType, IEquatable<JArrayLocalRef>
{
	/// <summary>
	/// JNI array reference.
	/// </summary>
	JArrayLocalRef ArrayValue { get; }
}

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
/// <typeparam name="TArrayRef">A <see cref="IArrayReferenceType"/> type.</typeparam>
internal interface IArrayReferenceType<out TArrayRef> : IArrayReferenceType
	where TArrayRef : unmanaged, IArrayReferenceType<TArrayRef>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	static abstract TArrayRef New(IntPtr value);
}