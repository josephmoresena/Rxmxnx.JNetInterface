using System.Diagnostics;

using AndroidInterface;

using Rxmxnx.JNetInterface.Functional;
using Rxmxnx.JNetInterface.Native;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public readonly struct ToastRequest : IAndroidNativeAction
{
	public JGlobal? AndroidContext { get; init; }
	public String? Text { get; init; }

	void IAndroidNativeAction.Accept(AndroidJniContext jctx)
	{
		if (this.AndroidContext is null || String.IsNullOrEmpty(this.Text)) return;
		try
		{
			using AndroidContext context = this.AndroidContext.AsLocal<AndroidContext>(jctx.Environment);
			using AndroidToast toast = // Invokes Toast.makeText
				AndroidToast.MakeText(context, this.Text, AndroidToast.Length.Short);
			toast.Show(); // Invokes Toast.show
		}
		catch (Exception e)
		{
			if (e is not ThrowableException throwableException)
			{
				Trace.WriteLine(e);
				return;
			}
			String throwableToString = throwableException.WithSafeInvoke(t => t.ToString());
			Trace.WriteLine(throwableToString);
			// Clear JNI exception.
			jctx.Environment.PendingException = default;
		}
	}
}