namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// Interface for <see cref="JDirectBufferObject"/>
/// </summary>
public interface IDirectBufferObject : IInterfaceObject<JDirectBufferObject>
{
	/// <summary>
	/// Direct buffer address.
	/// </summary>
	IntPtr Address { get; }
	/// <inheritdoc cref="JBufferObject.Capacity"/>
	Int64 Capacity { get; }

	/// <summary>
	/// Internal buffer.
	/// </summary>
	internal JBufferObject InternalBuffer { get; }

	/// <summary>
	/// Creates a <see cref="IFixedMemory"/> instance from current direct buffer.
	/// </summary>
	/// <returns>A <see cref="IFixedMemory"/> instance from current direct buffer.</returns>
	IFixedMemory AsFixedMemory();
}

/// <summary>
/// Interface for <see cref="JDirectBufferObject"/>
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
public interface IDirectBufferObject<TPrimitive> : IDirectBufferObject
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>
{
	IFixedMemory IDirectBufferObject.AsFixedMemory() => this.GetFixedContext();

	/// <summary>
	/// Creates a <see cref="IFixedContext{TPrimitive}"/> instance from current direct buffer.
	/// </summary>
	/// <returns>A <see cref="IFixedContext{TPrimitive}"/> instance from current direct buffer.</returns>
	IFixedContext<TPrimitive> GetFixedContext()
		=> ((ValPtr<TPrimitive>)this.Address).GetUnsafeFixedContext((Int32)this.Capacity);
}