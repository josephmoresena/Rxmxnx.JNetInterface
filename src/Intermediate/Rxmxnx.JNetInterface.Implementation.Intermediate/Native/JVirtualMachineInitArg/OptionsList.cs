namespace Rxmxnx.JNetInterface.Native;

public sealed partial class JVirtualMachineInitArg
{
	/// <summary>
	/// Internal option list class.
	/// </summary>
	private sealed class OptionList : IList<JVirtualMachineInitOption>
	{
		/// <summary>
		/// Internal list.
		/// </summary>
		private readonly List<JVirtualMachineInitOption> _list;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="list">Internal list.</param>
		public OptionList(List<JVirtualMachineInitOption> list) => this._list = list;

		JVirtualMachineInitOption IList<JVirtualMachineInitOption>.this[Int32 index]
		{
			get => this._list[index];
			set => this._list[index] = value;
		}
		Int32 ICollection<JVirtualMachineInitOption>.Count => this._list.Count;

		IEnumerator<JVirtualMachineInitOption> IEnumerable<JVirtualMachineInitOption>.GetEnumerator()
			=> this._list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this._list.GetEnumerator();
		void ICollection<JVirtualMachineInitOption>.Add(JVirtualMachineInitOption item) => this._list.Add(item);
		void ICollection<JVirtualMachineInitOption>.Clear() => this._list.Clear();
		Boolean ICollection<JVirtualMachineInitOption>.Contains(JVirtualMachineInitOption item)
			=> this._list.Contains(item);
		void ICollection<JVirtualMachineInitOption>.CopyTo(JVirtualMachineInitOption[] array, Int32 arrayIndex)
			=> this._list.CopyTo(array, arrayIndex);
		Boolean ICollection<JVirtualMachineInitOption>.Remove(JVirtualMachineInitOption item)
			=> this._list.Remove(item);
		Boolean ICollection<JVirtualMachineInitOption>.IsReadOnly
			=> (this._list as ICollection<JVirtualMachineInitOption>).IsReadOnly;
		Int32 IList<JVirtualMachineInitOption>.IndexOf(JVirtualMachineInitOption item) => this._list.IndexOf(item);
		void IList<JVirtualMachineInitOption>.Insert(Int32 index, JVirtualMachineInitOption item)
			=> this._list.Insert(index, item);
		void IList<JVirtualMachineInitOption>.RemoveAt(Int32 index) => this._list.RemoveAt(index);

		/// <inheritdoc/>
		public override String ToString() => $"[{String.Join(", ", this._list.Select(o => o.ToSimplifiedString()))}]";
	}
}