namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class is used for create an interface view of an object.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract partial class InterfaceView : View<JLocalObject>, IInterfaceType
	{
		static JTypeKind IDataType.Kind => JTypeKind.Interface;
		static Type IDataType.FamilyType => typeof(InterfaceView);

		/// <inheritdoc/>
		private protected InterfaceView(JLocalObject jObject) : base(jObject) { }

		/// <inheritdoc/>
		public void Dispose()
		{
			this.Object.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}