namespace Rxmxnx.JNetInterface.Restricted;

internal partial interface IAccessFeature
{
	/// <summary>
	/// This struct is designed to serve as a default <see cref="IParameterSlot"/> implementation.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private readonly struct DefaultSlot(Int32 count) : IParameterSlot
	{
		/// <summary>
		/// Internal instance.
		/// </summary>
		private readonly IObject?[] _args = new IObject?[count];

		void IParameterSlot.SetParameterValue<TObject>(Byte index, TObject? value) where TObject : default
			=> this._args[index] = value;
		void IParameterSlot.SetNullValue(Byte index) => this._args[index] = default;

		/// <summary>
		/// Defines an explicit conversion of a given <see cref="DefaultSlot"/> to <see cref="ReadOnlySpan{IObject}"/>.
		/// </summary>
		/// <param name="slot">A <see cref="DefaultSlot"/> to implicitly convert.</param>
		public static implicit operator ReadOnlySpan<IObject?>(DefaultSlot slot) => slot._args;
	}
}