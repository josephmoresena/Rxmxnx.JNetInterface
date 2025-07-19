namespace Rxmxnx.JNetInterface.ApplicationTest;

public enum NetVersion : Byte
{
	Net80 = 8,
	Net90 = 9,
}

public static class NetVersionExtensions
{
	public static String GetTargetFramework(this NetVersion version)
		=> version switch
		{
			NetVersion.Net80 => "net8.0",
			_ => "net9.0",
		};
}