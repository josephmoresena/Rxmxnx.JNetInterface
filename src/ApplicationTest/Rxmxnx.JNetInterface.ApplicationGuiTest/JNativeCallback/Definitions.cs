using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JNativeCallback
{
	private static readonly JMethodDefinition finalizeDefinition = (JMethodDefinition)IndeterminateCall
		.CreateMethodDefinition("finalize"u8, [JArgumentMetadata.Get<JLong>(), JArgumentMetadata.Get<JLong>(),])
		.Definition;
	private static readonly IndeterminateCall constructorDefinition = IndeterminateCall.CreateConstructorDefinition([
		JArgumentMetadata.Get<JLong>(), JArgumentMetadata.Get<JLong>(),
	]);
}