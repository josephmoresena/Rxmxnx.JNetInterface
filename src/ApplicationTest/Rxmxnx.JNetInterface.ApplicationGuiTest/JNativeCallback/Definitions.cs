using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface;

public partial class JNativeCallback
{
	private static readonly JMethodDefinition finalizeDef = (JMethodDefinition)IndeterminateCall.CreateMethodDefinition(
		"finalize"u8, [
			JArgumentMetadata.Get<JLong>(),
			JArgumentMetadata.Get<JLong>(),
		]).Definition;
	private static readonly JFunctionDefinition<JStringObject>.Parameterless getExceptionMessageDef =
		new("getExceptionMessage"u8);
	private static readonly IndeterminateCall constructorDef = IndeterminateCall.CreateConstructorDefinition([
		JArgumentMetadata.Get<JLong>(), JArgumentMetadata.Get<JLong>(),
	]);
}