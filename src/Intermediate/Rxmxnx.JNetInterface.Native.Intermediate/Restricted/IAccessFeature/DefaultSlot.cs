namespace Rxmxnx.JNetInterface.Restricted;

internal partial interface IAccessFeature
{
	/// <summary>
	/// This struct is designed to serve as a default <see cref="IParameterSlot"/> implementation.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private readonly struct DefaultSlot(JCallDefinition definition) : IParameterSlot
	{
		/// <summary>
		/// Internal instance.
		/// </summary>
		private readonly IObject?[] _args = definition.Count > 0 ? new IObject?[definition.Count] : [];

		JCallDefinition IParameterSlot.CallDefinition => definition;

		void IParameterSlot.SetParameterValue<TObject>(Byte index, TObject? value) where TObject : default
			=> this._args[index] = value;
		void IParameterSlot.SetParameterDefault(Byte index) => this._args[index] = default;

		/// <summary>
		/// Defines an explicit conversion of a given <see cref="DefaultSlot"/> to <see cref="ReadOnlySpan{IObject}"/>.
		/// </summary>
		/// <param name="slot">A <see cref="DefaultSlot"/> to implicitly convert.</param>
		public static implicit operator ReadOnlySpan<IObject?>(DefaultSlot slot) => slot._args;
	}
}