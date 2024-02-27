namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject : ILocalObject
{
	IVirtualMachine ILocalObject.VirtualMachine => this.Lifetime.Environment.VirtualMachine;
	Boolean ILocalObject.IsProxy => this.IsProxy;
	ObjectLifetime ILocalObject.Lifetime => this.Lifetime;
	ObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();
	void ILocalObject.ProcessMetadata(ObjectMetadata instanceMetadata) => this.ProcessMetadata(instanceMetadata);

	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="instanceMetadata">The object metadata for <paramref name="jLocal"/>.</param>
	private static void ProcessMetadata(JLocalObject jLocal, ObjectMetadata instanceMetadata)
		=> jLocal.ProcessMetadata(instanceMetadata);
	/// <summary>
	/// Throws an exception if the instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </summary>
	/// <typeparam name="TReferenceObject"><see langword="JReferenceObject"/> type.</typeparam>
	/// <typeparam name="TDataType"><see langword="IDatatype"/> type.</typeparam>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// </returns>
	/// <exception cref="InvalidCastException">
	/// Throws an exception if the instance cannot be cast to <typeparamref name="TDataType"/> instance.
	/// </exception>
	private static TReferenceObject Validate<TReferenceObject, TDataType>(TReferenceObject jObject, IEnvironment env)
		where TReferenceObject : JReferenceObject where TDataType : JReferenceObject, IDataType<TDataType>
	{
		ValidationUtilities.ThrowIfInvalidCast<TDataType>(env.ClassFeature.IsAssignableTo<TDataType>(jObject));
		return jObject;
	}
	/// <summary>
	/// Retrieves a <typeparamref name="TReference"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="dispose">
	/// Indicates whether current instance should be disposed after casting.
	/// </param>
	/// <returns>A <typeparamref name="TReference"/> instance from current global instance.</returns>
	private static TReference CastTo<TReference>(JLocalObject jLocal, Boolean dispose)
		where TReference : JLocalObject, IReferenceType<TReference>
	{
		IEnvironment env = jLocal.Lifetime.Environment;
		if (jLocal is TReference result) return result;
		if (JLocalObject.IsClassType<TReference>())
		{
			result = (TReference)(Object)env.ClassFeature.AsClassObject(jLocal);
		}
		else
		{
			JReferenceTypeMetadata metadata = IReferenceType.GetMetadata<TReference>();
			if (!jLocal.ObjectClassName.AsSpan().SequenceEqual(UnicodeClassNames.ClassObject))
			{
				result = (TReference)metadata.ParseInstance(jLocal);
			}
			else
			{
				JClassObject jClass = env.ClassFeature.AsClassObject(jLocal);
				result = JLocalObject.IsObjectType<TReference>() ?
					(TReference)(Object)jClass :
					(TReference)metadata.ParseInstance(jClass);
			}
		}
		if (dispose) jLocal.Dispose();
		return result;
	}

	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}