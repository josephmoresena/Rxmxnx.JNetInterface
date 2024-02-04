namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// <see cref="JReferenceTypeMetadata"/> enum builder.
	/// </summary>
	/// <typeparam name="TEnum">Type of enum.</typeparam>
	protected new ref partial struct JTypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TEnum>
		where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
	{
		/// <summary>
		/// A <see cref="JLocalObject.JTypeMetadataBuilder"/> instance.
		/// </summary>
		private JTypeMetadataBuilder _builder;
		/// <summary>
		/// Enum field list.
		/// </summary>
		private readonly FieldList _fields;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="enumTypeName">Enum name of current type.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private JTypeMetadataBuilder(ReadOnlySpan<Byte> enumTypeName, ISet<Type> interfaceTypes)
		{
			this._builder = new(enumTypeName, JTypeKind.Enum, interfaceTypes);
			this._fields = new();
		}

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TEnum> WithSignature(CString signature)
		{
			this._builder.WithSignature(signature);
			return this;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TEnum> Implements<TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			this._builder.AppendInterface<TInterface>();
			return this;
		}
		/// <summary>
		/// Appends an enum value to current type definition.
		/// </summary>
		/// <param name="ordinal">Enum value ordinal.</param>
		/// <param name="name">Enum value name.</param>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TEnum> AppendValue(Int32 ordinal, CString name)
		{
			this._fields.AddField(this._builder.DataTypeName, ordinal, name);
			return this;
		}
		/// <summary>
		/// Appends all enum values to current type.
		/// </summary>
		/// <param name="offset">Enum value ordinal offset.</param>
		/// <param name="names">Enum value names.</param>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TEnum> AppendValues(Int32 offset = 0, params CString[] names)
		{
			for (Int32 i = 0; i < names.Length; i++)
				this._fields.AddField(this._builder.DataTypeName, i + offset, names[i]);
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JEnumTypeMetadata Build()
			=> new JEnumGenericTypeMetadata(this._builder, this._fields.Validate(this._builder.DataTypeName));

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TInterface}"/> instance.</returns>
		public static JTypeMetadataBuilder<TEnum> Create(ReadOnlySpan<Byte> className)
		{
			ValidationUtilities.ValidateNotEmpty(className);
			ISet<Type> interfaceTypes = IReferenceType<TEnum>.GetInterfaceTypes().ToHashSet();
			return new(className, interfaceTypes);
		}
	}
}