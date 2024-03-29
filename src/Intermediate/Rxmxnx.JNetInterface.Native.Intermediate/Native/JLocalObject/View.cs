namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class is used for create view instances of object.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IObject"/> instance.</typeparam>
	public new abstract class View<TObject> : JReferenceObject.View<TObject>, ILocalViewObject, IWrapper<JLocalObject>
		where TObject : JLocalObject, ILocalObject
	{
		/// <inheritdoc cref="JLocalObject.Environment"/>
		public IEnvironment Environment => this.Object.Environment;

		/// <inheritdoc/>
		protected View(TObject jObject) : base(jObject) { }

		ILocalObject ILocalViewObject.Object => this.Object;
		JObjectLocalRef ILocalObject.InternalReference => this.Object.InternalReference;

		/// <summary>
		/// Retrieves a <typeparamref name="TReference"/> instance from current instance.
		/// </summary>
		/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
		/// <returns>A <typeparamref name="TReference"/> instance from current instance.</returns>
		public TReference CastTo<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>
			=> this as TReference ?? this.Object.CastTo<TReference>();
		JLocalObject IWrapper<JLocalObject>.Value => this.Object;
	}
}