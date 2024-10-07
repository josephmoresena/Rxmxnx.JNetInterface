namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Constructor definition for primitive wrapper class.
/// </summary>
/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2094,
                 Justification = CommonConstants.ClassJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal sealed unsafe class PrimitiveWrapperConstructor<TPrimitive>(AccessibleInfoSequence info)
	: JConstructorDefinition(info, PrimitiveWrapperConstructor<TPrimitive>.sizes[0],
	                         PrimitiveWrapperConstructor<TPrimitive>.sizes, 0)
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
{
	/// <summary>
	/// Call sizes.
	/// </summary>
	private static readonly Int32[] sizes = [sizeof(TPrimitive),];
}