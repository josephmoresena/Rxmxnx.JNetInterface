using System.Diagnostics;

namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public interface IExecutionNotifier
{
	Int32 RefreshTime { get; }
	void Begin(ProcessStartInfo info);
	void End(ProcessStartInfo info);
}