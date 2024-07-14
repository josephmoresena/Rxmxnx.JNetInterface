namespace Rxmxnx.JNetInterface.Tests;

public interface IProxyRequest<TTest> where TTest : class, IProxyRequest<TTest>
{
	static abstract UInt32 MaxThreads { get; }
	[ExcludeFromCodeCoverage]
	static virtual UInt32 MaxVms => TTest.MaxThreads;
}