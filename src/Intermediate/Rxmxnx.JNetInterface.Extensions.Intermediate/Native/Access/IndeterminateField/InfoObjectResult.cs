namespace Rxmxnx.JNetInterface.Native.Access;

public partial class IndeterminateField
{
	/// <summary>
	/// This struct stores result of Object to Primitive validation.
	/// </summary>
#if !PACKAGE
	internal
#else
		private
#endif
		readonly struct InfoObjectResult
	{
		/// <summary>
		/// Indicates whether object is <c>java.lang.Boolean</c>.
		/// </summary>
		public Boolean IsBoolean { get; init; }
		/// <summary>
		/// Indicates whether object is <c>java.lang.Character</c>.
		/// </summary>
		public Boolean IsCharacter { get; init; }
		/// <summary>
		/// Indicates whether object is <c>java.lang.Number</c>.
		/// </summary>
		public Boolean IsNumber { get; init; }
	}
}