namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java array local reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
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
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IArrayReferenceType<TArray> : IArrayReferenceType, IObjectReferenceType<TArray>
	where TArray : unmanaged, IArrayReferenceType<TArray>
{
	/// <summary>
	/// Converts a given <see cref="JArrayLocalRef"/> to <typeparamref name="TArray"/> instance.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> to convert.</param>
	internal static abstract TArray FromReference(in JArrayLocalRef arrayRef);
}