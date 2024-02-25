namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	internal static readonly JClassTypeMetadata<JLocalObject> ObjectClassMetadata = TypeMetadataBuilder<JLocalObject>
		.Create(UnicodeClassNames.Object).WithSignature(UnicodeObjectSignatures.ObjectSignature).Build();

	static JClassTypeMetadata<JLocalObject> IClassType<JLocalObject>.Metadata => JLocalObject.ObjectClassMetadata;
	static Type IDataType.FamilyType => typeof(JLocalObject);

	/// <summary>
	/// Internal reference value.
	/// </summary>
	internal JObjectLocalRef InternalReference => base.To<JObjectLocalRef>();
	/// <inheritdoc cref="ILocalObject.Lifetime"/>
	internal ObjectLifetime Lifetime { get; }

	/// <summary>
	/// Interprets internal current value as <typeparamref name="TReference"/> value.
	/// </summary>
	/// <typeparam name="TReference">Type of value.</typeparam>
	/// <returns>A read-only reference of <typeparamref name="TReference"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal ref readonly TReference InternalAs<TReference>() where TReference : unmanaged, INativeType<TReference>
		=> ref base.As<TReference>();

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	internal void SetValue(JObjectLocalRef localRef) => this.Lifetime.SetValue(this, localRef);
	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <typeparam name="TValue">Type of <see langword="IObjectReference"/> instance.</typeparam>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	internal void SetValue<TValue>(TValue localRef) where TValue : unmanaged, IObjectReferenceType
		=> this.Lifetime.SetValue(this, localRef);
	/// <inheritdoc/>
	private protected override ReadOnlySpan<Byte> AsSpan() => this.Lifetime.Span;
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
	private protected override IDisposable GetSynchronizer()
	{
		IEnvironment env = this.Lifetime.Environment;
		return env.ReferenceFeature.GetSynchronizer(this);
	}
	/// <inheritdoc/>
	private protected override Boolean Same(JReferenceObject jObject)
		=> base.Same(jObject) || this.Environment.IsSameObject(this, jObject);
	/// <inheritdoc/>
	internal override Boolean IsAssignableTo<TDataType>() => this.Lifetime.IsAssignableTo<TDataType>(this);
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
	/// Retrieves the loaded global object for given object.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The loaded <see cref="JGlobalBase"/> object for <paramref name="jLocal"/>.</returns>
	internal static JGlobalBase? GetGlobalObject(JLocalObject jLocal) => jLocal.Lifetime.GetGlobalObject();
	/// <summary>
	/// Retrieves initial final <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="metadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
	internal static TObject Create<TObject>(JClassObject jClass, JReferenceTypeMetadata metadata,
		JObjectLocalRef localRef) where TObject : JLocalObject, IReferenceType<TObject>
	{
		using JLocalObject jLocalTemp = new(jClass, localRef);
		return (TObject)metadata.ParseInstance(jLocalTemp);
	}

	/// <summary>
	/// Throws an exception if the global instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </summary>
	/// <typeparam name="TDataType"><see langword="IDatatype"/> type.</typeparam>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <exception cref="InvalidCastException">
	/// Throws an exception if the instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </exception>
	internal static void Validate<TDataType>(JGlobalBase jGlobal, IEnvironment env)
		where TDataType : JLocalObject, IDataType<TDataType>
		=> JLocalObject.Validate<JGlobalBase, TDataType>(jGlobal, env);
	/// <summary>
	/// Throws an exception if the local instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </summary>
	/// <typeparam name="TDataType"><see langword="IDatatype"/> type.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>
	///     <paramref name="jLocal"/>
	/// </returns>
	/// <exception cref="InvalidCastException">
	/// Throws an exception if the instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </exception>
	internal static JLocalObject Validate<TDataType>(JLocalObject jLocal)
		where TDataType : JLocalObject, IDataType<TDataType>
	{
		if (jLocal.ObjectClassName.AsSpan().SequenceEqual(IDataType.GetMetadata<TDataType>().ClassName)) return jLocal;
		return jLocal as TDataType ??
			JLocalObject.Validate<JLocalObject, TDataType>(jLocal, jLocal.Lifetime.Environment);
	}
}