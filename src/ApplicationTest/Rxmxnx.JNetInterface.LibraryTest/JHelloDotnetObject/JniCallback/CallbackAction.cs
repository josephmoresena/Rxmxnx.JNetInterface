using Rxmxnx.JNetInterface.Functional;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.ApplicationTest;

// ReSharper disable once ClassCannotBeInstantiated
public partial class JHelloDotnetObject
{
	private sealed partial class JniCallback : IFrameAction
	{
		[ThreadStatic]
		private static VoidOperation operation;

		void IFrameAction.Accept(IEnvironment env, scoped ReadOnlySpan<JReferenceObject?> objects)
		{
			if (objects.IsEmpty || objects[0] is not JLocalObject jLocal) return;
			// ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
			switch (JniCallback.operation)
			{
				case VoidOperation.PrintRuntimeInformation:
					managed.PrintRuntimeInformation(jLocal, objects.Length > 1 ? objects[1] as JStringObject : null);
					break;
				case VoidOperation.FieldAccess:
					managed.ProcessField(jLocal);
					break;
				case VoidOperation.Throw:
					managed.Throw(jLocal);
					break;
			}
		}

		private void PrintRuntimeInformation(JEnvironmentRef envRef, JObjectLocalRef localRef,
			JStringLocalRef stringRef)
		{
			try
			{
				JniCallback.operation = VoidOperation.PrintRuntimeInformation;
#if !NET9_0_OR_GREATER
				managed.VirtualMachine.WithNativeFrame(envRef, this, [localRef, stringRef,]);
#else
				managed.VirtualMachine.WithNativeFrame(envRef, this, localRef, stringRef);
#endif
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.PrintRuntimeInformation ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
			finally
			{
				JniCallback.operation = VoidOperation.None;
			}
		}
		private void FieldAccess(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			try
			{
				JniCallback.operation = VoidOperation.FieldAccess;
#if !NET9_0_OR_GREATER
				managed.VirtualMachine.WithNativeFrame(envRef, this, [localRef,]);
#else
				managed.VirtualMachine.WithNativeFrame(envRef, this, localRef);
#endif
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.FieldAccess ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
			finally
			{
				JniCallback.operation = VoidOperation.None;
			}
		}
		private void Throw(JEnvironmentRef envRef, JObjectLocalRef localRef)
		{
			try
			{
				JniCallback.operation = VoidOperation.Throw;
#if !NET9_0_OR_GREATER
				managed.VirtualMachine.WithNativeFrame(envRef, this, [localRef,]);
#else
				managed.VirtualMachine.WithNativeFrame(envRef, this, localRef);
#endif
			}
			catch (Exception ex)
			{
				managed.Writer.WriteLine(
					$"JHelloDotnetObject.JniCallback.FieldAccess ex: {(AotInfo.IsReflectionDisabled ? ex.Message : ex.ToString())}");
				throw;
			}
			finally
			{
				JniCallback.operation = VoidOperation.None;
			}
		}

		private enum VoidOperation : Byte
		{
			None = 0x0,
			PrintRuntimeInformation = 0x1,
			FieldAccess = 0x2,
			Throw = 0x3,
		}
	}
}