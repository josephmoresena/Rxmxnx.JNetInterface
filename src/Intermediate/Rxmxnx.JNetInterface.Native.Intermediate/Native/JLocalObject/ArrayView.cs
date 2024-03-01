namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class is used for create a view of an array object.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract partial class ArrayView : View<JArrayObject>, IDataType, IDisposable
	{
		static JTypeKind IDataType.Kind => JTypeKind.Array;
		static Type IDataType.FamilyType => typeof(JArrayObject);

		/// <summary>
		/// Array length.
		/// </summary>
		public Int32 Length => this.Object.Length;

		/// <inheritdoc/>
		private protected ArrayView(JArrayObject jObject) : base(jObject) { }

		/// <inheritdoc/>
		public void Dispose()
		{
			this.Object.Dispose();
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Retrieves a <see cref="JArrayObject"/> instance from <paramref name="jLocal"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="dispose">
		/// Optional. Indicates whether current instance should be disposed after casting.
		/// </param>
		/// <returns>A <see cref="JArrayObject"/> instance from current global instance.</returns>
		internal static JArrayObject ParseArray<TElement>(JLocalObject jLocal, Boolean dispose = false)
			where TElement : IObject, IDataType<TElement>
		{
			IEnvironment env = jLocal.Environment;
			if (jLocal is JArrayObject result) return result;
			result = new Generic<TElement>(jLocal, env.ClassFeature.GetClass<JArrayObject<TElement>>());
			if (dispose) jLocal.Dispose();
			return result;
		}
	}
}