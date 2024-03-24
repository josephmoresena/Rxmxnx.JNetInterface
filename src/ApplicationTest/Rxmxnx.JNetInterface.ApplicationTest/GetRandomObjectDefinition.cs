using System.Diagnostics.CodeAnalysis;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

/// <summary>
/// Definition of <c>(I)Ljava.lang.Object</c> static java function;
/// </summary>
[ExcludeFromCodeCoverage]
public class GetRandomObjectDefinition(ReadOnlySpan<Byte> functionName)
	: JFunctionDefinition<JLocalObject>(functionName, JArgumentMetadata.Get<JInt>())
{
	public JLocalObject? Invoke(JClassObject helloDotnetClass, JInt value)
	{
		IObject?[] invokeArgs = this.CreateArgumentsArray();
		invokeArgs[0] = value;
		return this.StaticInvoke(helloDotnetClass, invokeArgs);
	}
}