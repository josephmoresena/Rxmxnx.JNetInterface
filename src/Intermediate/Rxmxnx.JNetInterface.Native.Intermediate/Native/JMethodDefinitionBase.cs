namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a method definition base.
/// </summary>
public abstract record JMethodDefinitionBase : JAccessibleObjectDefinition
{
	/// <summary>
	/// JNI signature for void return.
	/// </summary>
	public static CString VoidReturnSignature => UnicodeMethodSignatures.VoidReturnSignature;
	/// <summary>
	/// Prefix for the parameters declaration in the JNI signature for methods.
	/// </summary>
	public static CString MethodParameterPrefix => UnicodeMethodSignatures.MethodParameterPrefix;
	/// <summary>
	/// Suffix for the parameters declaration in the JNI signature for methods.
	/// </summary>
	public static CString MethodParameterSuffix => UnicodeMethodSignatures.MethodParameterSuffix;
	/// <summary>
	/// JNI name for class constructors.
	/// </summary>
	public static CString ConstructorName => UnicodeMethodNames.ConstructorName;

	/// <summary>
	/// Total count of call parameters.
	/// </summary>
	private readonly Int32 _callCount;

	/// <summary>
	/// Total size in bytes of call parameters.
	/// </summary>
	private readonly Int32 _callSize;
	/// <summary>
	/// Indicates whether the current call must use <see cref="JValue"/> arguments.
	/// </summary>
	private readonly Boolean _useJValue;

	/// <summary>
	/// Total size in bytes of call parameters.
	/// </summary>
	public Int32 Size => this._callSize;
	/// <summary>
	/// Total count of call parameters.
	/// </summary>
	public Int32 Count => this._callCount;

	/// <summary>
	/// Indicates whether the current call must use <see cref="JValue"/> arguments.
	/// </summary>
	internal Boolean UseJValue => this._useJValue;

	/// <summary>
	/// Return type.
	/// </summary>
	internal abstract Type? Return { get; }

	/// <inheritdoc/>
	internal override String ToStringFormat => "xMethod: {0} Descriptor: {1}";

	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Method defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JMethodDefinitionBase(CString name, params JArgumentMetadata[] metadata) : this(
		name, JMethodDefinitionBase.VoidReturnSignature, metadata) { }
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Method defined name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JMethodDefinitionBase(CString name, CString returnType, params JArgumentMetadata[] metadata) : base(
		new CStringSequence(
			name, JMethodDefinitionBase.CreateDescriptor(returnType, out Int32 size, out Int32 count, metadata)))
	{
		this._callSize = size;
		this._callCount = count;
		this._useJValue = this._callCount > 1 &&
			Math.Abs(this._callCount * JValue.Size - this._callCount) <= 0.15 * this._callSize;
	}

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
	/// <inheritdoc/>
	public override Int32 GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Creates the method descriptor using <paramref name="returnSignature"/> and <paramref name="metadata"/>.
	/// </summary>
	/// <param name="returnSignature">Method return type signature.</param>
	/// <param name="totalSize">Total size in bytes of call parameters.</param>
	/// <param name="totalCount">Total count of call parameters.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>Method descriptor.</returns>
	private static CString CreateDescriptor(CString returnSignature, out Int32 totalSize, out Int32 totalCount,
		params JArgumentMetadata[] metadata)
	{
		totalCount = metadata.Length;
		totalSize = 0;

		using MemoryStream memory = new();
		memory.Write(JMethodDefinitionBase.MethodParameterPrefix);
		foreach (JArgumentMetadata arg in metadata)
		{
			memory.Write(arg.Signature);
			totalSize += arg.Size;
		}
		memory.Write(JMethodDefinitionBase.MethodParameterSuffix);
		memory.Write(returnSignature, true);
		return memory.ToArray();
	}
}