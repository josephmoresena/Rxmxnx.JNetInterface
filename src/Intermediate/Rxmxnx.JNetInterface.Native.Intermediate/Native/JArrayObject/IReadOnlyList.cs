namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject<TElement> : IReadOnlyList<TElement?>
{
	Int32 IReadOnlyCollection<TElement?>.Count => this.Length;
}