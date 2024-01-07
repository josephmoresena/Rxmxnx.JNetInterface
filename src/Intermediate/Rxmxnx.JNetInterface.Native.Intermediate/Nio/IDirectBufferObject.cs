namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// Interface for <see cref="JDirectBufferObject"/>
/// </summary>
public interface IDirectBufferObject : IInterfaceObject<JDirectBufferObject>;

/// <summary>
/// Interface for <see cref="JDirectBufferObject"/>
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
public interface IDirectBufferObject<TPrimitive> : IDirectBufferObject
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>;