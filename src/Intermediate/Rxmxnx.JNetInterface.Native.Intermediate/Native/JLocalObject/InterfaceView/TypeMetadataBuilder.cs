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
			/// Interface type.
			/// </summary>
			private readonly Type _interfaceType;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="interfaceName">Interface name of the current type.</param>
			/// <param name="interfaceTypes">Interface types.</param>
			private TypeMetadataBuilder(ReadOnlySpan<Byte> interfaceName, IReadOnlySet<Type> interfaceTypes)
			{
				JTypeKind typeKind = TInterface.FamilyType == typeof(InterfaceView) ?
					JTypeKind.Interface :
					JTypeKind.Annotation;
				this._builder = new(interfaceName, typeKind, interfaceTypes);
				this._interfaceType = typeof(IInterfaceObject<TInterface>);
			}

			/// <summary>
			/// Appends an interface to current type definition.
			/// </summary>
			/// <typeparam name="TOtherInterface"><see cref="IDataType"/> interface type.</typeparam>
			/// <returns>Current instance.</returns>
			public TypeMetadataBuilder<TInterface> Extends<TOtherInterface>()
				where TOtherInterface : JInterfaceObject<TOtherInterface>, IInterfaceType<TOtherInterface>
			{
				IReadOnlySet<Type> superInterfacesType = IReferenceType<TOtherInterface>.TypeInterfaces;
				NativeValidationUtilities.ThrowIfInvalidExtension(this._builder.DataTypeName, this._interfaceType,
				                                                  superInterfacesType);
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
			/// <param name="className">Class name of the current type.</param>
			/// <returns>A new <see cref="TypeMetadataBuilder{TInterface}"/> instance.</returns>
			public static TypeMetadataBuilder<TInterface> Create(ReadOnlySpan<Byte> className)
			{
				CommonValidationUtilities.ValidateNotEmpty(className);
				IReadOnlySet<Type> interfaceTypes = IReferenceType<TInterface>.TypeInterfaces;
				return new(className, interfaceTypes);
			}
		}
	}
}