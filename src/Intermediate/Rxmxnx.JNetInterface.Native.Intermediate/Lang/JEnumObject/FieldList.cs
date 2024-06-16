namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	protected new ref partial struct TypeMetadataBuilder<TEnum>
	{
		/// <summary>
		/// Internal implementation of <see cref="IEnumFieldList"/>.
		/// </summary>
		private sealed class EnumFieldList : IEnumFieldList, IAppendableProperty
		{
			/// <summary>
			/// Enum ordinal dictionary.
			/// </summary>
			private readonly Dictionary<String, Int32> _hashDictionary = [];
			/// <summary>
			/// Enum name dictionary.
			/// </summary>
			private readonly Dictionary<String, CString> _nameDictionary = [];
			/// <summary>
			/// Enum ordinal dictionary.
			/// </summary>
			private readonly Dictionary<Int32, String> _ordinalDictionary = [];
			/// <inheritdoc/>
			public String PropertyName => nameof(JEnumTypeMetadata.Fields);
			/// <inheritdoc/>
			public void AppendValue(StringBuilder stringBuilder)
			{
				Boolean first = true;
				stringBuilder.Append(MetadataTextUtilities.OpenArray);
				foreach (Int32 ordinal in this._ordinalDictionary.Keys)
				{
					if (!first)
						stringBuilder.Append(MetadataTextUtilities.Separator);
					first = false;
					stringBuilder.Append(MetadataTextUtilities.OpenObject);
					stringBuilder.Append(ordinal);
					stringBuilder.Append(MetadataTextUtilities.Separator);
					stringBuilder.Append($"{this[ordinal]}");
					stringBuilder.Append(MetadataTextUtilities.CloseObject);
				}
				stringBuilder.Append(MetadataTextUtilities.CloseArray);
			}

			/// <inheritdoc/>
			public Int32 Count => this._nameDictionary.Count;
			/// <inheritdoc/>
			public CString this[Int32 ordinal] => this._nameDictionary[this._ordinalDictionary[ordinal]];
			/// <inheritdoc/>
			public Int32 this[ReadOnlySpan<Byte> name] => this._hashDictionary[Convert.ToHexString(name).ToLower()];
			/// <inheritdoc/>
			public Int32 this[String hash] => this._hashDictionary[hash];

			/// <inheritdoc/>
			public Boolean HasOrdinal(Int32 ordinal) => this._ordinalDictionary.ContainsKey(ordinal);
			/// <inheritdoc/>
			public Boolean HasHash(String hash) => this._hashDictionary.ContainsKey(hash);
			/// <inheritdoc/>
			public IReadOnlySet<Int32> GetMissingFields(out Int32 count, out Int32 maxOrdinal)
			{
				Int32[] defined = [.. this._ordinalDictionary.Keys,];
				HashSet<Int32> result = Enumerable.Range(0, defined.Length).ToHashSet();
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
			public EnumFieldList Validate(ReadOnlySpan<Byte> enumTypeName)
			{
				if (IVirtualMachine.MetadataValidationEnabled)
					NativeValidationUtilities.ThrowIfInvalidList(enumTypeName, this);
				return this;
			}
		}
	}
}