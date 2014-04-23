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
using UWA.Core.DataAccessLayer;

namespace UWA.AndroidClient
{
    [Application]
    public class UwaApplication : Application
    {
    	 
    	public UwaApplication(IntPtr handle, JniHandleOwnership transfer)
    	: base(handle, transfer)
    	{
    	}
    	 
    	public override void OnCreate()
    	{
            CopyDatabase("UwaDatabase.db3");
    	    base.OnCreate();
    	}

        private void CopyDatabase(string dataBaseName)
        {
            var dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dataBaseName);

            if (!System.IO.File.Exists(dbPath))
            {
                var dbAssetStream = Assets.Open(dataBaseName);
                var dbFileStream = new System.IO.FileStream(dbPath, System.IO.FileMode.OpenOrCreate);
                var buffer = new byte[1024];

                int b = buffer.Length;
                int length;

                while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                {
                    dbFileStream.Write(buffer, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
        }
    }
}