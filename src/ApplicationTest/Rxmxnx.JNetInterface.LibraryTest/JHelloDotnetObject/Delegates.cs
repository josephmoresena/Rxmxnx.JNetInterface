using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JHelloDotnetObject
{
	private delegate JStringLocalRef GetStringDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);
	private delegate JInt GetThreadIdDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);

	private delegate void PrintRuntimeInformationDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JStringLocalRef stringRef);

	private delegate JObjectLocalRef SumArrayDelegate(JEnvironmentRef envRef, JClassLocalRef classRef,
		JIntArrayLocalRef intArrayRef);

	private delegate JArrayLocalRef GetIntArrayArrayDelegate(JEnvironmentRef envRef, JClassLocalRef classRef,
		Int32 length);

	private delegate JArrayLocalRef GetClassArrayDelegate(JEnvironmentRef envRef, JClassLocalRef classRef);
	private delegate JClassLocalRef GetClassDelegate(JEnvironmentRef envRef, JClassLocalRef classRef);
}