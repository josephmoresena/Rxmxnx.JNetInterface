using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
	private sealed class StringConsumerDefinition(ReadOnlySpan<Byte> methodName)
		: JMethodDefinition(methodName, JArgumentMetadata.Get<JStringObject>());
}