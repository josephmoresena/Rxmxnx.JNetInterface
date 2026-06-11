namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This struct holds the minimal JNI object information.
/// </summary>
/// <param name="reference">A JNI reference.</param>
/// <param name="jniClassName">The JNI object class name.</param>
internal struct JniObjectInfo(JniObjectReference reference, String? jniClassName)
{
	public JniObjectReference Reference = reference;
	public readonly String? JniClassName = jniClassName;
}