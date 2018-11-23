using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidFunction : MonoBehaviour
{

    public void ShowToastMessage(string message)
    {
        //Grab Unity'class
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //Grab the activity from the class via static function
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        //Call android system popup on ui thread
        //Pass in callback to activity
        unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
         {
             Debug.Log("Running on UI thread");
             //Get application context for activity


             //Instanciate a Toast objec using Addroid's api
             AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
             //Convert our message string as java string
             AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);
             //careate a toast object instance using the makeText static function from androdi.widget.Toast
             AndroidJavaObject toastInstance = toastClass.CallStatic<AndroidJavaObject>(
                  "makeText",
                  applicationContext,
                  javaString,
                  1);
             //show the toast
             toastInstance.Call("show");
         }));

       
    }

    public void ShowNotification(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //Grab the activity from the class via static function
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        AndroidJavaClass utilityClass = new AndroidJavaClass("com.allen.tools.Utility");
        AndroidJavaObject utilityInstance = utilityClass.CallStatic<AndroidJavaObject>("Create", unityActivity, applicationContext);
        utilityInstance.Call("ShowNotification", "Noti10", 0);
    }
    public void ShowDelayedNotification(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //Grab the activity from the class via static function
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        AndroidJavaClass utilityClass = new AndroidJavaClass("com.allen.tools.Utility");
        AndroidJavaObject utilityInstance = utilityClass.CallStatic<AndroidJavaObject>("Create", unityActivity, applicationContext);
        utilityInstance.Call("ShowNotification", "Noti5", 5000);
    }
    public void ShowStaticHelloWorldLog()
    {
        //get java class from my plugin
        AndroidJavaClass androidLibratyUtility = new AndroidJavaClass("com.neil.androidunityt.UtilityBridgeMain");
        //call static function
        androidLibratyUtility.CallStatic("HelloWorld");
    }
}
