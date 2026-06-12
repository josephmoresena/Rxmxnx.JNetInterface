using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using HelloJniLib;

using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal static class ExportedMethods
{
	private static readonly HttpClient httpClient = new();

#pragma warning disable CA2255
	[ModuleInitializer]
	internal static void Initializer()
	{
		AndroidJniHost.Register<AndroidToast>();
		AndroidJniHost.Register<AndroidContext>();
	}
#pragma warning restore CA2255

	public static String GetRuntimeInformation(DateTime call, DateTime? load, Int32 count)
		=> $"Load: {load.GetString()}" + Environment.NewLine + $"Call: {call.GetString()}" + Environment.NewLine +
			$"Count: {count}" + Environment.NewLine + Environment.NewLine +
			$"Number of Cores: {Environment.ProcessorCount}" + Environment.NewLine +
			$"Little-Endian: {BitConverter.IsLittleEndian}" + Environment.NewLine +
			$"OS: {RuntimeInformation.OSDescription}" + Environment.NewLine +
			$"OS Arch: {RuntimeInformation.OSArchitecture.GetName()}" + Environment.NewLine +
			$"OS Version: {Environment.OSVersion}" + Environment.NewLine + $"Computer: {Environment.MachineName}" +
			Environment.NewLine + $"User: {Environment.UserName}" + Environment.NewLine +
			$"System Path: {Environment.SystemDirectory}" + Environment.NewLine +
			$"Current Path: {Environment.CurrentDirectory}" + Environment.NewLine +
			$"Process Arch: {RuntimeInformation.ProcessArchitecture.GetName()}" + Environment.NewLine +
			$"Pubic Ip: {ExportedMethods.GetIp()}" + Environment.NewLine + Environment.NewLine +
			ExportedMethods.GetRuntimeInformation();

	private static String GetRuntimeInformation()
		=> !AotInfo.IsReflectionDisabled ? ExportedMethods.GetRuntimeReflectionInformation() : "REFLECTION DISABLED";
	private static String GetRuntimeReflectionInformation()
		=> $"Framework Version: {Environment.Version}" + Environment.NewLine +
			$"Runtime Name: {RuntimeInformation.FrameworkDescription}" + Environment.NewLine +
			$"Runtime Path: {RuntimeEnvironment.GetRuntimeDirectory()}" + Environment.NewLine +
			$"Runtime Version: {RuntimeEnvironment.GetSystemVersion()}" + Environment.NewLine + Environment.NewLine;
	private static String GetName(this Architecture architecture)
		=> architecture switch
		{
			Architecture.S390x => nameof(Architecture.S390x),
			Architecture.Arm64 => nameof(Architecture.Arm64),
			Architecture.Arm => nameof(Architecture.Arm),
			Architecture.Wasm => nameof(Architecture.Wasm),
			Architecture.X64 => nameof(Architecture.X64),
			Architecture.X86 => nameof(Architecture.X86),
			_ => architecture.ToString(),
		};
	private static String GetString(this DateTime date) => ((DateTime?)date).GetString();
	private static String GetString(this DateTime? date)
		=> date is not null ? date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff") : "null";
	private static String GetIp()
	{
		try
		{
			using HttpRequestMessage request = new(HttpMethod.Get, "https://api64.ipify.org/?format=text");
			using HttpResponseMessage response = ExportedMethods.httpClient.Send(request);
			return response.Content.ReadAsStringAsync().Result;
		}
		catch (Exception e)
		{
			return e.Message;
		}
	}
}