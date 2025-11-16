using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JComponentObject : JLocalObject, IClassType<JComponentObject>
{
	private static readonly IndeterminateCall setVisibleDef = IndeterminateCall.CreateMethodDefinition("setVisible"u8,
#if !NET9_0_OR_GREATER
			[JArgumentMetadata.Get<JBoolean>(),]
#else
			JArgumentMetadata.Get<JBoolean>()
#endif
	);
	private static readonly JFunctionDefinition<JBoolean>.Parameterless isVisibleDef = new("isVisible"u8);
	private static readonly IndeterminateCall setSizeCoordinateDef = IndeterminateCall.CreateMethodDefinition(
		"setSize"u8,
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JInt>(), JArgumentMetadata.Get<JInt>(),]
#else
		JArgumentMetadata.Get<JInt>(), JArgumentMetadata.Get<JInt>()
#endif
	);
	private static readonly JFunctionDefinition<JInt>.Parameterless getHeightDef = new("getHeight"u8);
	private static readonly JFunctionDefinition<JInt>.Parameterless getWidthDef = new("getWidth"u8);
	private static readonly JClassTypeMetadata<JComponentObject> typeMetadata = TypeMetadataBuilder<JComponentObject>
		.Create("java/awt/Component"u8, JTypeModifier.Abstract).Build();

	static JClassTypeMetadata<JComponentObject> IClassType<JComponentObject>.Metadata => JComponentObject.typeMetadata;

	public Int32 Height
	{
		get
		{
			IEnvironment env = this.Environment;
			using JClassObject jClass = JClassObject.GetClass<JComponentObject>(env);
			return JComponentObject.getHeightDef.Invoke(this, jClass).Value;
		}
	}
	public Int32 Width
	{
		get
		{
			IEnvironment env = this.Environment;
			using JClassObject jClass = JClassObject.GetClass<JComponentObject>(env);
			return JComponentObject.getWidthDef.Invoke(this, jClass).Value;
		}
	}

	protected JComponentObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JComponentObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JComponentObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void SetVisible(Boolean visible)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JComponentObject>(env);
		JComponentObject.setVisibleDef.MethodCall(this, jClass, false,
#if !NET9_0_OR_GREATER
		                                          [(JBoolean)visible,]
#else
		                                          (JBoolean)visible
#endif
		);
	}
	public Boolean IsVisible()
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JComponentObject>(env);
		return JComponentObject.isVisibleDef.Invoke(this, jClass).Value;
	}

	public void SetSize(Int32 width, Int32 height)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JComponentObject>(env);
		JComponentObject.setSizeCoordinateDef.MethodCall(this, jClass, false,
#if !NET9_0_OR_GREATER
		                                                 [(JInt)width, (JInt)height,]
#else
		                                                 (JInt)width, (JInt)height
#endif
		);
	}

	static JComponentObject IClassType<JComponentObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JComponentObject IClassType<JComponentObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JComponentObject IClassType<JComponentObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}