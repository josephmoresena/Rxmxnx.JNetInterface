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

		/// <summary>
		/// JNI array reference.
		/// </summary>
		internal JArrayLocalRef Reference => base.As<JArrayLocalRef>();

		/// <inheritdoc/>
		private protected ArrayView(JArrayObject jObject) : base(jObject) { }

		/// <inheritdoc/>
		public void Dispose()
		{
			this.Object.Dispose();
			GC.SuppressFinalize(this);
		}

		/// <inheritdoc cref="JReferenceObject.As{TValue}()"/>
		internal new ref readonly TValue As<TValue>() where TValue : unmanaged, IArrayReferenceType<TValue>
			=> ref base.As<TValue>();
	}
}