namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
internal sealed unsafe class MemoryHelper<TPointer> where TPointer : unmanaged, IFixedPointer
{
	private readonly TPointer _value;
	private readonly TPointer[] _values;

	private MemoryHandle _externalHandle;
	private MemoryHandle _localHandle;

	public MemoryHelper(IntPtr value, Int32 count)
	{
		this._value = NativeUtilities.Transform<IntPtr, TPointer>(in value);
		this._values = new TPointer[count];
		this._localHandle = this._values.AsMemory().Pin();
	}
	public MemoryHelper(MemoryHandle handle, Int32 count)
	{
		this._externalHandle = handle;
		IntPtr value = (IntPtr)handle.Pointer;
		this._value = NativeUtilities.Transform<IntPtr, TPointer>(in value);
		this._values = new TPointer[count];
		this._localHandle = this._values.AsMemory().Pin();
	}
	~MemoryHelper()
	{
		this._localHandle.Dispose();
		this._externalHandle.Dispose();
	}

	public TPointer Get()
	{
		lock (this._values)
		{
			for (Int32 i = 0; i < this._values.Length; i++)
			{
				if (this._values[i].Pointer == this._value.Pointer) continue;
				Int32 offset = i * sizeof(TPointer);
				this._values[i] = this._value;
				IntPtr result = (IntPtr)this._localHandle.Pointer + offset;
				return NativeUtilities.Transform<IntPtr, TPointer>(result);
			}
		}
		throw new InsufficientMemoryException();
	}
	public Boolean Free(TPointer value)
	{
		Boolean result = false;
		Int32 index = (value.Pointer - (IntPtr)this._localHandle.Pointer).ToInt32();
		lock (this._values)
		{
			if (index >= this._values.Length) return result;
			result = this._values[index].Pointer == this._value.Pointer;
			this._values[index] = this._value;
		}
		return result;
	}
}