using System.Diagnostics.CodeAnalysis;

using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JHelloDotnetObject
{
	[SuppressMessage("csharpsquid", "S2094:Classes should not be empty.",
	                 Justification = "Type needs class inheritance.")]
	private sealed class InitPrimitiveArrayArrayDefinition<TPrimitive>(ReadOnlySpan<Byte> functionName)
		: JFunctionDefinition<JArrayObject<JArrayObject<TPrimitive>>>(functionName, JArgumentMetadata.Get<JInt>())
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
}