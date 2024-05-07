namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IClassFeature
{
	/// <summary>
	/// <c>void</c> class instance.
	/// </summary>
	JClassObject VoidPrimitive { get; }
	/// <summary>
	/// <c>boolean</c> class instance.
	/// </summary>
	JClassObject BooleanPrimitive { get; }
	/// <summary>
	/// <c>byte</c> class instance.
	/// </summary>
	JClassObject BytePrimitive { get; }
	/// <summary>
	/// <c>char</c> class instance.
	/// </summary>
	JClassObject CharPrimitive { get; }
	/// <summary>
	/// <c>double</c> class instance.
	/// </summary>
	JClassObject DoublePrimitive { get; }
	/// <summary>
	/// <c>float</c> class instance.
	/// </summary>
	JClassObject FloatPrimitive { get; }
	/// <summary>
	/// <c>int</c> class instance.
	/// </summary>
	JClassObject IntPrimitive { get; }
	/// <summary>
	/// <c>long</c> class instance.
	/// </summary>
	JClassObject LongPrimitive { get; }
	/// <summary>
	/// <c>short</c> class instance.
	/// </summary>
	JClassObject ShortPrimitive { get; }

	/// <summary>
	/// <c>java.lang.Object</c> class instance.
	/// </summary>
	JClassObject Object => this.GetClass<JLocalObject>();
	/// <summary>
	/// <c>java.lang.Class&lt;?&gt;</c> class instance.
	/// </summary>
	JClassObject ClassObject { get; }
	/// <summary>
	/// <c>java.lang.Throwable</c> class instance.
	/// </summary>
	JClassObject ThrowableObject => this.GetClass<JThrowableObject>();
	/// <summary>
	/// <c>java.lang.StackTraceElementObject</c> class instance.
	/// </summary>
	JClassObject StackTraceElementObject => this.GetClass<JStackTraceElementObject>();
	/// <summary>
	/// <c>java.lang.String</c> class instance.
	/// </summary>
	JClassObject StringObject => this.GetClass<JStringObject>();
	/// <summary>
	/// <c>java.lang.Number</c> class instance.
	/// </summary>
	JClassObject NumberObject => this.GetClass<JNumberObject>();
	/// <summary>
	/// <c>java.lang.ClassLoader</c> class instance.
	/// </summary>
	JClassObject ClassLoaderObject => this.GetClass<JClassLoaderObject>();
	/// <summary>
	/// <c>java.nio.Buffer</c> class instance.
	/// </summary>
	JClassObject BufferObject => this.GetClass<JBufferObject>();
	/// <summary>
	/// <c>java.lang.reflect.Method</c> class instance.
	/// </summary>
	JClassObject MethodObject => this.GetClass<JMethodObject>();
	/// <summary>
	/// <c>java.lang.reflect.Constructor</c> class instance.
	/// </summary>
	JClassObject ConstructorObject => this.GetClass<JConstructorObject>();
	/// <summary>
	/// <c>java.lang.reflect.Field</c> class instance.
	/// </summary>
	JClassObject FieldObject => this.GetClass<JFieldObject>();
	/// <summary>
	/// <c>java.lang.Enum</c> class instance.
	/// </summary>
	JClassObject EnumObject => this.GetClass<JEnumObject>();

	/// <summary>
	/// <c>java.lang.Exception</c> class instance.
	/// </summary>
	JClassObject ExceptionObject => this.GetClass<JExceptionObject>();
	/// <summary>
	/// <c>java.lang.Error</c> class instance.
	/// </summary>
	JClassObject ErrorObject => this.GetClass<JErrorObject>();

	/// <summary>
	/// <c>java.lang.Void</c> class instance.
	/// </summary>
	JClassObject VoidObject { get; }
	/// <summary>
	/// <c>java.lang.Boolean</c> class instance.
	/// </summary>
	JClassObject BooleanObject { get; }
	/// <summary>
	/// <c>java.lang.Byte</c> class instance.
	/// </summary>
	JClassObject ByteObject { get; }
	/// <summary>
	/// <c>java.lang.Character</c> class instance.
	/// </summary>
	JClassObject CharacterObject { get; }
	/// <summary>
	/// <c>java.lang.Double</c> class instance.
	/// </summary>
	JClassObject DoubleObject { get; }
	/// <summary>
	/// <c>java.lang.Float</c> class instance.
	/// </summary>
	JClassObject FloatObject { get; }
	/// <summary>
	/// <c>java.lang.Integer</c> class instance.
	/// </summary>
	JClassObject IntegerObject { get; }
	/// <summary>
	/// <c>java.lang.Long</c> class instance.
	/// </summary>
	JClassObject LongObject { get; }
	/// <summary>
	/// <c>java.lang.Short</c> class instance.
	/// </summary>
	JClassObject ShortObject { get; }
}