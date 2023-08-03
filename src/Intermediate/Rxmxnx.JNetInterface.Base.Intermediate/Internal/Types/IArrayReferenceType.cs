namespace Rxmxnx.JNetInterface.Internal.Types;

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
/// <typeparam name="TArray">Type of <see cref="IArrayReferenceType{TArray}"/>.</typeparam>
internal interface IArrayReferenceType<TArray> : IArrayReferenceType, IObjectReferenceType<TArray>
	where TArray : unmanaged, IArrayReferenceType<TArray> { }