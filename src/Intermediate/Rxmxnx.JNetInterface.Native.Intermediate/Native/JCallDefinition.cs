namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a java call definition.
/// </summary>
public abstract record JCallDefinition : JAccessibleObjectDefinition
{
	/// <summary>
	/// JNI signature for void return.
	/// </summary>
	public static CString VoidReturnSignature => UnicodeMethodSignatures.VoidReturnSignature;
	/// <summary>
	/// Prefix for the parameters declaration in the JNI signature for calls.
	/// </summary>
	public static CString MethodParameterPrefix => UnicodeMethodSignatures.MethodParameterPrefix;
	/// <summary>
	/// Suffix for the parameters declaration in the JNI signature for calls.
	/// </summary>
	public static CString MethodParameterSuffix => UnicodeMethodSignatures.MethodParameterSuffix;
	/// <summary>
	/// JNI name for class constructors.
	/// </summary>
	public static CString ConstructorName => UnicodeMethodNames.ConstructorName;

	/// <summary>
	/// Total size in bytes of call parameters.
	/// </summary>
	private readonly Int32 _callSize;
	/// <summary>
	/// Call argument's size.
	/// </summary>
	private readonly Int32[] _sizes;
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
	public Int32 Count => this._sizes.Length;

	/// <summary>
	/// Indicates whether the current call must use <see cref="JValue"/> arguments.
	/// </summary>
	internal Boolean UseJValue => this._useJValue;
	/// <summary>
	/// List of size in bytes of each call argument.
	/// </summary>
	internal IReadOnlyList<Int32> Sizes => this._sizes;

	/// <summary>
	/// Call return type.
	/// </summary>
	internal abstract Type? Return { get; }

	/// <inheritdoc/>
	internal override String ToStringFormat => "xMethod: {0} Descriptor: {1}";

	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Call defined name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JCallDefinition(CString name, params JArgumentMetadata[] metadata) : this(
		name, JCallDefinition.VoidReturnSignature, metadata) { }
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Call defined name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JCallDefinition(CString name, CString returnType, params JArgumentMetadata[] metadata) : base(
		new CStringSequence(
			name, JCallDefinition.CreateDescriptor(returnType, out Int32 size, out Int32[] sizes, metadata)))
	{
		this._callSize = size;
		this._sizes = sizes;
		this._useJValue = this._sizes.Length > 1 &&
			Math.Abs(this._sizes.Length * JValue.Size - this._callSize) <= 0.15 * this._callSize;
	}

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
	/// <inheritdoc/>
	public override Int32 GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Creates the argument array for current call.
	/// </summary>
	/// <returns>A new array to be used as argument for current call.</returns>
	protected IObject?[] CreateArgumentsArray() => new IObject?[this._sizes.Length];

	/// <summary>
	/// Creates the method descriptor using <paramref name="returnSignature"/> and <paramref name="metadata"/>.
	/// </summary>
	/// <param name="returnSignature">Method return type signature.</param>
	/// <param name="totalSize">Total size in bytes of call parameters.</param>
	/// <param name="sizes">Arguments sizes.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>Method descriptor.</returns>
	private static CString CreateDescriptor(CString returnSignature, out Int32 totalSize, out Int32[] sizes,
		params JArgumentMetadata[] metadata)
	{
		totalSize = 0;
		sizes = new Int32[metadata.Length];

		using MemoryStream memory = new();
		memory.Write(JCallDefinition.MethodParameterPrefix);
		for (Int32 i = 0; i < metadata.Length; i++)
		{
			memory.Write(metadata[i].Signature);
			totalSize += metadata[i].Size;
			sizes[i] = metadata[i].Size;
		}
		memory.Write(JCallDefinition.MethodParameterSuffix);
		memory.Write(returnSignature, true);
		return memory.ToArray();
	}
}