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
	/// Retrieves a <typeparamref name="TReference"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="dispose">
	/// Optional. Indicates whether <paramref name="jLocal"/> should be disposed after casting.
	/// </param>
	/// <returns>A <typeparamref name="TReference"/> instance from <paramref name="jLocal"/>.</returns>
	private TReference CastTo<TReference>(JLocalObject jLocal, Boolean dispose)
		where TReference : JReferenceObject, IReferenceType<TReference>
	{
		if (jLocal is TReference result) return result;
		JLocalObject.Validate<TReference>(jLocal);
		result = TReference.Create(jLocal);
		if (dispose && result is JLocalObject) this.Dispose();
		return result;
	}

	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JLocalObject IClassType<JLocalObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}