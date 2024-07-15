namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
internal sealed unsafe class MemoryHelper<TPointer> where TPointer : unmanaged, IFixedPointer
{
	private readonly IntPtr _noValue;
	private readonly IntPtr _value;
	private readonly IntPtr[] _values;

	private MemoryHandle _externalHandle;
	private MemoryHandle _localHandle;

	public MemoryHelper(MemoryHandle handle, Int32 count)
	{
		this._externalHandle = handle;
		this._values = new IntPtr[count];
		this._value = (IntPtr)handle.Pointer;
		this._noValue = this._value + IntPtr.Size;
		this._localHandle = this._values.AsMemory().Pin();
	}

	public TPointer Get()
	{
		lock (this._values)
		{
			for (Int32 i = 0; i < this._values.Length; i++)
			{
				if (this._values[i] == this._value) continue;
				Int32 offset = i * sizeof(TPointer);
				this._values[i] = this._value;
				IntPtr result = (IntPtr)this._localHandle.Pointer + offset;
				return NativeUtilities.Transform<IntPtr, TPointer>(in result);
			}
		}
		throw new InsufficientMemoryException();
	}
	public Boolean Free(TPointer value)
	{
		Boolean result = false;
		IntPtr ptr = (IntPtr)this._localHandle.Pointer;
		lock (this._values)
		{
			Int32 index = MemoryHelper<TPointer>.GetIndex(value.Pointer, ptr, this._values.Length);
			if (index < 0) return result;
			result = this._values[index] == this._value;
			this._values[index] = this._noValue;
		}
		return result;
	}
	public void Free(Action<TPointer> free)
	{
		lock (this._values)
		{
			Span<IntPtr> span = this._values;
			foreach (ref IntPtr value in span)
			{
				try
				{
					if (value != this._value) continue;
					IntPtr valuePtr = value.GetUnsafeIntPtr();
					free(NativeUtilities.Transform<IntPtr, TPointer>(in valuePtr));
				}
				catch (Exception)
				{
					// ignored
				}
				finally
				{
					value = this._noValue;
				}
			}
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