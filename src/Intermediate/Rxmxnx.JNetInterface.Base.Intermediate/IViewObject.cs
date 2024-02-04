namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes viewed <see cref="IObject"/>.
/// </summary>
public interface IViewObject : IObject, IWrapper<IObject>
{
	/// <summary>
	/// Real <see cref="IObject"/> instance.
	/// </summary>
	IObject Object { get; }

	CString IObject.ObjectClassName => this.Object.ObjectClassName;
	CString IObject.ObjectSignature => this.Object.ObjectSignature;
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset) => this.Object.CopyTo(span, ref offset);
	void IObject.CopyTo(Span<JValue> span, Int32 index) => this.Object.CopyTo(span, index);

	IObject IWrapper<IObject>.Value => this.Object;
}