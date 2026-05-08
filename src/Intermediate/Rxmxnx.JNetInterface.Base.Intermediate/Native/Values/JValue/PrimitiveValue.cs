namespace Rxmxnx.JNetInterface.Native.Values;

#if !PACKAGE
public partial struct JValue
#else
internal unsafe partial struct JValue
#endif
{
	/// <summary>
	/// Primitive value struct.
	/// </summary>
	[StructLayout(LayoutKind.Explicit, Size = sizeof(Double), Pack = 0)]
	public readonly struct PrimitiveValue()
	{
		/// <summary>
		/// Least significant integer (4 bytes).
		/// </summary>
		[FieldOffset(0)]
		private readonly Int32 _lsi = 0;
		/// <summary>
		/// Most significant integer (4 bytes).
		/// </summary>
		[FieldOffset(sizeof(Int32))]
		private readonly Int32 _msi = 0;

		/// <summary>
		/// Indicates whether current instance has the <see langword="default"/> value.
		/// </summary>
		public Boolean IsDefault => this._lsi == 0 && this._msi == 0;
	}
}