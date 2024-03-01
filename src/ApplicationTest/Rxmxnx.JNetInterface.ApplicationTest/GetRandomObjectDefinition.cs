using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public record GetRandomObjectDefinition : JFunctionDefinition<JLocalObject>
{
	public GetRandomObjectDefinition(ReadOnlySpan<Byte> functionName) : base(
		functionName, JArgumentMetadata.Get<JInt>()) { }

	public JLocalObject? Invoke(JClassObject helloDotnetClass, JInt value)
	{
		IObject?[] invokeArgs = this.CreateArgumentsArray();
		invokeArgs[0] = value;
		return this.StaticInvoke(helloDotnetClass, invokeArgs);
	}
}