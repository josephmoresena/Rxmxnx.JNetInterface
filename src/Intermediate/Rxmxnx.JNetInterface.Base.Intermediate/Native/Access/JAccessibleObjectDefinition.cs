namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a <c>java.lang.Class&lt;?&gt;</c> accessible object definition.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS4035,
                 Justification = CommonConstants.InternalInheritanceJustification)]
public abstract class JAccessibleObjectDefinition : IEquatable<JAccessibleObjectDefinition>
{
	/// <summary>
	/// Internal <see cref="CStringSequence"/> containing the name and descriptor of accessible object.
	/// </summary>
	private readonly CStringSequence _sequence;

	/// <summary>
	/// Definition name.
	/// </summary>
	public CString Name => this._sequence[0];
	/// <summary>
	/// Definition descriptor.
	/// </summary>
	public CString Descriptor => this._sequence[1];
	/// <summary>
	/// Definition hash.
	/// </summary>
	public String Hash => this._sequence.ToString();

	/// <summary>
	/// Accessible object information.
	/// </summary>
	internal CStringSequence Information => this._sequence;

	/// <summary>
	/// The format used for <see cref="JAccessibleObjectDefinition.ToString()"/> method.
	/// </summary>
	private protected abstract String ToStringFormat { get; }

	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="sequence">
	/// <see cref="CStringSequence"/> containing the name and descriptor of the accessible object.
	/// </param>
	private protected JAccessibleObjectDefinition(CStringSequence sequence) => this._sequence = sequence;
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="definition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	private protected JAccessibleObjectDefinition(JAccessibleObjectDefinition definition)
		=> this._sequence = definition._sequence;

	/// <inheritdoc/>
	public Boolean Equals(JAccessibleObjectDefinition? other)
	{
		if (other is null) return false;
		if (Object.ReferenceEquals(this, other)) return true;
		return this._sequence.Equals(other._sequence) && this.ToStringFormat == other.ToStringFormat;
	}

	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => this.Equals(obj as JAccessibleObjectDefinition);
	/// <inheritdoc/>
	public override String ToString() => String.Format(this.ToStringFormat, this._sequence[0], this._sequence[1]);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._sequence.GetHashCode();

	/// <inheritdoc cref="Object.ToString()"/>
	/// <remarks>Use this method for trace.</remarks>
	public abstract String ToTraceText();

	/// <summary>
	/// Determines whether a specified <see cref="JAccessibleObjectDefinition"/> and a
	/// <see cref="JAccessibleObjectDefinition"/> instance
	/// have the same value.
	/// </summary>
	/// <param name="left">The <see cref="JAccessibleObjectDefinition"/> to compare.</param>
	/// <param name="right">The <see cref="JAccessibleObjectDefinition"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is the same as the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator ==(JAccessibleObjectDefinition? left, JAccessibleObjectDefinition? right)
		=> left?.Equals(right) ?? right is null;
	/// <summary>
	/// Determines whether a specified <see cref="JAccessibleObjectDefinition"/> and a
	/// <see cref="JAccessibleObjectDefinition"/> instance
	/// have different values.
	/// </summary>
	/// <param name="left">The <see cref="JAccessibleObjectDefinition"/> to compare.</param>
	/// <param name="right">The <see cref="JAccessibleObjectDefinition"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is different from the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=(JAccessibleObjectDefinition? left, JAccessibleObjectDefinition? right)
		=> !(left == right);
	/// <summary>
	/// Retrieves a valid signature from <paramref name="signature"/>.
	/// </summary>
	/// <param name="signature">A signature to validate.</param>
	/// <returns><paramref name="signature"/> if is a valid signature.</returns>
	protected static ReadOnlySpan<Byte> ValidateSignature(ReadOnlySpan<Byte> signature)
	{
		ValidationUtilities.ThrowIfInvalidSignature(signature, false);
		return signature;
	}
}