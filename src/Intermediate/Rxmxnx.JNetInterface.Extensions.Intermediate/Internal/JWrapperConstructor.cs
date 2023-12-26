namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Constructor definition for wrapper class.
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
internal sealed record JWrapperConstructor<TPrimitive>()
	: JConstructorDefinition(JArgumentMetadata.Create<TPrimitive>())
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive> { }