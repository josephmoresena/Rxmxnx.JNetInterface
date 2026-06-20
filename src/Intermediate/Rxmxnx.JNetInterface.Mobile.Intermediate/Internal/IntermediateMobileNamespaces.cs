#if DEFINE_JAVA_INTEROP && !PACKAGE && !NET9_0_OR_GREATER
/*
 * This file is required only for the intermediate compilation step performed
 * during NuGet package generation.
 *
 * Since the intermediate build cannot resolve the Java.Interop NuGet dependency,
 * any Java.Interop APIs referenced by the generated sources must be declared
 * here as placeholders.
 *
 * When new Java.Interop APIs are introduced, remember to add the corresponding
 * declarations to this file. Do not remove this file, as it is required for the
 * intermediate compilation even though the final NuGet package is built against
 * the actual Java.Interop dependency.
 */
namespace Java.Interop 
{
    [Flags]
	public enum JniObjectReferenceOptions
	{
		None,
		Copy,
		CopyAndDispose,
        CopyAndDoNotRegister,
	}
    public enum JniObjectReferenceType
    {
		Invalid,
		Local,
		Global,
		WeakGlobal,
	}
    public interface IJavaPeerable 
    {
        JniObjectReference PeerReference  { get; }
        String? GetJniTypeName() => default;
    }
    public struct JniObjectReference 
    {
        public IntPtr Handle => IntPtr.Zero;
        public Boolean IsValid => false;
        public JniObjectReference NewWeakGlobalRef() => default;
        public JniObjectReference(IntPtr handle, JniObjectReferenceType type = JniObjectReferenceType.Invalid) { }
        public override Boolean Equals(Object? o) => false;
        public override Int32 GetHashCode() => this.Handle.GetHashCode();
        public static void Dispose(ref JniObjectReference reference) { }
        public static Boolean operator == (JniObjectReference lhs, JniObjectReference rhs) => false;
        public static Boolean operator != (JniObjectReference lhs, JniObjectReference rhs) => true;
    }
    public sealed class JniRuntime
    {
        public static readonly JniRuntime CurrentRuntime = new();
        public readonly JniValueManager ValueManager = default!;
        public IntPtr InvocationPointer => IntPtr.Zero;
        public void FailFast(String? message) { }
        public void DestroyRuntime() { }
        public static JniRuntime? GetRegisteredRuntime(IntPtr invocationPointer) => default;
        
        public abstract partial class JniValueManager
        {
            [return: MaybeNull]
			public abstract T GetValue<[DynamicallyAccessedMembers(Rxmxnx.JNetInterface.AndroidJniExtensions.JavaObjectMembers)] T>(ref JniObjectReference reference, JniObjectReferenceOptions options);
            public abstract void CollectPeers();
        }
    }
    public static class JniEnvironment
    {
        public static IntPtr EnvironmentPointer => IntPtr.Zero;
        public static class Exceptions
        {
            public static JniObjectReference ExceptionOccurred() => default;
            public static Boolean ExceptionCheck() => false;
        }
    }
    public sealed class JavaException : Exception
    {
        public unsafe JavaException (ref JniObjectReference reference, JniObjectReferenceOptions transfer) { }
    }
}
#endif