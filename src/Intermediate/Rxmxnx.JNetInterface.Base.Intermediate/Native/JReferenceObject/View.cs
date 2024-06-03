namespace Rxmxnx.JNetInterface.Native;

public partial class JReferenceObject
{
	/// <summary>
	/// This class is used for create view instances of object.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class View : JReferenceObject, IViewObject
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		private protected View(JReferenceObject jObject) : base(jObject) { }
		/// <inheritdoc cref="IObject.ObjectClassName"/>
		public override CString ObjectClassName => this.GetObject().ObjectClassName;
		/// <inheritdoc cref="IObject.ObjectSignature"/>
		public override CString ObjectSignature => this.GetObject().ObjectSignature;

		IObject IViewObject.Object => this.GetObject();

		/// <inheritdoc/>
		private protected override Boolean IsInstanceOf<TDataType>() => this.GetObject().IsInstanceOf<TDataType>();

		/// <inheritdoc/>
		internal override Boolean IsDefaultInstance() => this.GetObject().IsDefaultInstance();
		/// <inheritdoc/>
		internal override void ClearValue() => this.GetObject().ClearValue();
		/// <inheritdoc/>
		internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
			=> this.GetObject().SetAssignableTo<TDataType>(isAssignable);
		/// <inheritdoc/>
		private protected override IDisposable GetSynchronizer() => this.GetObject().GetSynchronizer();
		/// <inheritdoc/>
		private protected override ReadOnlySpan<Byte> AsSpan() => this.GetObject().AsSpan();
		/// <inheritdoc/>
		[ExcludeFromCodeCoverage]
		private protected override void CopyTo(Span<Byte> span, ref Int32 offset) => this.GetObject().AsSpan();
		/// <inheritdoc/>
		[ExcludeFromCodeCoverage]
		private protected override void CopyTo(Span<JValue> span, Int32 index) => this.GetObject().AsSpan();
		/// <inheritdoc/>
		[ExcludeFromCodeCoverage]
		private protected override Boolean Same(JReferenceObject jObject)
			=> Object.ReferenceEquals(this, jObject) || this.GetObject().Same(jObject);

		/// <inheritdoc/>
		[ExcludeFromCodeCoverage]
		public override String ToTraceText() => this.GetObject().ToTraceText();

		/// <summary>
		/// Retrieves viewed object.
		/// </summary>
		/// <returns>A <see cref="JReferenceObject"/> instance.</returns>
		private protected abstract JReferenceObject GetObject();
	}

	/// <summary>
	/// This class is used for create view instances of object.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IObject"/> instance.</typeparam>
	public abstract class View<TObject> : View, IWrapper<TObject> where TObject : JReferenceObject, IObject
	{
		/// <summary>
		/// <typeparamref name="TObject"/> instance.
		/// </summary>
		public TObject Object { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jObject">A <typeparamref name="TObject"/> instance.</param>
		private protected View(TObject jObject) : base(jObject) => this.Object = jObject;

		TObject IWrapper<TObject>.Value => this.Object;

		/// <inheritdoc/>
		private protected override JReferenceObject GetObject() => this.Object;
	}
}