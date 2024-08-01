namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial class EnvironmentCache
	{
		JClassObject IClassFeature.ClassObject => this.GetLoadedClass(this.ClassObject);
		JClassObject IClassFeature.ThrowableObject => this.GetLoadedClass(this.ThrowableObject);
		JClassObject IClassFeature.StackTraceElementObject => this.GetLoadedClass(this.StackTraceElementObject);

		JClassObject IClassFeature.VoidPrimitive => this.GetLoadedClass(this.VoidPrimitive);
		JClassObject IClassFeature.BooleanPrimitive => this.GetLoadedClass(this.BooleanPrimitive);
		JClassObject IClassFeature.BytePrimitive => this.GetLoadedClass(this.BytePrimitive);
		JClassObject IClassFeature.CharPrimitive => this.GetLoadedClass(this.CharPrimitive);
		JClassObject IClassFeature.DoublePrimitive => this.GetLoadedClass(this.DoublePrimitive);
		JClassObject IClassFeature.FloatPrimitive => this.GetLoadedClass(this.FloatPrimitive);
		JClassObject IClassFeature.IntPrimitive => this.GetLoadedClass(this.IntPrimitive);
		JClassObject IClassFeature.LongPrimitive => this.GetLoadedClass(this.LongPrimitive);
		JClassObject IClassFeature.ShortPrimitive => this.GetLoadedClass(this.ShortPrimitive);
	}
}