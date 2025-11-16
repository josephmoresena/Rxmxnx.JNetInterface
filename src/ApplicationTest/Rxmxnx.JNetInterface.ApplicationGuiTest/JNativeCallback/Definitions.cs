using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface;

public partial class JNativeCallback
{
	private static readonly JMethodDefinition finalizeDef = (JMethodDefinition)IndeterminateCall.CreateMethodDefinition(
		"finalize"u8,
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JLong>(), JArgumentMetadata.Get<JLong>(),]
#else
		JArgumentMetadata.Get<JLong>(), JArgumentMetadata.Get<JLong>()
#endif
	).Definition;
	private static readonly JFunctionDefinition<JStringObject>.Parameterless getExceptionMessageDef =
		new("getExceptionMessage"u8);
	private static readonly IndeterminateCall constructorDef = IndeterminateCall.CreateConstructorDefinition(
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JLong>(), JArgumentMetadata.Get<JLong>(),]
#else
		JArgumentMetadata.Get<JLong>(), JArgumentMetadata.Get<JLong>()
#endif
	);
}