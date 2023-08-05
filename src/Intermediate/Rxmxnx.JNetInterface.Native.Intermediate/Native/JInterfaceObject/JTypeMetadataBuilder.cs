namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JInterfaceObject
{
	/// <summary>
	/// <see cref="JReferenceTypeMetadata"/> class builder.
	/// </summary>
	/// <typeparam name="TInterface">Type of interface.</typeparam>
	protected new sealed partial class JTypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface> : JTypeMetadataBuilder
		where TInterface : JInterfaceObject, IInterfaceType<TInterface>
	{
		/// <inheritdoc/>
		protected override JTypeKind Kind => JTypeKind.Interface;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="interfaceName">Interface name of current type.</param>
		/// <param name="baseTypes">Base types.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private JTypeMetadataBuilder(CString interfaceName, ISet<Type> baseTypes, ISet<Type> interfaceTypes) : base(
			interfaceName, baseTypes, interfaceTypes) { }

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public new JTypeMetadataBuilder<TInterface> WithSignature(CString signature)
		{
			base.WithSignature(signature);
			return this;
		}
		/// <summary>
		/// Sets the array signature.
		/// </summary>
		/// <param name="arraySignature">Array signature.</param>
		/// <returns>Current instance.</returns>
		public new JTypeMetadataBuilder<TInterface> WithArraySignature(CString arraySignature)
		{
			base.WithArraySignature(arraySignature);
			return this;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TOtherInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TInterface> AppendInterface<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TOtherInterface>()
			where TOtherInterface : JInterfaceObject, IInterfaceType<TOtherInterface>
		{
			ValidationUtilities.ThrowIfInvalidExtension<TInterface, TOtherInterface>(this.DataTypeName);
			base.AppendInterface<TOtherInterface>(typeof(IDerivedType<TInterface, TOtherInterface>));
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JInterfaceTypeMetadata Build()
			=> new JInterfaceGenericTypeMetadata(this.DataTypeName, this.CreateInterfaceSet(), this.Signature,
			                                     this.ArraySignature);

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TInterface}"/> instance.</returns>
		public static JTypeMetadataBuilder<TInterface> Create(CString className)
		{
			ValidationUtilities.ValidateNotEmpty(className);
			ISet<Type> baseTypes = IReferenceType<TInterface>.GetBaseTypes().ToHashSet();
			ISet<Type> interfaceTypes = IReferenceType<TInterface>.GetInterfaceTypes().ToHashSet();
			return new(className, baseTypes, interfaceTypes);
		}
	}
}