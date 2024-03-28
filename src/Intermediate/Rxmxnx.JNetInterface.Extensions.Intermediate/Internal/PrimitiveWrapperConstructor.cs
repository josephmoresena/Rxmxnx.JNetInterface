namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Constructor definition for primitive wrapper class.
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2094,
                 Justification = CommonConstants.ClassJustification)]
internal sealed class PrimitiveWrapperConstructor<TPrimitive>()
	: JConstructorDefinition(JArgumentMetadata.Get<TPrimitive>())
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive> { }