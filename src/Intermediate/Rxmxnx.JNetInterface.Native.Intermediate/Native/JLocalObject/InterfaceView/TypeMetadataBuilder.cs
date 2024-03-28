namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	public abstract partial class InterfaceView
	{
		/// <summary>
		/// <see cref="JReferenceTypeMetadata"/> interface builder.
		/// </summary>
		/// <typeparam name="TInterface">Type of interface.</typeparam>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
		                 Justification = CommonConstants.NoMethodOverloadingJustification)]
		protected ref partial struct TypeMetadataBuilder<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			/// <summary>
			/// A <see cref="JLocalObject.TypeMetadataBuilder"/> instance.
			/// </summary>
			private TypeMetadataBuilder _builder;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="interfaceName">Interface name of current type.</param>
			/// <param name="interfaceTypes">Interface types.</param>
			private TypeMetadataBuilder(ReadOnlySpan<Byte> interfaceName, ISet<Type> interfaceTypes)
			{
				JTypeKind typeKind = TInterface.FamilyType == typeof(InterfaceView) ?
					JTypeKind.Interface :
					JTypeKind.Annotation;
				this._builder = new(interfaceName, typeKind, interfaceTypes);
			}

			/// <summary>
			/// Appends an interface to current type definition.
			/// </summary>
			/// <typeparam name="TOtherInterface"><see cref="IDataType"/> interface type.</typeparam>
			/// <returns>Current instance.</returns>
			public TypeMetadataBuilder<TInterface> Extends<TOtherInterface>()
				where TOtherInterface : JInterfaceObject<TOtherInterface>, IInterfaceType<TOtherInterface>
			{
				NativeValidationUtilities.ThrowIfInvalidExtension<TInterface, TOtherInterface>(
					this._builder.DataTypeName);
				this._builder.AppendInterface<TOtherInterface>();
				return this;
			}
			/// <summary>
			/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
			/// </summary>
			/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
			public JInterfaceTypeMetadata<TInterface> Build() => new InterfaceTypeMetadata(this._builder);

			/// <summary>
			/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
			/// </summary>
			/// <param name="className">Class name of current type.</param>
			/// <returns>A new <see cref="TypeMetadataBuilder{TInterface}"/> instance.</returns>
			public static TypeMetadataBuilder<TInterface> Create(ReadOnlySpan<Byte> className)
			{
				ValidationUtilities.ValidateNotEmpty(className);
				ISet<Type> interfaceTypes = IReferenceType<TInterface>.GetInterfaceTypes().ToHashSet();
				return new(className, interfaceTypes);
			}
		}
	}
}