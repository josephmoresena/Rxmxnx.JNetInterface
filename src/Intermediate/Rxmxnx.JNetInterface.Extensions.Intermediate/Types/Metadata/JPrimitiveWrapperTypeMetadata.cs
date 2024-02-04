namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a primitive wrapper class <see cref="IPrimitiveWrapperType"/> type.
/// </summary>
internal sealed record JPrimitiveWrapperTypeMetadata<TWrapper> : JPrimitiveWrapperTypeMetadata
	where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
{
	/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
	private readonly JClassTypeMetadata _baseMetadata;

	/// <inheritdoc/>
	public override JPrimitiveTypeMetadata PrimitiveMetadata => TWrapper.PrimitiveMetadata;
	/// <inheritdoc/>
	public override Type Type => typeof(TWrapper);
	/// <inheritdoc/>
	public override JTypeModifier Modifier => JTypeModifier.Final;
	/// <inheritdoc/>
	public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TWrapper>();
	/// <inheritdoc/>
	public override IReadOnlySet<JInterfaceTypeMetadata> Interfaces
		=> this.PrimitiveMetadata.SizeOf != 0 ? InterfaceSet.PrimitiveWrapperSet : InterfaceSet.Empty;
	/// <inheritdoc/>
	public override JClassTypeMetadata BaseMetadata => this._baseMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="baseMetadata">Base <see cref="JClassTypeMetadata"/> instance.</param>
	public JPrimitiveWrapperTypeMetadata(JClassTypeMetadata? baseMetadata = default) : base(
		TWrapper.PrimitiveMetadata.WrapperInformation)
		=> this._baseMetadata = baseMetadata ?? IClassType.GetMetadata<JLocalObject>();

	/// <inheritdoc/>
	public override String ToString()
		=> $"{nameof(JDataTypeMetadata)} {{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";

	/// <inheritdoc/>
	internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
		Boolean realClass = false)
	{
		IEnvironment env = jClass.Environment;
		JClassObject wrapperClass = env.ClassFeature.GetClass<TWrapper>();
		return TWrapper.Create(wrapperClass, localRef);
	}
	/// <inheritdoc/>
	internal override TWrapper? ParseInstance(JLocalObject? jLocal)
	{
		switch (jLocal)
		{
			case null:
				return default;
			case TWrapper result:
				return result;
			default:
				JLocalObject.Validate<TWrapper>(jLocal);
				return TWrapper.Create(jLocal);
		}
	}
	/// <inheritdoc/>
	internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
	{
		if (jGlobal is null) return default;
		if (!jGlobal.ObjectMetadata.ObjectClassName.AsSpan().SequenceEqual(this.ClassName))
			JLocalObject.Validate<TWrapper>(jGlobal, env);
		return TWrapper.Create(env, jGlobal);
	}
	/// <inheritdoc/>
	internal override JFunctionDefinition<TWrapper> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] metadata)
		=> JFunctionDefinition<TWrapper>.Create(functionName, metadata);
	/// <inheritdoc/>
	internal override JFieldDefinition<TWrapper> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName) => new(fieldName);
	/// <inheritdoc/>
	internal override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TWrapper>();
}