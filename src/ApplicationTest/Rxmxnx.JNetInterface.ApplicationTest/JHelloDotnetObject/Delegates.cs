using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
	private delegate JStringLocalRef GetStringDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);
	private delegate JInt GetIntDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);

	private delegate void PassStringDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JStringLocalRef stringRef);

	private delegate JObjectLocalRef SumArrayDelegate(JEnvironmentRef envRef, JClassLocalRef classRef,
		JIntArrayLocalRef intArrayRef);

	private delegate JArrayLocalRef GetIntArrayArrayDelegate(JEnvironmentRef envRef, JClassLocalRef classRef,
		Int32 length);
}