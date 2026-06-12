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

	private Task _backgroundThread = Task.CompletedTask;
	private readonly JGlobal? _androidContext;
	private readonly JGlobal? _toastClass;
	private Int32 _count;
	private Boolean _disposed;

	public MainActivity()
	{
		Trace.WriteLine(
			$"Main classes: {String.Join('|', AndroidJniHost.MainClassesInformation.Select(i => i.ClassName))}");
		(this._androidContext, this._toastClass) = AndroidJniHost.CreateSyncContext().With(this).Invoke(jctx =>
		{
			using JClassObject androidToastClass = JClassObject.GetClass<AndroidToast>(jctx.Environment);
			return (jctx.Objects[0]!.Global, androidToastClass.Global);
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
		if (!this._backgroundThread.IsCompleted) return;
		this._backgroundThread = this.ToastBackground();
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
		this._androidContext?.Dispose();
	}
	private async Task ToastBackground()
	{
		Int32 i = 0;
		while (this._count - i > 0)
		{
			String textToToast = $"Random {i}: {Random.Shared.Next()}";
			await AndroidJniHost.CreateAsyncContext().Post(Application.SynchronizationContext,
			                                               (this._androidContext, textToToast),
			                                               MainActivity.ToastBackground);
			await Task.Delay(1000);
			i++;
		}
	}

	private static void ToastBackground(AndroidJniContext jctx, (JGlobal? globalContext, String text) args)
	{
		if (args.globalContext is null) return;
		try
		{
			using AndroidContext context = args.globalContext.AsLocal<AndroidContext>(jctx.Environment);
			using AndroidToast toast = // Invokes Toast.makeText
				AndroidToast.MakeText(context, args.text, AndroidToast.Length.Short);
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