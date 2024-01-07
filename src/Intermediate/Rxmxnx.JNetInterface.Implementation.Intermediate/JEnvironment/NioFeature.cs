namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : INioFeature
	{
		public JBufferObject<TValue> NewDirectByteBuffer<TValue>(IFixedMemory<TValue>.IDisposable memory)
			where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
			=> throw new NotImplementedException();
		public IntPtr GetAddress(JBufferObject buffer) => throw new NotImplementedException();
		public Int32 GetCapacity<TValue>(JBufferObject<TValue> buffer)
			where TValue : unmanaged, IPrimitiveType<TValue>, IBinaryNumber<TValue>
			=> throw new NotImplementedException();
	}
}