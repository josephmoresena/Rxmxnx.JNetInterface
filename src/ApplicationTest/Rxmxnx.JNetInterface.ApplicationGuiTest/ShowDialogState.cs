using System.Runtime.InteropServices;
using System.Web;

using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Swing;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal sealed class ShowDialogState(JWindowObject owner) : JNativeCallback.ActionListenerState, IDisposable
{
	private const String OpenHtml = "<html>";
	private const String CloseHtml = "</html>";

	private static readonly String breakLineHtml = "<br/>" + Environment.NewLine;

	private readonly Object _lock = new();
	private readonly JWeak _owner = owner.Weak;
	public void Dispose()
	{
		lock (this._lock)
			this._owner.Dispose();
	}

	public override void ActionPerformed(JActionEventObject actionEvent)
	{
		lock (this._lock)
		{
			IEnvironment env = actionEvent.Environment;
			using JWindowObject window = this._owner.AsLocal<JWindowObject>(env);
			using JDialogObjectSwing dialog = JDialogObjectSwing.Create(window, "System Info");

			using (JLabelObject jLabel = JLabelObject.Create(env, ShowDialogState.GetRuntimeInformation()))
			using (JStringObject jString = JStringObject.Create(env, "Center"u8))
				dialog.Add(jLabel, jString);

			dialog.Pack();
			dialog.SetRelativeTo(window);
			dialog.SetVisible(true);
		}
	}

	private static String GetRuntimeInformation()
		=> ShowDialogState.OpenHtml + $"Number of Cores: {Environment.ProcessorCount}" + ShowDialogState.breakLineHtml +
			$"Little-Endian: {BitConverter.IsLittleEndian}" + ShowDialogState.breakLineHtml +
			$"OS: {RuntimeInformation.OSDescription}" + ShowDialogState.breakLineHtml +
			$"OS Arch: {RuntimeInformation.OSArchitecture}" + ShowDialogState.breakLineHtml +
			$"OS Version: {HttpUtility.HtmlEncode(Environment.OSVersion)}" + ShowDialogState.breakLineHtml +
			$"Computer: {HttpUtility.HtmlEncode(Environment.MachineName)}" + ShowDialogState.breakLineHtml +
			$"User: {HttpUtility.HtmlEncode(Environment.UserName)}" + ShowDialogState.breakLineHtml +
			$"System Path: {HttpUtility.HtmlEncode(Environment.SystemDirectory)}" + ShowDialogState.breakLineHtml +
			$"Current Path: {HttpUtility.HtmlEncode(Environment.CurrentDirectory)}" + ShowDialogState.breakLineHtml +
			$"Process Arch: {RuntimeInformation.ProcessArchitecture}" + ShowDialogState.breakLineHtml +
			$"Process ID: {Environment.CurrentManagedThreadId}" + ShowDialogState.breakLineHtml +
			$"Framework Version: {HttpUtility.HtmlEncode(Environment.Version)}" + ShowDialogState.breakLineHtml +
			$"Runtime Name: {RuntimeInformation.FrameworkDescription}" + ShowDialogState.breakLineHtml +
			$"Runtime Path: {HttpUtility.HtmlEncode(RuntimeEnvironment.GetRuntimeDirectory())}" +
			ShowDialogState.breakLineHtml +
			$"Runtime Version: {HttpUtility.HtmlEncode(RuntimeEnvironment.GetSystemVersion())}" +
			ShowDialogState.CloseHtml;
}