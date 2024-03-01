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
		JLocalObject IWrapper<JLocalObject>.Value => this.Object;
	}
}