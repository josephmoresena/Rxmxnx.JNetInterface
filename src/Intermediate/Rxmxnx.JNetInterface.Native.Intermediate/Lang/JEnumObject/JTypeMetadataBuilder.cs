namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// <see cref="JReferenceTypeMetadata"/> enum builder.
	/// </summary>
	/// <typeparam name="TEnum">Type of enum.</typeparam>
	protected new sealed partial class JTypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TEnum> : JTypeMetadataBuilder
		where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
	{
		/// <summary>
		/// Enum field list.
		/// </summary>
		private readonly FieldList _fields;

		/// <inheritdoc/>
		protected override JTypeKind Kind => JTypeKind.Enum;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="enumType">Enum name of current type.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private JTypeMetadataBuilder(CString enumType, ISet<Type> interfaceTypes) : base(enumType, interfaceTypes)
			=> this._fields = new(enumType);

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public new JTypeMetadataBuilder<TEnum> WithSignature(CString signature)
		{
			base.WithSignature(signature);
			return this;
		}
		/// <summary>
		/// Sets the array signature.
		/// </summary>
		/// <param name="arraySignature">Array signature.</param>
		/// <returns>Current instance.</returns>
		public new JTypeMetadataBuilder<TEnum> WithArraySignature(CString arraySignature)
		{
			base.WithArraySignature(arraySignature);
			return this;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TEnum> Implements<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			this.AppendInterface<TInterface>();
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
			this._fields.AddField(ordinal, name);
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
				this._fields.AddField(i + offset, names[i]);
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JEnumTypeMetadata Build()
			=> new JEnumGenericTypeMetadata(this.DataTypeName, this._fields.Validate(), this.CreateInterfaceSet(),
			                                this.Signature, this.ArraySignature);

		/// <inheritdoc/>
		protected override Type GetImplementingType<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TOtherInterface>()
			=> typeof(IDerivedType<TEnum, TOtherInterface>);
		/// <inheritdoc/>
		protected override Type GetImplementingType(JInterfaceTypeMetadata interfaceMetadata)
			=> interfaceMetadata.GetImplementingType<TEnum>();

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TInterface}"/> instance.</returns>
		public static JTypeMetadataBuilder<TEnum> Create(CString className)
		{
			ValidationUtilities.ValidateNotEmpty(className);
			ISet<Type> interfaceTypes = IReferenceType<TEnum>.GetInterfaceTypes().ToHashSet();
			return new(className, interfaceTypes);
		}
	}
}