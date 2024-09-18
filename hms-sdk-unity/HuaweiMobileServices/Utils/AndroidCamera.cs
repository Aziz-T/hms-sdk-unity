using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HuaweiMobileServices.Utils
{
    public class AndroidCamera
    {
        private const string CAMERA_INTENT = "android.media.action.IMAGE_CAPTURE";
        private static readonly AndroidJavaClass sJavaClass = new AndroidJavaClass("org.m0skit0.android.hms.unity.helper.CameraHelper.CameraBridge");
        public static Action<AndroidIntent, AndroidBitmap> mOnSuccessListener;
        public static Action<HMSException> mOnFailureListener;


        public static void OpenCamera(AndroidJavaObject intent)
        {
            var callback = new CameraCallBackWrapper()
            .AddOnFailureListener(mOnFailureListener)
            .AddOnSuccessListener((data, bitmap) =>
            {
                HMSDispatcher.InvokeAsync(() => { mOnSuccessListener.Invoke(data, bitmap); });
            });
            sJavaClass.CallStatic("receiveShow", intent, callback);
        }


        public static void StartCamera(string type = null)
        {
            mOnSuccessListener += (data, bitmap) =>
            {
                Debug.Log("[HMS] Camera: SuccessPath: " + data.GetData()?.GetPath);
                Debug.Log("[HMS] Cemara: SuccessBitmap: " + bitmap == null ? "null" : "not null");
            };

            mOnFailureListener += (exception) =>
            {
                Debug.Log("[HMS] Camera: Failure: " + exception.Message);
            };

            AndroidIntent cameraIntent = new AndroidIntent(CAMERA_INTENT)
            .AddFlags(AndroidIntent.FLAG_GRANT_READ_URI_PERMISSION)
            .AddFlags(AndroidIntent.FLAG_GRANT_WRITE_URI_PERMISSION); 

            OpenCamera(cameraIntent.JavaObject);
        }

    }


    public class CameraCallBackWrapper : AndroidJavaProxy
    {
        private Action<AndroidIntent, AndroidBitmap> mOnSuccessListener;
        private Action<HMSException> mOnFailureListener;
        public CameraCallBackWrapper() : base("org.m0skit0.android.hms.unity.helper.CameraHelper.UnityCameraCallback") { }
        public CameraCallBackWrapper AddOnSuccessListener(Action<AndroidIntent, AndroidBitmap> onSuccessListener)
        {
            mOnSuccessListener = onSuccessListener;
            return this;
        }

        public CameraCallBackWrapper AddOnFailureListener(Action<HMSException> onFailureListener)
        {
            mOnFailureListener = onFailureListener;
            return this;
        }
        public void onSuccess(AndroidJavaObject data, AndroidJavaObject bitmap)
        {
            this.CallOnMainThread(() => { mOnSuccessListener?.Invoke(data.AsWrapper<AndroidIntent>(), bitmap.AsWrapper<AndroidBitmap>()); });
        }

        public void onFailure(AndroidJavaObject exception)
        {
            this.CallOnMainThread(() => { mOnFailureListener?.Invoke(exception.AsException()); });
        }
    }
}
