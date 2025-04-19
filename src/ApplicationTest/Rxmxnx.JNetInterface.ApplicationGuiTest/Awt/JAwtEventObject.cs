using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.JNetInterface.Util;

namespace Rxmxnx.JNetInterface.Awt;

public class JAwtEventObject : JEventObject, IClassType<JAwtEventObject>
{
	public enum EventId : UInt32
	{
		Opened = 200,
		Closing = 201,
		Closed = 202,
		Iconified = 203,
		Deiconified = 204,
		Activated = 205,
		Deactivated = 206,
		GainedFocus = 207,
		LostFocus = 208,
		StateChanged = 209,
	}

	private static readonly JFunctionDefinition<JInt>.Parameterless getIdDef = new("getID"u8);
	private static readonly JFunctionDefinition<JLocalObject>.Parameterless getSourceDef = new("getSource"u8);
	private static readonly JClassTypeMetadata<JAwtEventObject> typeMetadata = TypeMetadataBuilder<JEventObject>
	                                                                           .Create<JAwtEventObject>(
		                                                                           "java/awt/AWTEvent"u8,
		                                                                           JTypeModifier.Abstract).Build();
	static JClassTypeMetadata<JAwtEventObject> IClassType<JAwtEventObject>.Metadata => JAwtEventObject.typeMetadata;

	protected JAwtEventObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JAwtEventObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JAwtEventObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public EventId GetId()
	{
		using JClassObject jClass = JClassObject.GetClass<JAwtEventObject>(this.Environment);
		return (EventId)JAwtEventObject.getIdDef.Invoke(this, jClass).Value;
	}
	public JLocalObject? GetSource()
	{
		using JClassObject jClass = JClassObject.GetClass<JAwtEventObject>(this.Environment);
		return JAwtEventObject.getSourceDef.Invoke(this, jClass);
	}

	static JAwtEventObject IClassType<JAwtEventObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JAwtEventObject IClassType<JAwtEventObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JAwtEventObject IClassType<JAwtEventObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}