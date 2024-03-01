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
	private static void Validate<TReferenceObject, TDataType>(TReferenceObject jObject, IEnvironment env)
		where TReferenceObject : JReferenceObject where TDataType : JReferenceObject, IDataType<TDataType>
		=> ValidationUtilities.ThrowIfInvalidCast<TDataType>(env.ClassFeature.IsAssignableTo<TDataType>(jObject));

	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}