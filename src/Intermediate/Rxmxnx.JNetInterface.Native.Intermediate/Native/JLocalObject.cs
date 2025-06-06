namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local <c>java.lang.Object</c> instance.
/// </summary>
public partial class JLocalObject : JReferenceObject, IClassType<JLocalObject>
{
	/// <summary>
	/// JNI object reference.
	/// </summary>
	public JObjectLocalRef Reference => this.To<JObjectLocalRef>();
	/// <summary>
	/// Retrieves the class object from the current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JClassObject Class => this.Lifetime.GetLoadClassObject(this);
	/// <summary>
	/// Retrieves the global object from the current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JGlobal Global => this.Lifetime.GetLoadGlobalObject(this);
	/// <summary>
	/// Retrieves the global object from the current instance.
	/// </summary>
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public JWeak Weak => this.Lifetime.GetLoadWeakObject(this);
	/// <summary>
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment => this.Lifetime.Environment;
	/// <inheritdoc/>
	public void Dispose()
	{
		this.Dispose(true);
		if (JLocalObject.IsUnloaded(this))
			GC.SuppressFinalize(this);
	}

	/// <inheritdoc cref="JObject.ObjectClassName"/>
	public sealed override CString ObjectClassName => this.Lifetime.Class?.Name ?? JObject.TypeInfo.Name;
	/// <inheritdoc cref="JObject.ObjectSignature"/>
	public sealed override CString ObjectSignature => this.Lifetime.Class?.ClassSignature ?? JObject.TypeInfo.Signature;
	/// <summary>
	/// JNI local object reference.
	/// </summary>
	public JObjectLocalRef LocalReference => base.To<JObjectLocalRef>();

	/// <summary>
	/// Retrieves a <typeparamref name="TReference"/> instance from current instance.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <param name="dispose">
	/// Optional. Indicates whether current instance should be disposed after casting.
	/// </param>
	/// <returns>A <typeparamref name="TReference"/> instance from the current instance.</returns>
	public TReference
		CastTo<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>(
			Boolean dispose = false)
		where TReference : JReferenceObject, IReferenceType<TReference>
		=> this.CastTo<TReference>(this, dispose);
	/// <inheritdoc/>
	public override String ToString() => JObject.GetObjectIdentifier(this.Class.ClassSignature, this.Reference);
	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public override String ToTraceText() => JObject.GetObjectIdentifier(this.Class.ClassSignature, this.Reference);

	/// <summary>
	/// Indicates whether current instance is an instance of <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the current instance is an instance of
	/// <paramref name="jClass"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean InstanceOf(JClassObject jClass)
	{
		IEnvironment env = this.Environment;
		JReferenceObject jObject = (JReferenceObject?)this.Lifetime.GetGlobalObject() ?? this;
		return env.ClassFeature.IsInstanceOf(jObject, jClass);
	}
}