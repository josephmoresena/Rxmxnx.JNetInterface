using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
	private sealed record StringConsumerDefinition : JMethodDefinition
	{
		public StringConsumerDefinition(ReadOnlySpan<Byte> methodName) : base(
			methodName, JArgumentMetadata.Get<JStringObject>()) { }
	}
}