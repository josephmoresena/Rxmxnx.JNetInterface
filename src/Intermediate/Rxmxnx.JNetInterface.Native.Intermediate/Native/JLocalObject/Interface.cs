namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// This class is used for create an interface view of an object.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract partial class Interface : View<JLocalObject>, IInterfaceType
	{
		static JTypeKind IDataType.Kind => JTypeKind.Interface;
		static Type IDataType.FamilyType => typeof(Interface);

		/// <inheritdoc/>
		private protected Interface(JLocalObject jObject) : base(jObject) { }
	}
}