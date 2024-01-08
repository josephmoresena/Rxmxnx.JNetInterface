namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class is used for create view instances of object.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IObject"/> instance.</typeparam>
	public new abstract class View<TObject> : JReferenceObject.View<TObject>, ILocalViewObject
		where TObject : JLocalObject, ILocalObject
	{
		/// <inheritdoc/>
		protected View(TObject jObject) : base(jObject) { }

		ILocalObject ILocalViewObject.Object => this.Object;
	}
}