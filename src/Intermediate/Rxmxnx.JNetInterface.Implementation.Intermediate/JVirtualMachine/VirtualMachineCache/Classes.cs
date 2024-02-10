namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	private partial record VirtualMachineCache
	{
		/// <inheritdoc/>
		public override void LoadMainClasses(JEnvironment env)
		{
			base.LoadMainClasses(env);

			this.GlobalClassCache[this.ClassMetadata.Hash] = this.ClassObject;
			this.GlobalClassCache[this.ThrowableMetadata.Hash] = this.ThrowableObject;
			this.GlobalClassCache[this.StackTraceElementMetadata.Hash] = this.StackTraceElementObject;

			this.GlobalClassCache[this.BooleanMetadata.Hash] = this.VoidPrimitive;
			this.GlobalClassCache[this.BooleanMetadata.Hash] = this.BooleanPrimitive;
			this.GlobalClassCache[this.ByteMetadata.Hash] = this.BytePrimitive;
			this.GlobalClassCache[this.CharMetadata.Hash] = this.CharPrimitive;
			this.GlobalClassCache[this.DoubleMetadata.Hash] = this.DoublePrimitive;
			this.GlobalClassCache[this.FloatMetadata.Hash] = this.FloatPrimitive;
			this.GlobalClassCache[this.IntMetadata.Hash] = this.IntPrimitive;
			this.GlobalClassCache[this.LongMetadata.Hash] = this.LongPrimitive;
			this.GlobalClassCache[this.ShortMetadata.Hash] = this.ShortPrimitive;
		}
	}
}