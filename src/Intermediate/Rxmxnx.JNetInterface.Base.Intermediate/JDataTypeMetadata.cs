namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata for a reference <see cref="IDataType"/> type.
/// </summary>
public abstract record JDataTypeMetadata
{
	/// <summary>
	/// Class name of current type.
	/// </summary>
	[ReadOnly(true)]
	public abstract CString ClassName { get; }
	/// <summary>
	/// JNI signature for current type type.
	/// </summary>
	[ReadOnly(true)]
	public abstract CString Signature { get; }
	/// <summary>
	/// JNI signature for an array of current type.
	/// </summary>
	[ReadOnly(true)]
	public abstract CString ArraySignature { get; }
	/// <summary>
	/// Class JNI signature for current type type.
	/// </summary>
	[ReadOnly(true)]
	public abstract CString ClassSignature { get; }
	/// <summary>
	/// Size of current type in bytes.
	/// </summary>
	[ReadOnly(true)]
	public abstract Int32 SizeOf { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">CLR type of <see cref="IDataType"/>.</param>
	internal JDataTypeMetadata(Type type) { }
}