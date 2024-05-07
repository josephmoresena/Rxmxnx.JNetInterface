namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial record JClassTypeMetadata<TClass>
{
	/// <summary>
	/// This record stores the view metadata for a class <see cref="IDataType"/> type.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract record View : JClassTypeMetadata<TClass>
	{
		/// <summary>
		/// Internal instance.
		/// </summary>
		private readonly JClassTypeMetadata _metadata;

		/// <inheritdoc/>
		public override JClassTypeMetadata? BaseMetadata => this._metadata.BaseMetadata;
		/// <inheritdoc/>
		public override JTypeModifier Modifier => this._metadata.Modifier;
		/// <inheritdoc/>
		public override IInterfaceSet Interfaces => this._metadata.Interfaces;

		/// <inheritdoc/>
		private protected View(JClassTypeMetadata<TClass> metadata) : base(metadata) => this._metadata = metadata;

		/// <inheritdoc/>
		internal override Boolean IsInstance(JReferenceObject jObject) => this._metadata.IsInstance(jObject);
		/// <inheritdoc/>
		public override String ToString() => base.ToString();

		/// <inheritdoc/>
		internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
			Boolean realClass = false)
			=> this._metadata.CreateInstance(jClass, localRef, realClass);
		/// <inheritdoc/>
		internal override JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false)
			=> this._metadata.ParseInstance(jLocal, dispose);
		/// <inheritdoc/>
		internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
			=> this._metadata.ParseInstance(env, jGlobal);
	}
}