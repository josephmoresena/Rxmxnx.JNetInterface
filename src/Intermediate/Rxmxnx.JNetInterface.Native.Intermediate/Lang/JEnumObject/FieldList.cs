namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	protected new ref partial struct JTypeMetadataBuilder<TEnum>
	{
		/// <summary>
		/// Internal implementation of <see cref="IEnumFieldList"/>.
		/// </summary>
		private sealed record FieldList : IEnumFieldList
		{
			/// <summary>
			/// Enum ordinal dictionary.
			/// </summary>
			private readonly Dictionary<String, Int32> _hashDictionary = new();
			/// <summary>
			/// Enum name dictionary.
			/// </summary>
			private readonly Dictionary<String, CString> _nameDictionary = new();
			/// <summary>
			/// Enum ordinal dictionary.
			/// </summary>
			private readonly Dictionary<Int32, String> _ordinalDictionary = new();

			CString IEnumFieldList.this[Int32 ordinal] => this._nameDictionary[this._ordinalDictionary[ordinal]];
			Int32 IEnumFieldList.this[CString name] => this._hashDictionary[name.ToHexString()];
			Int32 IEnumFieldList.this[String hash] => this._hashDictionary[hash];

			Boolean IEnumFieldList.HasOrdinal(Int32 ordinal) => this._ordinalDictionary.ContainsKey(ordinal);
			Boolean IEnumFieldList.HasHash(String hash) => this._hashDictionary.ContainsKey(hash);
			IReadOnlySet<Int32> IEnumFieldList.GetMissingFields(out Int32 count, out Int32 maxOrdinal)
			{
				Int32[] defined = this._ordinalDictionary.Keys.ToArray();
				HashSet<Int32> result = Enumerable.Range(0, defined.Length + 1).ToHashSet();
				maxOrdinal = defined.Max();
				count = defined.Length;
				result.ExceptWith(defined);
				return result;
			}

			/// <summary>
			/// Adds a new enum field.
			/// </summary>
			/// <param name="enumTypeName">Enum type name.</param>
			/// <param name="ordinal">Enum ordinal.</param>
			/// <param name="name">Enum name.</param>
			public void AddField(ReadOnlySpan<Byte> enumTypeName, Int32 ordinal, CString name)
			{
				String hash = name.ToHexString();
				NativeValidationUtilities.ThrowIfInvalidOrdinal(enumTypeName, this, ordinal);
				NativeValidationUtilities.ThrowIfInvalidHash(enumTypeName, this, hash);
				this._ordinalDictionary.Add(ordinal, hash);
				this._hashDictionary.Add(hash, ordinal);
				this._nameDictionary.Add(hash, name);
			}
			/// <summary>
			/// Validates and returns the current instance.
			/// </summary>
			/// <param name="enumTypeName">Enum type name.</param>
			/// <returns>The current instance.</returns>
			public FieldList Validate(ReadOnlySpan<Byte> enumTypeName)
			{
				NativeValidationUtilities.ThrowIfInvalidList(enumTypeName, this);
				return this;
			}

			/// <inheritdoc/>
			public override String ToString()
				=> $"[{String.Join(", ", this._ordinalDictionary.Select(p => $"{{ {p.Key}, {this._nameDictionary[p.Value]} }}"))}]";
		}
	}
}