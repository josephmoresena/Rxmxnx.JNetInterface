namespace Rxmxnx.JNetInterface.ApplicationTest.Util;

public sealed partial class NetVersionParser
{
	[GeneratedRegex(@".+\.net(\d{1,}\.\d).{0,}")]
	private static partial Regex NetVersionRegex();

	public static Decimal? GetNetVersion(String? input)
	{
		if (String.IsNullOrEmpty(input)) return default;

		Match match = NetVersionParser.NetVersionRegex().Match(input);
		if (!match.Success) return default;

		return Decimal.TryParse(match.Groups[1].Value, out Decimal result) ? result : default(Decimal?);
	}
}