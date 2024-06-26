namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class is used for create an interface view of an object.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
	                 Justification = CommonConstants.InternalInheritanceJustification)]
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	public abstract partial class InterfaceView : View<JLocalObject>, IInterfaceType
	{
		/// <summary>
		/// JNI object reference.
		/// </summary>
		public JObjectLocalRef Reference => this.Object.Reference;

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