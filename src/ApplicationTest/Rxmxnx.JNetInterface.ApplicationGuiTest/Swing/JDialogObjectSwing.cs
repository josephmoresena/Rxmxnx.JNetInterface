using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Swing;

public class JDialogObjectSwing : JDialogObject, IClassType<JDialogObjectSwing>,
	IInterfaceObject<JRootPaneContainerObject>
{
	private static readonly IndeterminateCall constructorDef = IndeterminateCall.CreateConstructorDefinition(
#if !NET9_0_OR_GREATER
		[
			JArgumentMetadata.Get<JFrameObjectAwt>(), JArgumentMetadata.Get<JStringObject>(),
			JArgumentMetadata.Get<JBoolean>(),
		]
#else
		JArgumentMetadata.Get<JFrameObjectAwt>(), JArgumentMetadata.Get<JStringObject>(),
		JArgumentMetadata.Get<JBoolean>()
#endif
	);
	private static readonly JClassTypeMetadata<JDialogObjectSwing> typeMetadata = TypeMetadataBuilder<JDialogObject>
		.Create<JDialogObjectSwing>("javax/swing/JDialog"u8).Implements<JRootPaneContainerObject>().Build();
	static JClassTypeMetadata<JDialogObjectSwing> IClassType<JDialogObjectSwing>.Metadata
		=> JDialogObjectSwing.typeMetadata;
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd2;
#if !NET8_0_OR_GREATER
	// .NET 7.0 has issues inheriting static abstract members in non-generic interfaces from base classes.
	static Int32 IDataType.AndroidApiLevel => -1;
#endif

	protected JDialogObjectSwing(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JDialogObjectSwing(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JDialogObjectSwing(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public new void Add(JComponentObject component, JLocalObject constraints)
	{
		if (this.Environment.VirtualMachine.Version >= JRuntimeVersion.J5)
		{
			// For JDK 1.5 and later, use the add method directly.
			base.Add(component, constraints);
			return;
		}

		// Cast the javax.swing.JDialog instance to javax.swing.RootPaneContainer
		JRootPaneContainerObject rootPane = this.CastTo<JRootPaneContainerObject>();
		using JContainerObject? contentContainer = rootPane.GetContentPane();
		contentContainer?.Add(component, constraints);
	}

	public static JDialogObjectSwing Create(JFrameObjectAwt frame, String title, Boolean modal)
	{
		IEnvironment env = frame.Environment;
		using JClassObject jClass = JClassObject.GetClass<JDialogObjectSwing>(env);
		using JStringObject jString = JStringObject.Create(env, title);
#if !NET9_0_OR_GREATER
		return JDialogObjectSwing.constructorDef.NewCall<JDialogObjectSwing>(env, [frame, jString, (JBoolean)modal,]);
#else
		return JDialogObjectSwing.constructorDef.NewCall<JDialogObjectSwing>(env, frame, jString, (JBoolean)modal);
#endif
	}

	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JDialogObjectSwing IClassType<JDialogObjectSwing>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}