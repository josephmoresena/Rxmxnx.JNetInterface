namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JNumberFormatExceptionObject>;

/// <summary>
/// This class represents a local <c>java.lang.NumberFormatException</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public class JNumberFormatExceptionObject : JIllegalArgumentExceptionObject,
	IThrowableType<JNumberFormatExceptionObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.NumberFormatExceptionHash, 31);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JNumberFormatExceptionObject>(JNumberFormatExceptionObject.typeInfo,
		                                                                     IClassType
			                                                                     .GetMetadata<
				                                                                     JIllegalArgumentExceptionObject>(),
		                                                                     JTypeModifier.Extensible));

	static TypeMetadata IThrowableType<JNumberFormatExceptionObject>.Metadata
		=> JNumberFormatExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JNumberFormatExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNumberFormatExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JNumberFormatExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JNumberFormatExceptionObject IClassType<JNumberFormatExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNumberFormatExceptionObject IClassType<JNumberFormatExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNumberFormatExceptionObject IClassType<JNumberFormatExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}