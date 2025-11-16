namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java native pointer type.
/// </summary>
internal interface INativePointerType : IFixedPointer, INativeType;

/// <summary>
/// This interface exposes a java native pointer type.
/// </summary>
/// <typeparam name="TPointer">Type of native pointer type.</typeparam>
internal interface INativePointerType<out TPointer> : INativePointerType where TPointer : INativePointerType<TPointer>
{
	/// <summary>
	/// Creates a <typeparamref name="TPointer"/> from a <see cref="IntPtr"/> value.
	/// </summary>
	/// <param name="value">A <see cref="IntPtr"/> value.</param>
	/// <returns>A new <typeparamref name="TPointer"/> instance.</returns>
	static abstract TPointer New(IntPtr value);
}