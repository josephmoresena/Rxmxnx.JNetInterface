namespace Rxmxnx.JNetInterface.Native.Access;

public partial class IndeterminateField
{
	/// <summary>
	/// This struct stores query for Object to Primitive validation.
	/// </summary>
#if !PACKAGE
	internal
#else
		private
#endif
		readonly struct InfoObjectQuery
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