using _Microsoft.Android.Resource.Designer;

using Android;
using Android.Content.PM;
using Android.OS;
using Android.Views;

using HelloJniLib;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;

using Activity = Android.App.Activity;
using Environment = System.Environment;
using Trace = System.Diagnostics.Trace;

namespace Rxmxnx.JNetInterface.ApplicationTest;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity, View.IOnClickListener
{
	private static readonly DateTime load = DateTime.Now;
	private readonly JGlobal? _looperClass;
	private readonly JGlobal? _toastClass;

	private Task _backgroundThread = Task.CompletedTask;
	private Int32 _count;
	private Boolean _disposed;

	public MainActivity()
	{
		Trace.WriteLine(
			$"Main classes: {String.Join('|', AndroidJniHost.MainClassesInformation.Select(i => i.ClassName))}");
		(this._looperClass, this._toastClass) = AndroidJniHost.CreateSyncContext().Invoke(jctx =>
		{
			using JClassObject
				androidLooperClass = JClassObject.GetClass<AndroidLooper>(jctx.Environment); // Loads android.os.Looper
			using JClassObject androidToastClass = JClassObject.GetClass<AndroidToast>(jctx.Environment);
			return (androidLooperClass.Global, androidToastClass.Global);
		});
	}

	public void OnClick(View? v)
	{
		if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
		{
#pragma warning disable CA1416
			if (this.CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
				this.RequestPermissions([Manifest.Permission.PostNotifications,], 101);
#pragma warning restore CA1416
		}
		if (v is not TextView text) return;
		this._count++;
#if !RELEASE_PACKAGE
		text.Text = $"{Environment.NewLine}Package: {JObject.CompilationFramework}" +
			ExportedMethods.GetRuntimeInformation(DateTime.Now, MainActivity.load, this._count);
#else
		text.Text = ExportedMethods.GetRuntimeInformation(DateTime.Now, MainActivity.load, this._count);
#endif
		if (this._backgroundThread.IsCompleted)
			this._backgroundThread = AndroidJniHost.CreateAsyncContext(new(() => "ToastBackground"u8)).With(this)
			                                       .InvokeAsync(this._count, MainActivity.ToastBackground);
	}

	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);

		// Set our view from the "main" layout resource
		this.SetContentView(ResourceConstant.Layout.activity_main);

		TextView? text = this.FindViewById<TextView>(ResourceConstant.Id.textView1);
		text?.SetOnClickListener(this);
	}
	protected override void Dispose(Boolean disposing)
	{
		this._backgroundThread.Wait();
		this.ReleaseUnmanagedResources();
		base.Dispose(disposing);
	}

	~MainActivity() { this.ReleaseUnmanagedResources(); }

	private void ReleaseUnmanagedResources()
	{
		if (this._disposed) return;
		this._disposed = true;
		this._toastClass?.Dispose(); // Removes global class instance.
		this._looperClass?.Dispose(); // Removes global class instance.
	}

	private static void ToastBackground(AndroidJniContext jctx, Int32 count)
	{
		Trace.WriteLine($"Enabled logs: {AndroidJniHost.TraceEnabled}");
		if (jctx.Objects[0]?.CastTo<AndroidContext>(true) is not { } context) return;
		try
		{
			Int32 i = 0;
			AndroidLooper.Prepare(jctx.Environment); // Prepares current thread to show Toast.
			while (count - i > 0)
			{
				using AndroidToast toast = // Invokes Toast.makeText
					AndroidToast.MakeText(context, $"Random {i}: {Random.Shared.Next()}", AndroidToast.Length.Short);
				toast.Show(); // Invokes Toast.show
				Thread.Sleep(1000);
				i++;
			}
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