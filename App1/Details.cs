using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using Android.Net.Wifi;
using App1.Services;
using App1.Common;

namespace App1
{
    [Activity(Label = "Details")]
    public class Details : Activity
    {
        string[] tracksSrc = { "https://telugustories.blob.core.windows.net/audio/karna.mp3", "https://telugustories.blob.core.windows.net/audio/srisri1.mp3", "https://telugustories.blob.core.windows.net/audio/srisri2.mp3", "https://telugustories.blob.core.windows.net/audio/srisri3.mp3", "https://telugustories.blob.core.windows.net/audio/rohini1.mp3", "https://telugustories.blob.core.windows.net/audio/rohini2.mp3" };

        string[] thumbIds = { "Karna dialogue", "Sri Sri - 1", "Sri Sri - 2", "Sri Sri - 3", "Rohini story - 1", "Rohini story - 2" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Details);

            int storyId = int.Parse(Intent.GetStringExtra("StoryId"));

            TextView tvTitle = FindViewById<TextView>(Resource.Id.tvTitle);
            tvTitle.Text = thumbIds[storyId];

            AppPreferences pref = new AppPreferences(Application.Context);

            Button btnPlay = FindViewById<Button>(Resource.Id.btnPlay);
            Button btnPause = FindViewById<Button>(Resource.Id.btnPause);

            var currentStoryId = pref.getIntAccessKey("CurrentStoryId");
            var isPlaying = pref.getBoolAccessKey("IsPlaying");

            if (currentStoryId == storyId && isPlaying)
            {
                btnPlay.Visibility = ViewStates.Invisible;
                btnPause.Visibility = ViewStates.Visible;
            }
            else
            {
                btnPlay.Visibility = ViewStates.Visible;
                btnPause.Visibility = ViewStates.Invisible;
            }

            btnPlay.Click += delegate
            {
                SendAudioCommand(StreamingBackgroundService.ActionPlay, tracksSrc[storyId]);
                pref.saveInt("CurrentStoryId", storyId);
                pref.saveString("CurrentStory", tvTitle.Text);
                btnPause.Visibility = ViewStates.Visible;
                btnPlay.Visibility = ViewStates.Invisible;
            };

            btnPause.Click += delegate
            {
                SendAudioCommand(StreamingBackgroundService.ActionPause, string.Empty);
                btnPause.Visibility = ViewStates.Invisible;
                btnPlay.Visibility = ViewStates.Visible;
            };
        }

        private void SendAudioCommand(string action, string src)
        {
            var intent = new Intent(action);
            if (!string.IsNullOrEmpty(src))
            {
                intent.PutExtra("SourceId", src);
            }
            StartService(intent);
        }
    }
}