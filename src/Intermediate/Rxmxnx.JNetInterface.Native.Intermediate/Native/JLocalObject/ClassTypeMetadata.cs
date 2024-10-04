namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This record stores the metadata for a class <see cref="IClassType"/> type.
	/// </summary>
	private sealed class ClassTypeMetadata<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass> : JClassTypeMetadata<TClass>
		where TClass : JLocalObject, IClassType<TClass>
	{
		/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
		private readonly JClassTypeMetadata? _baseMetadata;
		/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
		private readonly IInterfaceSet _interfaces;
		/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
		private readonly JTypeModifier _modifier;

		/// <inheritdoc/>
		public override JClassTypeMetadata? BaseMetadata => this._baseMetadata;
		/// <inheritdoc/>
		public override JTypeModifier Modifier => this._modifier;
		/// <inheritdoc/>
		public override IInterfaceSet Interfaces => this._interfaces;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="builder">A <see cref="TypeMetadataBuilder"/> instance.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <param name="baseMetadata">Base type of the current type metadata.</param>
		public ClassTypeMetadata(TypeMetadataBuilder builder, JTypeModifier modifier, JClassTypeMetadata? baseMetadata)
			: base(builder.DataTypeName)
		{
			this._modifier = modifier;
			this._interfaces = InterfaceSet.GetClassInterfaces(baseMetadata, builder.GetInterfaceSet());
			this._baseMetadata = baseMetadata;
		}
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="information">Internal sequence information.</param>
		/// <param name="isVoid">Indicates if current class is <c>void</c> wrapper.</param>
		/// <param name="baseMetadata">Base type of the current type metadata.</param>
		public ClassTypeMetadata(TypeInfoSequence information, Boolean isVoid, JClassTypeMetadata? baseMetadata) :
			base(information)
		{
			this._modifier = JTypeModifier.Final;
			this._interfaces = !isVoid ? InterfaceSet.SerializableComparableSet : InterfaceSet.Empty;
			this._baseMetadata = baseMetadata;
		}
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="information">Internal sequence information.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <param name="baseMetadata">Base type of the current type metadata.</param>
		/// <param name="interfaces">Interfaces metadata set.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ClassTypeMetadata(TypeInfoSequence information, JTypeModifier modifier, JClassTypeMetadata? baseMetadata,
			ImmutableHashSet<JInterfaceTypeMetadata>? interfaces) : base(information)
		{
			this._modifier = modifier;
			this._interfaces = interfaces is not null ?
				InterfaceSet.GetClassInterfaces(baseMetadata, interfaces) :
				InterfaceSet.Empty;
			this._baseMetadata = baseMetadata;
		}
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="information">Internal sequence information.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <param name="baseMetadata">Base type of the current type metadata.</param>
		/// <param name="interfaces">Interfaces metadata set.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ClassTypeMetadata(TypeInfoSequence information, JTypeModifier modifier, JClassTypeMetadata? baseMetadata,
			IInterfaceSet interfaces) : base(information)
		{
			this._modifier = modifier;
			this._interfaces = interfaces;
			this._baseMetadata = baseMetadata;
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal override JClassObject GetClass(IEnvironment env) => env.ClassFeature.GetClass<TClass>();
		/// <inheritdoc/>
		internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
			Boolean realClass = false)
			=> TClass.Create(new IReferenceType.ClassInitializer
			{
				Class = jClass, RealClass = realClass, LocalReference = localRef,
			});
		/// <inheritdoc/>
		internal override JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false)
			=> jLocal?.CastTo<TClass>(dispose);
		/// <inheritdoc/>
		internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
		{
			if (jGlobal is null) return default;
			JLocalObject.Validate<TClass>(jGlobal);
			return TClass.Create(new IReferenceType.GlobalInitializer { Global = jGlobal, Environment = env, });
		}
	}
}