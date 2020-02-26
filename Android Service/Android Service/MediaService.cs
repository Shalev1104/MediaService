using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Android_Service
{
    [Service]
    public class MediaService : Service
    {

        private MediaPlayer player;

        // מנגינה מהאינטרנט
        private const string Mp3 = @"http://www.hochmuth.com/mp3/Vivaldi_Sonata_eminor_.mp3";

        public override IBinder OnBind(Intent intent)
        {
            return null;	// חובה להחזיר 
        }

        // Serviceהמטודה המבצעת את פעילות ה-
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent,[GeneratedEnum] StartCommandFlags flags,int startId)
        {
            base.OnStartCommand(intent, flags, startId);
            if (intent.Extras != null)
            {
                string action = intent.Extras.GetString("ACTION");

                if (action == "Pause")
                {
                    OnPause();
                }
            }
            else
            {
                Toast.MakeText(this, "Loading", ToastLength.Short).Show();

                // Thread הפעלת 
                Task.Run(() =>
                {
                    // טעינת הקובץ
                    try
                    {
                        player.SetDataSource(this, Android.Net.Uri.Parse(Mp3));
                        player.Prepare();
                    }
                    catch (IOException e)
                    {
                        Toast.MakeText(this, e.Message, ToastLength.Short).Show();
                    }
                    // הגדרה שהמנגינה תחזור על עצמה
                    player.Looping = true;

                    // הפעלת הנגן
                    player.Start();
                });
            }

            // ראה הסבר בהמשך
            return StartCommandResult.Sticky;
        }

        public void OnPause()
        {
            if (player != null)
            {
                if (player.IsPlaying)
                    player.Pause();
                else
                    if (player.CurrentPosition > 1)
                    player.Start();
            }
        }

        public override void OnDestroy()
        {
            player.Stop();
        }
    }

}