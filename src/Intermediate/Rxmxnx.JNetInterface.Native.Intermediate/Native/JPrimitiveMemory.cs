namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a primitive memory block.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> in memory block.</typeparam>
public sealed partial class JPrimitiveMemory<TPrimitive> : JPrimitiveMemory, IFixedContext<TPrimitive>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Memory release mode.
	/// </summary>
	public new JReleaseMode? ReleaseMode
	{
		get => this.Copy ? base.ReleaseMode : default(JReleaseMode?);
		set
		{
			base.ReleaseMode = value switch
			{
				JReleaseMode.Abort when this.Copy => JReleaseMode.Abort,
				_ => JReleaseMode.Free,
			};
		}
	}
	/// <inheritdoc/>
	public Span<TPrimitive> Values => this._context.Values;

	/// <summary>
	/// Copies the content from the current memory back into the array.
	/// </summary>
	/// <returns>
	/// <see langword="true"/> if the current memory is not a copy or if the copy-back operation was performed;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public new Boolean Commit()
	{
		if (!this.Copy) return true;
		if (this.Critical) return false;
		base.Commit();
		return true;
	}

	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JPrimitiveMemory{TPrimitive}"/> to
	/// <see cref="JNativeMemory{TPrimitive}"/>.
	/// </summary>
	/// <param name="jPrimitiveMemory">A <see cref="JPrimitiveMemory{TPrimitive}"/> to implicitly convert.</param>
	/// <returns>A <see cref="JNativeMemory{TPrimitive}"/> instance.</returns>
	public static implicit operator JNativeMemory<TPrimitive>(JPrimitiveMemory<TPrimitive> jPrimitiveMemory)
		=> new(jPrimitiveMemory, jPrimitiveMemory._context);
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JNativeMemory{TPrimitive}"/> to
	/// <see cref="JPrimitiveMemory{TPrimitive}"/>.
	/// </summary>
	/// <param name="jNativeMemory">A <see cref="JNativeMemory{TPrimitive}"/> to explicitly convert.</param>
	/// <returns>A <see cref="JPrimitiveMemory{TPrimitive}"/> instance.</returns>
	public static explicit operator JPrimitiveMemory<TPrimitive>(JNativeMemory<TPrimitive> jNativeMemory)
		=> new(jNativeMemory, (IFixedContext<TPrimitive>)jNativeMemory.GetContext());
}