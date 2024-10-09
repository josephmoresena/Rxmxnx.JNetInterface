namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	private sealed partial class FieldList
	{
		/// <summary>
		/// Enum ordinal dictionary.
		/// </summary>
		private readonly Dictionary<String, Int32> _hashDictionary;
		/// <summary>
		/// Enum name dictionary.
		/// </summary>
		private readonly Dictionary<String, CString> _nameDictionary;
		/// <summary>
		/// Enum ordinal dictionary.
		/// </summary>
		private readonly Dictionary<Int32, String> _ordinalDictionary;

		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		public FieldList()
		{
			this._hashDictionary = [];
			this._nameDictionary = [];
			this._ordinalDictionary = [];
		}
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="values">Values span.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public FieldList(ReadOnlySpan<CString> values)
		{
			this._hashDictionary = new(values.Length);
			this._nameDictionary = new(values.Length);
			this._ordinalDictionary = new(values.Length);

			for (Int32 i = 0; i < values.Length; i++)
			{
				String hash = values[i].ToHexString();

				this._hashDictionary.Add(hash, i);
				this._nameDictionary.Add(hash, values[i]);
				this._ordinalDictionary.Add(i, hash);
			}
		}
	}
}