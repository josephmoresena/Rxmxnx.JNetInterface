using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Program : IManagedCallback
{
	private readonly IVirtualMachine _vm;

	private Program(IVirtualMachine vm) => this._vm = vm;

	IVirtualMachine IManagedCallback.VirtualMachine => this._vm;

	JStringObject IManagedCallback.GetString(JLocalObject jLocal)
	{
		IEnvironment env = jLocal.Environment;
		return JStringObject.Create(env, $"Hello from .NET, {Environment.MachineName}");
	}
	JInt IManagedCallback.GetInt(JLocalObject jLocal) => Environment.CurrentManagedThreadId;
	void IManagedCallback.PassString(JLocalObject jLocal, JStringObject? jString) => Console.WriteLine(jString?.Value);
	void IManagedCallback.AccessStringField(JLocalObject jLocal)
	{
		IEnvironment env = jLocal.Environment;
		JFieldDefinition<JStringObject> sField = new("s_field"u8);
		using JStringObject? jString = sField.Get(jLocal);
		Console.WriteLine($"s_field -> {jString?.Value}");
		using JNativeMemory<Byte>? utf8Chars = jString?.GetNativeUtf8Chars();
		ReadOnlySpan<Byte> span = utf8Chars is not null ? utf8Chars.Values : ReadOnlySpan<Byte>.Empty;
		using JStringObject newString = JStringObject.Create(env, Convert.ToHexString(span));
		sField.Set(jLocal, newString);
	}
	void IManagedCallback.Throw(JLocalObject jLocal)
	{
		IEnvironment env = jLocal.Environment;
		try
		{
			JMethodDefinition.Parameterless throwMethod = new("throwException"u8);
			throwMethod.Invoke(jLocal);
		}
		catch (Exception e)
		{
			env.DescribeException();
			if (e is ThrowableException te)
			{
				Console.WriteLine($"=== {te.EnvironmentRef} thread: {te.ThreadId} === ");
				Console.WriteLine(te.WithSafeInvoke(t => t.ToString()));
			}
			env.PendingException = default;
		}
		finally
		{
			CString message = new(() => "Thrown from C# code."u8);
			JThrowableObject.ThrowNew<JIllegalArgumentExceptionObject>(env, message);
		}
	}

	static JIntegerObject? IManagedCallback.SumArray(JClassObject jClass, JArrayObject<JInt>? jArray)
	{
		if (jArray is null) return default;
		try
		{
			IEnvironment env = jClass.Environment;
			JInt result = 0;
			using JPrimitiveMemory<JInt> intElements = jArray.GetElements();
			foreach (JInt element in intElements.Values)
				result += element;
			return JNumberObject<JInt, JIntegerObject>.Create(env, result);
		}
		catch (JniException)
		{
			return default;
		}
	}
	static JArrayObject<JArrayObject<JInt>>? IManagedCallback.GetIntArrayArray(JClassObject jClass, Int32 length)
	{
		try
		{
			IEnvironment env = jClass.Environment;
			JArrayObject<JArrayObject<JInt>> jArrayArray = JArrayObject<JArrayObject<JInt>>.Create(env, length);
			for (Int32 i = 0; i < length; i++)
			{
				using JArrayObject<JInt> jArray = JArrayObject<JInt>.Create(env, length);
				using (JPrimitiveMemory<JInt> intElements = jArray.GetCriticalElements(JMemoryReferenceKind.Local))
				{
					for (Int32 j = 0; j < length; j++)
						intElements.Values[j] = i + j;
				}
				jArrayArray[i] = jArray;
			}
			return jArrayArray;
		}
		catch (JniException)
		{
			return default;
		}
	}
	static void IManagedCallback.PrintClass(JClassObject jClass) => Console.WriteLine(jClass.ToString());
}