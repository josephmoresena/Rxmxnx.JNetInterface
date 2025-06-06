﻿namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a <c>java.lang.Class&lt;?&gt;</c> accessible object definition.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS4035,
                 Justification = CommonConstants.InternalInheritanceJustification)]
#endif
public abstract partial class JAccessibleObjectDefinition : IEquatable<JAccessibleObjectDefinition>
{
	/// <summary>
	/// Definition name.
	/// </summary>
	public CString Name => this.Information.Name;
	/// <summary>
	/// Definition descriptor.
	/// </summary>
	public CString Descriptor => this.Information.Descriptor;
	/// <summary>
	/// Definition hash.
	/// </summary>
	public String Hash => this.Information.ToString();

	/// <summary>
	/// Accessible object information.
	/// </summary>
	internal AccessibleInfoSequence Information { get; }

	/// <summary>
	/// The format used for <see cref="JAccessibleObjectDefinition.ToString()"/> method.
	/// </summary>
	private protected abstract String ToStringFormat { get; }

	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="info">
	/// <see cref="AccessibleInfoSequence"/> containing the name and descriptor of the accessible object.
	/// </param>
	private protected JAccessibleObjectDefinition(AccessibleInfoSequence info) => this.Information = info;
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="definition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	private protected JAccessibleObjectDefinition(JAccessibleObjectDefinition definition)
		=> this.Information = definition.Information;

	/// <inheritdoc/>
	public Boolean Equals(JAccessibleObjectDefinition? other)
	{
		if (other is null) return false;
		if (Object.ReferenceEquals(this, other)) return true;
		return this.Information.Equals(other.Information) && this.ToStringFormat == other.ToStringFormat;
	}

	/// <inheritdoc/>
	public sealed override Boolean Equals(Object? obj) => this.Equals(obj as JAccessibleObjectDefinition);
	/// <inheritdoc/>
	public sealed override String ToString()
		=> String.Format(this.ToStringFormat, this.Information.Name, this.Information.Descriptor);
	/// <inheritdoc/>
	public sealed override Int32 GetHashCode() => this.Information.GetHashCode();

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
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
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
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=(JAccessibleObjectDefinition? left, JAccessibleObjectDefinition? right)
		=> !(left == right);

	/// <summary>
	/// Retrieves a valid signature from <paramref name="signature"/>.
	/// </summary>
	/// <param name="signature">A signature to validate.</param>
	/// <returns><paramref name="signature"/> if is a valid signature.</returns>
	private protected static ReadOnlySpan<Byte> ValidateSignature(ReadOnlySpan<Byte> signature)
	{
		CommonValidationUtilities.ThrowIfInvalidObjectSignature(signature);
		return signature;
	}
}