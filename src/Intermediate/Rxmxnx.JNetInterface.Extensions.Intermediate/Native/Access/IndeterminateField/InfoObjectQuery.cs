namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
public partial class IndeterminateField
{
	/// <summary>
	/// This struct stores query for Object to Primitive validation.
	/// </summary>
#if !PACKAGE
	internal readonly struct InfoObjectQuery
#else
	private readonly struct InfoObjectQuery
#endif
	{
		/// <summary>
		/// JNI primitive signature.
		/// </summary>
		public Byte PrimitiveSignature { get; init; }
		/// <summary>
		/// <c>java.lang.Object</c> instance.
		/// </summary>
		public JReferenceObject Object { get; init; }
	}
}