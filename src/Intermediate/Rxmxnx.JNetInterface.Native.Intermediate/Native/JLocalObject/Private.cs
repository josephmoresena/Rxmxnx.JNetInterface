namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject : ILocalObject
{
	static Type IDataType.FamilyType => typeof(JLocalObject);

	/// <summary>
	/// Internal <see cref="ObjectLifetime"/> instance.
	/// </summary>
	private readonly ObjectLifetime _lifetime;

	IVirtualMachine ILocalObject.VirtualMachine => this._lifetime.Environment.VirtualMachine;
	Boolean ILocalObject.IsDummy => this.IsDummy;
	ObjectLifetime ILocalObject.Lifetime => this._lifetime;
	ObjectMetadata ILocalObject.CreateMetadata() => this.CreateMetadata();
	void ILocalObject.ProcessMetadata(ObjectMetadata instanceMetadata) => this.ProcessMetadata(instanceMetadata);

	/// <summary>
	/// Retrieves <see cref="ObjectLifetime"/> for <see cref="JLocalObject"/> instantiation.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="overrideClass">Indicates whether <paramref name="jClass"/> must override current class.</param>
	/// <returns></returns>
	private ObjectLifetime GetLifetime(IEnvironment env, JObjectLocalRef localRef, JClassObject? jClass,
		Boolean overrideClass = false)
	{
		ObjectLifetime? result = default;
		if (localRef != default)
			result = this.Environment.ReferenceFeature.GetLifetime(this, localRef, jClass, overrideClass);
		return result ?? new(env, this, localRef)
		{
			Class = jClass, IsRealClass = jClass is not null && jClass.IsFinal.GetValueOrDefault(),
		};
	}

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
		where TReferenceObject : JReferenceObject where TDataType : JLocalObject, IDataType<TDataType>
	{
		ValidationUtilities.ThrowIfInvalidCast<TDataType>(env.ClassFeature.IsAssignableTo<TDataType>(jObject));
		return jObject;
	}
}