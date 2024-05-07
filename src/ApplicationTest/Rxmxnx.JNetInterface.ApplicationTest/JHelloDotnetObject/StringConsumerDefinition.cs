using System.Diagnostics.CodeAnalysis;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
	[SuppressMessage("csharpsquid", "S2094:Classes should not be empty.",
	                 Justification = "Type needs class inheritance.")]
	private sealed class StringConsumerDefinition(ReadOnlySpan<Byte> methodName)
		: JMethodDefinition(methodName, JArgumentMetadata.Get<JStringObject>());
}