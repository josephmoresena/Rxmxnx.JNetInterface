namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a <c>java.lang.Class&lt;?&gt;</c> JNI information container.
/// </summary>
public interface ITypeInformation
{
	/// <summary>
	/// JNI class name.
	/// </summary>
	CString ClassName { get; }
	/// <summary>
	/// Java class name.
	/// </summary>
	internal sealed String JavaClassName => ITypeInformation.GetJavaClassName(this);
	/// <summary>
	/// JNI signature for object instances of this type.
	/// </summary>
	internal CString Signature { get; }
	/// <summary>
	/// Current datatype hash.
	/// </summary>
	internal String Hash { get; }
	/// <summary>
	/// Kind of the current type.
	/// </summary>
	JTypeKind Kind { get; }
	/// <summary>
	/// Indicates whether current type is final;
	/// </summary>
	Boolean? IsFinal { get; }

	/// <summary>
	/// Retrieves the Java class name.
	/// </summary>
	/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
	/// <returns>A <see cref="CString"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static String GetJavaClassName(ITypeInformation typeInformation)
		=> ClassNameHelper.GetClassName(typeInformation.Signature);
	/// <summary>
	/// Retrieves the JNI signature for object instances of this type.
	/// </summary>
	/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
	/// <returns>A <see cref="CString"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CString GetClassSignature(ITypeInformation typeInformation) => typeInformation.ClassName;
	/// <summary>
	/// Retrieves the datatype hash.
	/// </summary>
	/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
	/// <returns>A <see cref="CString"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static String GetClassHash(ITypeInformation typeInformation) => typeInformation.Hash;
}