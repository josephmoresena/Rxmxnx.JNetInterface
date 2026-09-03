namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public interface IExecutionNotifier
{
	Int32 RefreshTime { get; }
	void Begin(ProcessStartInfo info);
	void End(ProcessStartInfo info);
	// ReSharper disable once UnusedMemberInSuper.Global
	void Result(Int32 result, String executionName);
}