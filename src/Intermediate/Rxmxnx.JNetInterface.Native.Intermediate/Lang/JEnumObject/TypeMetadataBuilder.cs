namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// <see cref="JReferenceTypeMetadata"/> enum builder.
	/// </summary>
	/// <typeparam name="TEnum">Type of enum.</typeparam>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	protected new ref struct TypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TEnum>
		where TEnum : JEnumObject<TEnum>, IEnumType<TEnum>
	{
		/// <summary>
		/// A <see cref="JLocalObject.TypeMetadataBuilder"/> instance.
		/// </summary>
		private TypeMetadataBuilder _builder;
		/// <summary>
		/// Enum field list.
		/// </summary>
		private readonly EnumFieldList _enumFields;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="enumTypeName">Enum name of the current type.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private TypeMetadataBuilder(ReadOnlySpan<Byte> enumTypeName, IReadOnlySet<Type> interfaceTypes)
		{
			this._builder = new(enumTypeName, JTypeKind.Enum, interfaceTypes);
			this._enumFields = new();
		}

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public TypeMetadataBuilder<TEnum> Implements<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
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
		public readonly TypeMetadataBuilder<TEnum> AppendValue(Int32 ordinal, CString name)
		{
			this._enumFields.AddField(this._builder.DataTypeName, ordinal, name);
			return this;
		}
		/// <summary>
		/// Appends all enum values to the current type.
		/// </summary>
		/// <param name="offset">Enum value ordinal offset.</param>
		/// <param name="names">Enum value names.</param>
		/// <returns>Current instance.</returns>
		public readonly TypeMetadataBuilder<TEnum> AppendValues(Int32 offset, ReadOnlySpan<CString> names)
		{
			for (Int32 i = 0; i < names.Length; i++)
				this._enumFields.AddField(this._builder.DataTypeName, i + offset, names[i]);
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JEnumTypeMetadata{TEnum}"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JEnumTypeMetadata{TEnum}"/> instance.</returns>
		public readonly JEnumTypeMetadata<TEnum> Build()
			=> new EnumTypeMetadata<TEnum>(this._builder, this._enumFields.Validate(this._builder.DataTypeName));

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of the current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TEnum}"/> instance.</returns>
		public static TypeMetadataBuilder<TEnum> Create(ReadOnlySpan<Byte> className)
		{
			CommonValidationUtilities.ValidateNotEmpty(className);
			IReadOnlySet<Type> interfaceTypes = ImmutableHashSet<Type>.Empty;
			if (IVirtualMachine.MetadataValidationEnabled) interfaceTypes = IReferenceType<TEnum>.TypeInterfaces;
			return new(className, interfaceTypes);
		}
	}
}