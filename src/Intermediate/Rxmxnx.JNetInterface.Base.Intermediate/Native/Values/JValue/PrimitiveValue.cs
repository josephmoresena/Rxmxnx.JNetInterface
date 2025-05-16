namespace Rxmxnx.JNetInterface.Native.Values;

internal partial struct JValue
{
	/// <summary>
	/// Primitive value struct.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct PrimitiveValue()
	{
		/// <summary>
		/// Least significant integer (4 bytes).
		/// </summary>
		private readonly Int32 _lsi = 0;
		/// <summary>
		/// Most significant integer (4 bytes).
		/// </summary>
		private readonly Int32 _msi = 0;

		/// <summary>
		/// Indicates whether current instance has the <see langword="default"/> value.
		/// </summary>
		public Boolean IsDefault => this._lsi == 0 && this._msi == 0;
	}
}