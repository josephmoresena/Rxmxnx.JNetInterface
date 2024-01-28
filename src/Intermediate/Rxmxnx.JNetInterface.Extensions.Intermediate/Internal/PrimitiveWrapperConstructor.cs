namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Constructor definition for primitive wrapper class.
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
internal sealed record PrimitiveWrapperConstructor<TPrimitive>()
	: JConstructorDefinition(JArgumentMetadata.Get<TPrimitive>())
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive> { }