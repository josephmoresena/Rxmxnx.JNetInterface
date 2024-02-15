namespace Rxmxnx.JNetInterface.Native;

internal partial class JPrimitiveObject
{
	/// <summary>
	/// Determines whether a specified <see cref="JPrimitiveObject"/> and a <see cref="JPrimitiveObject"/> instance
	/// have the same value.
	/// </summary>
	/// <param name="left">The <see cref="JPrimitiveObject"/> to compare.</param>
	/// <param name="right">The <see cref="JPrimitiveObject"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is the same as the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator ==(JPrimitiveObject? left, JPrimitiveObject? right)
		=> left?.Equals(right) ?? right is null;
	/// <summary>
	/// Determines whether a specified <see cref="JPrimitiveObject"/> and a <see cref="JPrimitiveObject"/> instance
	/// have different values.
	/// </summary>
	/// <param name="left">The <see cref="JPrimitiveObject"/> to compare.</param>
	/// <param name="right">The <see cref="JPrimitiveObject"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is different from the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean operator !=(JPrimitiveObject? left, JPrimitiveObject? right) => !(left == right);
}

internal sealed partial class JPrimitiveObject<TPrimitive>
{
	/// <summary>
	/// Determines whether a specified <see cref="JPrimitiveObject"/> and a <see cref="JPrimitiveObject"/> instance
	/// have the same value.
	/// </summary>
	/// <param name="left">The <see cref="JPrimitiveObject"/> to compare.</param>
	/// <param name="right">The <see cref="JPrimitiveObject"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is the same as the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator ==(JPrimitiveObject<TPrimitive>? left, JPrimitiveObject<TPrimitive>? right)
		=> left?.Equals(right as JPrimitiveObject) ?? right is null;
	/// <summary>
	/// Determines whether a specified <see cref="JPrimitiveObject"/> and a <see cref="JPrimitiveObject"/> instance
	/// have the same value.
	/// </summary>
	/// <param name="left">The <see cref="JPrimitiveObject"/> to compare.</param>
	/// <param name="right">The <see cref="JPrimitiveObject"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is the same as the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=(JPrimitiveObject<TPrimitive>? left, JPrimitiveObject<TPrimitive>? right)
		=> !(left == right);
}