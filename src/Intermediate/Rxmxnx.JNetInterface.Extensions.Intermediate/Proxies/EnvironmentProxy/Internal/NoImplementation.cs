namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	#region IReferenceFeature
	void IReferenceFeature.MonitorEnter(JObjectLocalRef localRef) { }
	void IReferenceFeature.MonitorExit(JObjectLocalRef localRef) { }
	#endregion

	#region IArrayFeature
	IntPtr IArrayFeature.GetPrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, out Boolean isCopy)
	{
		isCopy = false;
		return IntPtr.Zero;
	}
	ValPtr<Byte> IArrayFeature.GetPrimitiveCriticalSequence(JArrayLocalRef arrayRef) => ValPtr<Byte>.Zero;
	void IArrayFeature.
		ReleasePrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, IntPtr pointer, JReleaseMode mode) { }
	void IArrayFeature.ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr) { }
	#endregion

	#region IStringFeature
	ReadOnlyValPtr<Char> IStringFeature.GetSequence(JStringLocalRef stringRef, out Boolean isCopy)
	{
		isCopy = false;
		return ReadOnlyValPtr<Char>.Zero;
	}
	ReadOnlyValPtr<Byte> IStringFeature.GetUtf8Sequence(JStringLocalRef stringRef, out Boolean isCopy)
	{
		isCopy = false;
		return ReadOnlyValPtr<Byte>.Zero;
	}
	ReadOnlyValPtr<Char> IStringFeature.GetCriticalSequence(JStringLocalRef stringRef) => ReadOnlyValPtr<Char>.Zero;
	void IStringFeature.ReleaseSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer) { }
	void IStringFeature.ReleaseUtf8Sequence(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer) { }
	void IStringFeature.ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer) { }
	#endregion

	#region IAccessFeature
	JMethodId IAccessFeature.GetMethodId(JExecutableObject jExecutable) => default;
	JFieldId IAccessFeature.GetFieldId(JFieldObject jField) => default;
	#endregion
}