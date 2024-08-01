namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
internal sealed unsafe class MemoryHelper<TPointer> where TPointer : unmanaged, IFixedPointer
{
	private readonly HashSet<Int32> _busy;
	private readonly IntPtr _noValue;
	private readonly Queue<Int32> _toFree = new(Byte.MaxValue);
	private readonly IntPtr _value;
	private readonly IntPtr[] _values;

	private MemoryHandle _externalHandle;
	private MemoryHandle _localHandle;

	public MemoryHelper(MemoryHandle handle, Int32 sizeOf, Int32 count)
	{
		this._externalHandle = handle;
		this._values = new IntPtr[count];
		this._value = (IntPtr)handle.Pointer;
		this._noValue = this._value + sizeOf;
		this._busy = new(count);
		this._localHandle = this._values.AsMemory().Pin();
	}

	public TPointer Get()
	{
		lock (this._values)
		{
			while (this._busy.Count < this._values.Length)
			{
				Int32 index = Random.Shared.Next(0, this._values.Length);
				if (!this._busy.Add(index)) continue;
				this._values[index] = this._value;
				Int32 offset = index * sizeof(TPointer);
				IntPtr result = (IntPtr)this._localHandle.Pointer + offset;
				this.Free(index);
				return NativeUtilities.Transform<IntPtr, TPointer>(in result);
			}
		}
		throw new InsufficientMemoryException();
	}
	public void Free(TPointer value)
	{
		IntPtr ptr = (IntPtr)this._localHandle.Pointer;
		lock (this._values)
		{
			Int32 index = MemoryHelper<TPointer>.GetIndex(value.Pointer, ptr, this._values.Length);
			if (index < 0) return;
			this._values[index] = this._noValue;
			this._toFree.Enqueue(index);
		}
	}
	private void Free(Int32 exclude)
	{
		if (this._toFree.Count < Byte.MaxValue) return;
		while (this._toFree.Count > SByte.MaxValue)
		{
			Int32 indexToFree = this._toFree.Dequeue();
			if (indexToFree != exclude) this._busy.Remove(indexToFree);
		}
	}
	private void ReleaseUnmanagedResources()
	{
		this._externalHandle.Dispose();
		this._localHandle.Dispose();
	}
	~MemoryHelper() { this.ReleaseUnmanagedResources(); }
	private static Int32 GetIndex(IntPtr value, IntPtr ptr, Int32 length)
	{
		if (value < ptr) return -1;
		Int32 result = 0;
		while (ptr + result * sizeof(TPointer) < value)
		{
			if (result >= length) return -1;
			result++;
		}
		return result;
	}
}