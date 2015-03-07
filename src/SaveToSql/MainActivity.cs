using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using Android.Util;
using Environment = Android.OS.Environment;
using Android.Content.PM;
using Android.Provider;
using System.Collections.Generic;
using Java.IO;
using Android.Graphics;
using SaveToSql.Data;



namespace SaveToSql
{
	public static class App{
		public static string _dbFileName;
		public static DateTime _date;
		public static string _desc;
		public static Bitmap bitmap;
		public static File _file;
		public static File _dir;     
		public static string _lat;
		public static string _long;
	}

	[Activity (Label = "SaveToSql", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges=Android.Content.PM.ConfigChanges.Orientation|Android.Content.PM.ConfigChanges.ScreenSize, ScreenOrientation=ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{

		string _AppFolder;
		internal static SQLite.SQLiteAsyncConnection DB;

		int count = 1;

		EditText textDate;
		EditText textDesc;
		ImageView ImageView1;
		EditText editLat;
		EditText editLong;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			if (IsThereAnAppToTakePictures ()) {

				ConnectToDB ();

				CreateDirectoryForPictures ();

				textDate = FindViewById<EditText> (Resource.Id.editDate);
				textDate.Text = string.Format ("{0:yyyy/MM/dd}", DateTime.Now);			

				textDesc = FindViewById<EditText> (Resource.Id.editDesc);

				Button btnCamera = FindViewById<Button> (Resource.Id.btnCamera);
				ImageView1 = FindViewById<ImageView> (Resource.Id.imageView1);
				if (App.bitmap != null) {
					ImageView1.SetImageBitmap (App.bitmap);
					App.bitmap = null;
				}
				btnCamera.Click += TakeAPicture;
			}

			editLat = FindViewById<EditText> (Resource.Id.editLat);
			editLong = FindViewById<EditText> (Resource.Id.editLong);

			Button btnSave = FindViewById<Button> (Resource.Id.btnSave);
			btnSave.Click += delegate {
				//click event here
				BPitImage img = new BPitImage {


				}
				Stock stock = new Stock { Symbol = "AAPL" };

				conn.InsertAsync (stock).ContinueWith (t => {
					Console.WriteLine ("New customer ID: {0}", stock.Id);
				});

			};
		}

		static void ConnectToDB ()
		{
			string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
			App._dbFileName = System.IO.Path.Combine (folder, "BPitImages.db");
			//var conn = new SQLiteConnection (System.IO.Path.Combine (folder, "BPitImages.db"));
			var conn = new SQLiteConnection (App._dbFileName);

			conn.CreateTable<BPitImage> ();
		}

		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void CreateDirectoryForPictures()
		{
			App._dir = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "SaveToSQL");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
			}
		}

		private void TakeAPicture(object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);

			App._file = new Java.IO.File(App._dir, String.Format("SaveToSQLPicture_{0}.jpg", Guid.NewGuid()));

			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));

			StartActivityForResult(intent, 0);
		}


		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// make it available in the gallery
			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);

			// display in ImageView. We will resize the bitmap to fit the display
			// Loading the full sized image will consume to much memory 
			// and cause the application to crash.
			int height = Resources.DisplayMetrics.HeightPixels;
			int width = ImageView1.Width ;
			App.bitmap = App._file.Path.LoadAndResizeBitmap (width, height);

			ImageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
			if (App.bitmap != null) {
				ImageView1.SetImageBitmap (App.bitmap);
				App.bitmap = null;
			}
		}

	}
}


