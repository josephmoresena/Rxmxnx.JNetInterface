namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IEnumProvider
	{
		public String GetName(JEnumObject jEnum, out Int32 ordinal) => throw new NotImplementedException();
		public Int32 GetOrdinal(JEnumObject jEnum) => throw new NotImplementedException();
	}
}