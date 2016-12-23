using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;
using Android.Content;
using System;
using Android.Views;

namespace App1
{
    [Activity(Label = "మల్లి గాడి కథలు", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class MainActivity : Activity
    {
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //PlaySound();

            SetContentView(Resource.Layout.Main);

            GridView gvStories = FindViewById<GridView>(Resource.Id.gvStories);
            gvStories.Adapter = new TextAdapter(this);

            gvStories.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
                //Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
                var activity2 = new Intent(this, typeof(Details));
                activity2.PutExtra("StoryId", args.Position.ToString());
                StartActivity(activity2);
            };

            var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar);
            editToolbar.Title = "Editing";
            editToolbar.InflateMenu(Resource.Menu.edit_menus);
            editToolbar.MenuItemClick += (sender, e) => {
                Toast.MakeText(this, "Bottom toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();
            };

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }

        
    }

    public class TextAdapter : BaseAdapter
    {
        Context context;

        public TextAdapter(Context c)
        {
            context = c;
        }

        public override int Count
        {
            get { return thumbIds.Length; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        // create a new ImageView for each item referenced by the Adapter
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TextView textView;

            if (convertView == null)
            {  // if it's not recycled, initialize some attributes
                textView = new TextView(context);
                textView.SetPadding(20, 20, 20, 20);
            }
            else
            {
                textView = (TextView)convertView;
            }

            textView.SetText(thumbIds[position].ToCharArray(), 0, thumbIds[position].Length);
            return textView;
        }

        // references to our images
        string[] thumbIds = {
        "Karna dialogue", "Sri Sri - 1", "Sri Sri - 2", "Sri Sri - 3", "Rohini story - 1", "Rohini story - 2"
        };
    }

}

