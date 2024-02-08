namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IObjectReferenceType : IFixedPointer, IWrapper<JObjectLocalRef>, INativeType;

/// <summary>
/// This interface exposes a java object local reference.
/// </summary>
/// <typeparam name="TObject">Type of <see cref="IArrayReferenceType{TArray}"/>.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IObjectReferenceType<TObject> : IObjectReferenceType, INativeType<TObject>
	where TObject : unmanaged, IObjectReferenceType<TObject>;