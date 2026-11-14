namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class is used to create view instances of an object.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IObject"/> instance.</typeparam>
	public new abstract partial class View<TObject>(TObject jObject)
		: JReferenceObject.View<TObject>(jObject), IWrapper<JLocalObject> where TObject : JLocalObject, ILocalObject
	{
		/// <inheritdoc cref="JLocalObject.Environment"/>
		public IEnvironment Environment => this.Object.Environment;

		/// <summary>
		/// Retrieves a <typeparamref name="TReference"/> instance from the current instance.
		/// </summary>
		/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
		/// <returns>A <typeparamref name="TReference"/> instance from the current instance.</returns>
		public TReference CastTo<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>()
			where TReference : JReferenceObject, IReferenceType<TReference>
			=> this as TReference ?? this.Object.CastTo<TReference>();
	}
}