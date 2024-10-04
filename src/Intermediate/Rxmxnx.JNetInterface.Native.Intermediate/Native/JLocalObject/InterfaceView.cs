namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class is used for create an interface view of an object.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
	                 Justification = CommonConstants.InternalInheritanceJustification)]
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	public abstract partial class InterfaceView : View<JLocalObject>, IInterfaceType
	{
		/// <summary>
		/// JNI object reference.
		/// </summary>
		public JObjectLocalRef Reference => this.Object.Reference;

		/// <inheritdoc/>
		private protected InterfaceView(JLocalObject jObject) : base(jObject) { }

		/// <inheritdoc/>
		public void Dispose()
		{
			this.Object.Dispose();
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Creates the <see cref="JInterfaceTypeMetadata{TInterface}"/> metadata instance for built-in types.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IClassType{TInterface}"/> type.</typeparam>
		/// <param name="information">Interface type information.</param>
		/// <param name="interfaces">Interface interfaces metadata set.</param>
		/// <returns>A <see cref="JInterfaceTypeMetadata{TInterface}"/> instance.</returns>
		private protected static JInterfaceTypeMetadata<TInterface>
			CreateBuiltInMetadata<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>(
				TypeInfoSequence information, IInterfaceSet interfaces)
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
			=> new InterfaceTypeMetadata<TInterface>(information, interfaces);
	}
}