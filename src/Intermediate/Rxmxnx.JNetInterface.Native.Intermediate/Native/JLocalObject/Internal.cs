namespace Rxmxnx.JNetInterface.Native;

using TypeMetadata = JClassTypeMetadata<JLocalObject>;

public partial class JLocalObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	internal static readonly TypeMetadata ObjectClassMetadata =
		JLocalObject.CreateBuiltInMetadata<JLocalObject>(JObject.TypeInfo, JTypeModifier.Extensible);
	/// <summary>
	/// Indicates finalizer is at execution.
	/// </summary>
	internal static Boolean FinalizerExecution => JLocalObject.finalizerExecution;

	static TypeMetadata IClassType<JLocalObject>.Metadata => JLocalObject.ObjectClassMetadata;
	static Type IDataType.FamilyType => typeof(JLocalObject);

	/// <inheritdoc cref="ILocalObject.Lifetime"/>
	internal ObjectLifetime Lifetime { get; }

	/// <summary>
	/// Interprets internal current value as <typeparamref name="TReference"/> value.
	/// </summary>
	/// <typeparam name="TReference">Type of value.</typeparam>
	/// <returns>A read-only reference of <typeparamref name="TReference"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal ref readonly TReference LocalAs<TReference>() where TReference : unmanaged, INativeType
		=> ref base.As<TReference>();

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="localRef">A local object reference the value of the current instance.</param>
	internal void SetValue(JObjectLocalRef localRef) => this.Lifetime.SetValue(this, localRef);
	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <typeparam name="TValue">Type of <see langword="IObjectReference"/> instance.</typeparam>
	/// <param name="localRef">A local object reference the value of the current instance.</param>
	internal void SetValue<TValue>(TValue localRef) where TValue : unmanaged, IObjectReferenceType
		=> this.Lifetime.SetValue(this, localRef);
	/// <inheritdoc/>
	internal override ref readonly TValue As<TValue>()
	{
		JGlobalBase? jGlobal = this.Lifetime.GetGlobalObject();
		if (jGlobal is not null)
			return ref jGlobal.As<TValue>();
		return ref base.As<TValue>();
	}
	/// <inheritdoc/>
	internal override TValue To<TValue>() => this.Lifetime.GetGlobalObject()?.To<TValue>() ?? base.To<TValue>();
	/// <inheritdoc/>
	internal override void ClearValue() => this.Lifetime.Dispose();
	/// <inheritdoc/>
	internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
		=> this.Lifetime.SetAssignableTo<TDataType>(isAssignable);
	/// <inheritdoc/>
	internal override Boolean IsDefaultInstance()
	{
		if (this.Lifetime.GetGlobalObject() is { } jGlobal && !jGlobal.IsDefaultInstance())
			return false;
		return base.IsDefaultInstance();
	}

	/// <inheritdoc cref="JObject.ObjectClassName"/>
	private protected override Boolean IsInstanceOf<TDataType>() => this.Lifetime.InstanceOf<TDataType>(this);
	/// <inheritdoc/>
	private protected override ReadOnlySpan<Byte> AsSpan() => this.Lifetime.Span;
	/// <inheritdoc/>
	private protected override IDisposable GetSynchronizer()
	{
		IEnvironment env = this.Lifetime.Environment;
		JReferenceObject jObject = this.Lifetime.GetGlobalObject() as JReferenceObject ?? this;
		return env.ReferenceFeature.GetSynchronizer(jObject);
	}
	/// <inheritdoc/>
	private protected override Boolean Same(JReferenceObject jObject)
		=> base.Same(jObject) || this.Environment.IsSameObject(this, jObject);

	/// <summary>
	/// Indicates whether <see cref="IDataType{TDataType}"/> CLR type is the CLR type of
	/// <see cref="JLocalObject"/>.
	/// </summary>
	/// <typeparam name="TDataType">Generic <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if <see cref="IDataType{TDataType}"/> type is the CLR type of <see cref="JLocalObject"/>;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal static Boolean IsObjectType<TDataType>() where TDataType : IDataType<TDataType>
		=> typeof(TDataType) == typeof(JLocalObject);
	/// <summary>
	/// Indicates whether <see cref="IDataType{TDataType}"/> CLR type is the CLR type of
	/// <see cref="JClassObject"/>.
	/// </summary>
	/// <typeparam name="TDataType">Generic <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if <see cref="IDataType{TDataType}"/> type is the CLR type of <see cref="JClassObject"/>;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal static Boolean IsClassType<TDataType>() where TDataType : IDataType<TDataType>
		=> typeof(TDataType) == typeof(JClassObject);

	/// <summary>
	/// Throws an exception if <paramref name="jObject"/> cannot be cast to
	/// <typeparamref name="TDataType"/> instance.
	/// </summary>
	/// <typeparam name="TDataType"><see langword="IDatatype"/> type.</typeparam>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	///     <paramref name="jObject"/>
	/// </returns>
	/// <exception cref="InvalidCastException">
	/// Throws an exception if the instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </exception>
	internal static void Validate<TDataType>(JReferenceObject jObject)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		JReferenceTypeMetadata typeMetadata = (JReferenceTypeMetadata)IDataType.GetMetadata<TDataType>();
		if (jObject.ObjectClassName.AsSpan().SequenceEqual(typeMetadata.ClassName)) return;
		if (jObject is not TDataType)
			CommonValidationUtilities.ThrowIfInvalidCast(typeMetadata, typeMetadata.IsInstance(jObject));
	}
	/// <summary>
	/// Creates the <see cref="JClassTypeMetadata{TClass}"/> metadata instance for built-in types.
	/// </summary>
	/// <typeparam name="TClass"><see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="information">Class type information.</param>
	/// <param name="modifier">Modifier of the current type.</param>
	/// <param name="interfaces">Class interfaces metadata set.</param>
	/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
	private protected static JClassTypeMetadata<TClass>
		CreateBuiltInMetadata<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass>(
			TypeInfoSequence information, JTypeModifier modifier, IInterfaceSet? interfaces = default)
		where TClass : JLocalObject, IClassType<TClass>
	{
		JClassTypeMetadata? baseMetadata = !JLocalObject.IsObjectType<TClass>() ?
			IClassType.GetMetadata<JLocalObject>() :
			default;
		return new ClassTypeMetadata<TClass>(information, modifier, baseMetadata, interfaces ?? InterfaceSet.Empty);
	}
	/// <summary>
	/// Creates the <see cref="JClassTypeMetadata{TClass}"/> metadata instance for built-in types.
	/// </summary>
	/// <typeparam name="TClass"><see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="information">Class type information.</param>
	/// <param name="modifier">Modifier of the current type.</param>
	/// <param name="baseMetadata">Super class type metadata.</param>
	/// <param name="interfaces">Class interfaces metadata set.</param>
	/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
	private protected static JClassTypeMetadata<TClass>
		CreateBuiltInMetadata<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass>(
			TypeInfoSequence information, JClassTypeMetadata baseMetadata, JTypeModifier modifier,
			IInterfaceSet? interfaces = default) where TClass : JLocalObject, IClassType<TClass>
		=> new ClassTypeMetadata<TClass>(information, modifier, baseMetadata, interfaces ?? baseMetadata.Interfaces);
	/// <summary>
	/// Creates the <see cref="JClassTypeMetadata{TClass}"/> metadata instance for built-in types.
	/// </summary>
	/// <typeparam name="TClass"><see cref="IClassType{TClass}"/> type.</typeparam>
	/// <param name="information">Class type information.</param>
	/// <param name="modifier">Modifier of the current type.</param>
	/// <param name="baseMetadata">Super class type metadata.</param>
	/// <param name="interfaces">Class interfaces metadata set.</param>
	/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
	private protected static JClassTypeMetadata<TClass>
		CreateBuiltInMetadata<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass>(
			TypeInfoSequence information, JClassTypeMetadata baseMetadata, JTypeModifier modifier,
			ImmutableHashSet<JInterfaceTypeMetadata> interfaces) where TClass : JLocalObject, IClassType<TClass>
		=> new ClassTypeMetadata<TClass>(information, modifier, baseMetadata,
		                                 InterfaceSet.GetClassInterfaces(baseMetadata, interfaces));
	/// <summary>
	/// Creates a <see cref="JClassTypeMetadata{TClass}"/> instance for <paramref name="primitiveMetadata"/>
	/// wrapper class.
	/// </summary>
	/// <param name="primitiveMetadata">A <see cref="JPrimitiveTypeMetadata"/> instance.</param>
	/// <param name="baseMetadata">Base type metadata of the current type.</param>
	/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
	private protected static JClassTypeMetadata<TClass>
		CreateBuiltInMetadata<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass>(
			JPrimitiveTypeMetadata primitiveMetadata, JClassTypeMetadata? baseMetadata = default)
		where TClass : JLocalObject, IClassType<TClass>
		=> new ClassTypeMetadata<TClass>(primitiveMetadata.WrapperInformation, primitiveMetadata.SizeOf == 0,
		                                 baseMetadata ?? IClassType.GetMetadata<JLocalObject>());
}