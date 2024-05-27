namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java class type instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IClassType : IReferenceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Class;

	/// <summary>
	/// Retrieves the metadata for given class type.
	/// </summary>
	/// <typeparam name="TClass">Type of the current java class datatype.</typeparam>
	/// <returns>The <see cref="JClassTypeMetadata"/> instance for given type.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new static JClassTypeMetadata GetMetadata<TClass>() where TClass : JReferenceObject, IClassType<TClass>
		=> (JClassTypeMetadata)IDataType.GetMetadata<TClass>();
}

/// <summary>
/// This interface exposes an object that represents a java class type instance.
/// </summary>
/// <typeparam name="TClass">Type of java class type.</typeparam>
public interface
	IClassType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass> : IClassType,
	IReferenceType<TClass> where TClass : JReferenceObject, IClassType<TClass>
{
	/// <summary>
	/// Cached type interface set.
	/// </summary>
	private static readonly WeakReference<BaseTypeSet<TClass>?> typeBaseTypeSet = new(default);

	/// <summary>
	/// Retrieves current type base type set.
	/// </summary>
	internal static IReadOnlySet<Type> TypeBaseTypes
	{
		get
		{
			if (IClassType<TClass>.typeBaseTypeSet.TryGetTarget(out BaseTypeSet<TClass>? result))
				return result;
			result = new();
			IClassType<TClass>.typeBaseTypeSet.SetTarget(result);
			return result;
		}
	}

	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected new static abstract JClassTypeMetadata<TClass> Metadata { get; }

	static JDataTypeMetadata IDataType<TClass>.Metadata => TClass.Metadata;

	/// <summary>
	/// Creates a <typeparamref name="TClass"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ClassInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TClass"/> instance from <paramref name="initializer"/>.</returns>
	protected static abstract TClass Create(ClassInitializer initializer);
	/// <summary>
	/// Creates a <typeparamref name="TClass"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ObjectInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TClass"/> instance from <paramref name="initializer"/>.</returns>
	protected new static abstract TClass Create(ObjectInitializer initializer);
	/// <summary>
	/// Creates a <typeparamref name="TClass"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.GlobalInitializer"/> instance.</param>
	/// <returns>A <typeparamref name="TClass"/> instance from <paramref name="initializer"/>.</returns>
	protected static abstract TClass Create(GlobalInitializer initializer);

	static TClass IReferenceType<TClass>.Create(ObjectInitializer initializer) => TClass.Create(initializer);
}