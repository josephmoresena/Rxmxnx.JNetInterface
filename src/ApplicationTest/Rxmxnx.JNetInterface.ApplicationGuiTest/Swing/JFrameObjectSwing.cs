using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JFrameObjectSwing : JFrameObjectAwt, IClassType<JFrameObjectSwing>
{
	public enum CloseOperation
	{
		Dispose = 2,
		DoNothing = 0,
		Exit = 3,
		Hide = 1,
	}

	private static readonly IndeterminateCall titleConstructorDef =
		IndeterminateCall.CreateConstructorDefinition([JArgumentMetadata.Get<JStringObject>(),]);
	private static readonly IndeterminateCall setDefaultCloseOperationDef =
		IndeterminateCall.CreateMethodDefinition("setDefaultCloseOperation"u8, [JArgumentMetadata.Get<JInt>(),]);
	private static readonly IndeterminateCall setContentPaneDef =
		IndeterminateCall.CreateMethodDefinition("setContentPane"u8, [JArgumentMetadata.Get<JContainerObject>(),]);
	private static readonly JClassTypeMetadata<JFrameObjectSwing> typeMetadata =
		TypeMetadataBuilder<JFrameObjectAwt>.Create<JFrameObjectSwing>("javax/swing/JFrame"u8).Build();
	static JClassTypeMetadata<JFrameObjectSwing> IClassType<JFrameObjectSwing>.Metadata
		=> JFrameObjectSwing.typeMetadata;

	protected JFrameObjectSwing(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JFrameObjectSwing(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JFrameObjectSwing(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void SetCloseOperation(CloseOperation operation)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JFrameObjectSwing>(env);
		JFrameObjectSwing.setDefaultCloseOperationDef.MethodCall(this, jClass, false, [(JInt)(Int32)operation,]);
	}
	public void SetContentPane(JContainerObject contentPane)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JFrameObjectSwing>(env);
		JFrameObjectSwing.setContentPaneDef.MethodCall(this, jClass, false, [contentPane,]);
	}

	public static JFrameObjectSwing Create(IEnvironment env, String title)
	{
		using JStringObject jString = JStringObject.Create(env, title);
		return JFrameObjectSwing.titleConstructorDef.NewCall<JFrameObjectSwing>(env, [jString,]);
	}

	static JFrameObjectSwing IClassType<JFrameObjectSwing>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JFrameObjectSwing IClassType<JFrameObjectSwing>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JFrameObjectSwing IClassType<JFrameObjectSwing>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}