using System.Diagnostics.CodeAnalysis;
using System.Numerics;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JHelloDotnetObject
{
	[SuppressMessage("csharpsquid", "S2094:Classes should not be empty.",
	                 Justification = "Type needs class inheritance.")]
	private sealed class PrimitiveSumArrayDefinition<TNumber, TPrimitive>(ReadOnlySpan<Byte> functionName)
		: JFunctionDefinition<TNumber>(functionName, JArgumentMetadata.Get<JArrayObject<TPrimitive>>())
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
		where TNumber : JNumberObject<TPrimitive, TNumber>, IPrimitiveWrapperType<TNumber, TPrimitive>;
}