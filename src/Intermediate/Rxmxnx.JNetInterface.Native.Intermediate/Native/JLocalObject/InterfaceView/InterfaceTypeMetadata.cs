namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	public abstract partial class InterfaceView
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IInterfaceType"/> type.
		/// </summary>
		private sealed class InterfaceTypeMetadata<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface> : JInterfaceTypeMetadata
			<TInterface> where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
			private readonly IInterfaceSet _interfaces;

			/// <inheritdoc/>
			public override Type InterfaceType => typeof(IInterfaceObject<TInterface>);
			/// <inheritdoc/>
			public override Type Type => typeof(TInterface);
			/// <inheritdoc/>
			public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TInterface>();
			/// <inheritdoc/>
			public override IInterfaceSet Interfaces => this._interfaces;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="builder">A <see cref="JLocalObject.TypeMetadataBuilder"/> instance.</param>
			internal InterfaceTypeMetadata(TypeMetadataBuilder builder) :
				base(builder.DataTypeName, builder.IsAnnotation)
				=> this._interfaces = builder.IsAnnotation ?
					InterfaceSet.AnnotationSet :
					InterfaceSet.GetInterfaceInterfaces(builder.GetInterfaceSet());
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="information">Internal sequence information.</param>
			/// <param name="interfaces">Super-interfaces metadata set.</param>
			internal InterfaceTypeMetadata(TypeInfoSequence information, IInterfaceSet interfaces) : base(
				information, TInterface.FamilyType != typeof(InterfaceView))
				=> this._interfaces = interfaces;

			/// <inheritdoc/>
			public override JArrayTypeMetadata GetArrayMetadata()
				=> JReferenceTypeMetadata.GetArrayMetadata<TInterface>();

			/// <inheritdoc/>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal override JClassObject GetClass(IEnvironment env) => env.ClassFeature.GetClass<TInterface>();
			/// <inheritdoc/>
			internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
				Boolean realClass = false)
				=> new Proxy<TInterface>(new IReferenceType.ClassInitializer
				{
					Class = jClass, RealClass = realClass, LocalReference = localRef,
				});
			/// <inheritdoc/>
			internal override JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false)
			{
				if (jLocal == null) return default;
				if (jLocal is not IInterfaceObject<TInterface>)
					JLocalObject.Validate<TInterface>(jLocal);
				return TInterface.Create(jLocal);
			}
			/// <inheritdoc/>
			internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
			{
				if (jGlobal is null) return default;
				JLocalObject.Validate<TInterface>(jGlobal);
				return new Proxy<TInterface>(
					new IReferenceType.GlobalInitializer { Global = jGlobal, Environment = env, });
			}
			/// <inheritdoc/>
			internal override JFunctionDefinition<TInterface> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
				ReadOnlySpan<JArgumentMetadata> paramsMetadata)
				=> JFunctionDefinition<TInterface>.Create(functionName, paramsMetadata);
			/// <inheritdoc/>
			internal override JFieldDefinition<TInterface> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
				=> new(fieldName);
		}
	}
}