using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Swing;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public sealed class ShowDialogState(JWindowObject window) : JNativeCallback.ActionListenerState
{
	private readonly JWeak _frame = window.Weak;

	public override void ActionPerformed(JActionEventObject actionEvent)
	{
		IEnvironment env = actionEvent.Environment;
		using JWindowObject window = this._frame.AsLocal<JWindowObject>(env);
		using JDialogObjectSwing dialog = JDialogObjectSwing.Create(window, "System Info");

		using (JLabelObject jLabel = JLabelObject.Create(env, ShowDialogState.GetRuntimeInformation()))
		using (JStringObject jString = JStringObject.Create(env, "Center"u8))
			dialog.Add(jLabel, jString);

		dialog.SetVisible(true);
	}

	private static String GetRuntimeInformation()
		=> $"Number of Cores: {Environment.ProcessorCount}" + Environment.NewLine +
			$"Little-Endian: {BitConverter.IsLittleEndian}" + Environment.NewLine +
			$"OS: {RuntimeInformation.OSDescription}" + Environment.NewLine +
			$"OS Arch: {RuntimeInformation.OSArchitecture}" + Environment.NewLine +
			$"OS Version: {Environment.OSVersion}" + Environment.NewLine + $"Computer: {Environment.MachineName}" +
			Environment.NewLine + $"User: {Environment.UserName}" + Environment.NewLine +
			$"System Path: {Environment.SystemDirectory}" + Environment.NewLine +
			$"Current Path: {Environment.CurrentDirectory}" + Environment.NewLine +
			$"Process Arch: {RuntimeInformation.ProcessArchitecture}" + Environment.NewLine +
			$"Framework Version: {Environment.Version}" + Environment.NewLine +
			$"Runtime Name: {RuntimeInformation.FrameworkDescription}" + Environment.NewLine +
			$"Runtime Path: {RuntimeEnvironment.GetRuntimeDirectory()}" + Environment.NewLine +
			$"Runtime Version: {RuntimeEnvironment.GetSystemVersion()}";
}