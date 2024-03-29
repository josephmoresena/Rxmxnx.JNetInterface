namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JInterfaceObject
{
	/// <summary>
	/// <see cref="JReferenceTypeMetadata"/> interface builder.
	/// </summary>
	/// <typeparam name="TInterface">Type of interface.</typeparam>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	protected new ref partial struct JTypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
	{
		/// <summary>
		/// A <see cref="JLocalObject.JTypeMetadataBuilder"/> instance.
		/// </summary>
		private JTypeMetadataBuilder _builder;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="interfaceName">Interface name of current type.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private JTypeMetadataBuilder(ReadOnlySpan<Byte> interfaceName, ISet<Type> interfaceTypes)
			=> this._builder = new(interfaceName, JTypeKind.Interface, interfaceTypes);

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TOtherInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TInterface> Extends<TOtherInterface>()
			where TOtherInterface : JInterfaceObject<TOtherInterface>, IInterfaceType<TOtherInterface>
		{
			NativeValidationUtilities.ThrowIfInvalidExtension<TInterface, TOtherInterface>(this._builder.DataTypeName);
			this._builder.AppendInterface<TOtherInterface>();
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JInterfaceTypeMetadata<TInterface> Build() => new JInterfaceGenericTypeMetadata(this._builder);

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TInterface}"/> instance.</returns>
		public static JTypeMetadataBuilder<TInterface> Create(ReadOnlySpan<Byte> className)
		{
			ValidationUtilities.ValidateNotEmpty(className);
			ISet<Type> interfaceTypes = IReferenceType<TInterface>.GetInterfaceTypes().ToHashSet();
			return new(className, interfaceTypes);
		}
	}
}