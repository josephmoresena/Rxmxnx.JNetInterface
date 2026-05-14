using System.Runtime.InteropServices;
using System.Web;

using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Swing;
#if RELEASE_PACKAGE
using Rxmxnx.PInvoke;
#endif

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal sealed class ShowDialogState(JFrameObjectAwt owner) : ActionListenerState, IDisposable
{
	private const String openHtml = "<html>";
	private const String closeHtml = "</html>";

	private static readonly String breakLineHtml = $"<br/>{Environment.NewLine}";

#if NET9_0_OR_GREATER
	private readonly Lock _lock = new();
#else
	private readonly Object _lock = new();
#endif
	private readonly JGlobalBase _owner = CallbackState.UseGlobal(owner.Global);
	public void Dispose()
	{
#if NET9_0_OR_GREATER
		using (this._lock.EnterScope())
#else
		lock (this._lock)
#endif
		{
			if (CallbackState.FreeGlobal(this._owner))
				this._owner.Dispose();
		}
	}

	public override void ActionPerformed(JActionEventObject actionEvent)
	{
		IEnvironment env = actionEvent.Environment;
#if NET9_0_OR_GREATER
		using (this._lock.EnterScope())
#else
		lock (this._lock)
#endif
		{
			try
			{
				using JFrameObjectAwt frame = this._owner.AsLocal<JFrameObjectAwt>(env);
				using JDialogObjectSwing dialog = JDialogObjectSwing.Create(frame, "System Info", true);
				using (JLabelObject jLabel = JLabelObject.Create(env, ShowDialogState.GetRuntimeInformation()))
				using (JStringObject jString = JStringObject.Create(env, "Center"u8))
					dialog.Add(jLabel, jString);

				dialog.Pack();
				dialog.SetRelativeTo(frame);
				dialog.SetVisible(true);
			}
			catch (Exception ex)
			{
				UIAdapter.Instance.ShowError(ex);
				if (ex is JniException) env.PendingException = default;
			}
		}
	}

	private static String GetRuntimeInformation()
		=> ShowDialogState.openHtml + $"Number of Cores: {Environment.ProcessorCount}" + ShowDialogState.breakLineHtml +
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
#if !RELEASE_PACKAGE
			ShowDialogState.breakLineHtml + $"Package: {HttpUtility.HtmlEncode(JObject.CompilationFramework)}" +
#else
			(!AotInfo.IsReflectionDisabled ?
				$"{ShowDialogState.breakLineHtml}Package: {HttpUtility.HtmlEncode(RuntimeInformation.FrameworkDescription)}" :
				String.Empty) +
#endif
			ShowDialogState.closeHtml;
}