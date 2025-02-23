using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

/// <summary>
/// Definition of <c>(I)Ljava.lang.Object</c> static java function;
/// </summary>
public class GetObjectDefinition : JFunctionDefinition<JLocalObject>
{
	/// <summary>
	/// Instance.
	/// </summary>
	public static readonly GetObjectDefinition Instance = new();

	/// <summary>
	/// Private constructor.
	/// </summary>
	private GetObjectDefinition() : base("getObject"u8, JArgumentMetadata.Get<JInt>()) { }

	public JLocalObject? Invoke(JClassObject helloDotnetClass, JInt value)
		=> this.StaticInvoke(helloDotnetClass, [value,]);
}