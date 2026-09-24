using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Functional;
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
#if !NET9_0_OR_GREATER
		=> this.StaticInvoke(helloDotnetClass, in Unsafe.As<JInt, JIntArg>(ref value));
#else
		=> this.StaticInvoke(helloDotnetClass, in Unsafe.As<JInt, JIntArg>(ref value));
#endif

	[StructLayout(LayoutKind.Sequential)]
	private readonly struct JIntArg : ICallArgument
	{
		private readonly JInt _value;

		public void Configure<TSlot>(TSlot slot, JCallDefinition _) where TSlot : IParameterSlot
		{
			slot.SetParameterValue(0, this._value);
		}
	}
}