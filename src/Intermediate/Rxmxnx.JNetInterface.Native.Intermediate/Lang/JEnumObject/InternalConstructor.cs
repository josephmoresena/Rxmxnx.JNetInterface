namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <inheritdoc/>
	internal JEnumObject(InternalClassInitializer initializer) : base(
		IReferenceType.ClassInitializer.FromInternal(initializer)) { }
	/// <inheritdoc/>
	internal JEnumObject(InternalGlobalInitializer initializer) : base(
		IReferenceType.GlobalInitializer.FromInternal(initializer)) { }
	/// <inheritdoc/>
	internal JEnumObject(InternalObjectInitializer initializer) : base(
		IReferenceType.ObjectInitializer.FromInternal(initializer)) { }
}