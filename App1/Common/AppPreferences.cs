using Android.Content;
using Android.Preferences;
using System;

namespace App1.Common
{
    public class AppPreferences
    {
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

        private static string PREFERENCE_ACCESS_KEY = "MATPrefT1$";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }

        public void saveString(string key, string value)
        {
            mPrefsEditor.PutString(key, value);
            mPrefsEditor.Apply();
        }

        public void saveInt(string key, int value)
        {
            mPrefsEditor.PutInt(key, value);
            mPrefsEditor.Apply();
        }

        public void saveBool(string key, bool value)
        {
            mPrefsEditor.PutBoolean(key, value);
            mPrefsEditor.Apply();
        }

        public string getStringAccessKey(string key)
        {
            return mSharedPrefs.GetString(key, "");
        }

        public int getIntAccessKey(string key)
        {
            return mSharedPrefs.GetInt(key, 0);
        }

        public bool getBoolAccessKey(string key)
        {
            return mSharedPrefs.GetBoolean(key, false);
        }
    }
}