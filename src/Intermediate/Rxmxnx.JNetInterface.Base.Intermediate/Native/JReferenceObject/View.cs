namespace Rxmxnx.JNetInterface.Native;

public partial class JReferenceObject
{
	/// <summary>
	/// This class is used for create view instances of object.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IObject"/> instance.</typeparam>
	public abstract class View<TObject> : JReferenceObject, IViewObject where TObject : JReferenceObject, IObject
	{
		/// <summary>
		/// <typeparamref name="TObject"/> instance.
		/// </summary>
		public TObject Object { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jObject">A <typeparamref name="TObject"/> instance.</param>
		protected View(TObject jObject) => this.Object = jObject;

		IObject IViewObject.Object => this.Object;

		/// <inheritdoc cref="IObject.ObjectClassName"/>
		public override CString ObjectClassName => this.Object.ObjectClassName;
		/// <inheritdoc cref="IObject.ObjectSignature"/>
		public override CString ObjectSignature => this.Object.ObjectSignature;

		/// <inheritdoc/>
		internal override Boolean IsInstanceOf<TDataType>() => this.Object.IsInstanceOf<TDataType>();

		/// <inheritdoc/>
		internal override void ClearValue() => this.Object.ClearValue();
		/// <inheritdoc/>
		internal override IDisposable GetSynchronizer() => this.Object.GetSynchronizer();
		/// <inheritdoc/>
		internal override Boolean IsAssignableTo<TDataType>() => this.Object.IsAssignableTo<TDataType>();
		/// <inheritdoc/>
		internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
			=> this.Object.SetAssignableTo<TDataType>(isAssignable);
		/// <inheritdoc/>
		internal override ReadOnlySpan<Byte> AsSpan() => this.Object.AsSpan();
	}
}