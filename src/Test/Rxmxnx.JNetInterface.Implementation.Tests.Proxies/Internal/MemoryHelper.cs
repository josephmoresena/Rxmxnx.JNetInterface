namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
internal sealed unsafe class MemoryHelper
{
	private readonly IntPtr _value;
	private readonly IntPtr[] _values;
	private MemoryHandle _handle;

	public IntPtr Address => (IntPtr)this._handle.Pointer;

	public MemoryHelper(IntPtr value, Int32 count)
	{
		this._value = value;
		this._values = new IntPtr[count];
		this._handle = this._values.AsMemory().Pin();
	}

	public IntPtr Get()
	{
		lock (this._values)
		{
			for (Int32 i = 0; i < this._values.Length; i++)
			{
				if (this._values[i] == this._value) continue;
				Int32 offset = i * sizeof(IntPtr);
				this._values[i] = this._value;
				return this.Address + offset;
			}
		}
		return default;
	}
	public void Free(IntPtr value)
	{
		Int32 index = (value - this.Address).ToInt32();
		lock (this._values)
		{
			if (index < this._values.Length)
				this._values[index] = this._value!;
		}
	}
}