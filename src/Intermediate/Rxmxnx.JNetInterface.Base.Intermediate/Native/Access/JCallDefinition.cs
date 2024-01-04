namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a java call definition.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract record JCallDefinition : JAccessibleObjectDefinition
{
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
	internal JCallDefinition(ReadOnlySpan<Byte> name, params JArgumentMetadata[] metadata) : this(
		name, stackalloc Byte[1] { UnicodeMethodSignatures.VoidReturnSignatureChar, }, metadata) { }
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="name">Call defined name.</param>
	/// <param name="returnType">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	internal JCallDefinition(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> returnType,
		params JArgumentMetadata[] metadata) : base(name.WithSafeFixed(
			                                            JCallDefinition.CreateDescriptor(
				                                            returnType, out Int32 size, out Int32[] sizes, metadata),
			                                            JCallDefinition.CreateSequence))
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
	protected IObject?[] CreateArgumentsArray()
		=> this._sizes.Length != 0 ? new IObject?[this._sizes.Length] : Array.Empty<IObject?>();

	/// <summary>
	/// Creates the method descriptor using <paramref name="returnSignature"/> and <paramref name="metadata"/>.
	/// </summary>
	/// <param name="returnSignature">Method return type signature.</param>
	/// <param name="totalSize">Total size in bytes of call parameters.</param>
	/// <param name="sizes">Arguments sizes.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>Method descriptor.</returns>
	private static CString CreateDescriptor(ReadOnlySpan<Byte> returnSignature, out Int32 totalSize, out Int32[] sizes,
		params JArgumentMetadata[] metadata)
	{
		totalSize = 0;
		sizes = new Int32[metadata.Length];

		using MemoryStream memory = new();
		memory.WriteByte(UnicodeMethodSignatures.MethodParameterPrefixChar);
		for (Int32 i = 0; i < metadata.Length; i++)
		{
			memory.Write(metadata[i].Signature);
			totalSize += metadata[i].Size;
			sizes[i] = metadata[i].Size;
		}
		memory.WriteByte(UnicodeMethodSignatures.MethodParameterSuffixChar);
		memory.Write(returnSignature);
		memory.WriteByte(default);
		return memory.ToArray();
	}
	/// <summary>
	/// Creates a call sequence.
	/// </summary>
	/// <param name="memName">A <see cref="IReadOnlyFixedMemory"/> containing name.</param>
	/// <param name="descriptor">A <see cref="CString"/> containing call descriptor.</param>
	/// <returns>A <see cref="CStringSequence"/> instance.</returns>
	private static CStringSequence CreateSequence(in IReadOnlyFixedMemory memName, CString descriptor)
		=> CStringSequence.Create((memName, descriptor), JCallDefinition.CreateSequence, memName.Bytes.Length,
		                          descriptor.Length);
	/// <summary>
	/// Creates a call sequence.
	/// </summary>
	/// <param name="span">A span of bytes.</param>
	/// <param name="index">Index of current sequence item.</param>
	/// <param name="arg">Creation instance.</param>
	private static void CreateSequence(Span<Byte> span, Int32 index,
		(IReadOnlyFixedMemory memName, CString descriptor) arg)
	{
		if (index == 0)
			arg.memName.Bytes.CopyTo(span);
		else
			arg.descriptor.AsSpan().CopyTo(span);
	}
}