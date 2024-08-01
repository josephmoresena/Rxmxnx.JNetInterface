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
		get => !this.Critical ? base.ReleaseMode : default(JReleaseMode?);
		set
		{
			if (this.Critical || !value.HasValue)
				return;
			base.ReleaseMode = value.GetValueOrDefault();
		}
	}
	/// <inheritdoc/>
	public Span<TPrimitive> Values => this._context.Values;

	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JPrimitiveMemory{TPrimitive}"/> to
	/// <see cref="JNativeMemory{TPrimitive}"/>.
	/// </summary>
	/// <param name="jPrimitiveMemory">A <see cref="JPrimitiveMemory{TPrimitive}"/> instance.</param>
	/// <returns>A <see cref="JNativeMemory{TPrimitive}"/> instance.</returns>
	public static implicit operator JNativeMemory<TPrimitive>(JPrimitiveMemory<TPrimitive> jPrimitiveMemory)
		=> new(jPrimitiveMemory, jPrimitiveMemory._context);
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JNativeMemory{TPrimitive}"/> to
	/// <see cref="JPrimitiveMemory{TPrimitive}"/>.
	/// </summary>
	/// <param name="jNativeMemory">A <see cref="JNativeMemory{TPrimitive}"/> instance.</param>
	/// <returns>A <see cref="JPrimitiveMemory{TPrimitive}"/> instance.</returns>
	public static explicit operator JPrimitiveMemory<TPrimitive>(JNativeMemory<TPrimitive> jNativeMemory)
		=> new(jNativeMemory, (IFixedContext<TPrimitive>)jNativeMemory.GetContext());
}