package org.m0skit0.android.hms.unity.helper.CameraHelper;


import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.os.Build;
import android.util.Log;

import org.m0skit0.android.hms.unity.BridgeType;
import org.m0skit0.android.hms.unity.activity.NativeBridgeActivity;

public class CameraBridge {

    private static final String TAG = CameraBridge.class.getSimpleName();
    private static final int CODE = BridgeType.CAMERA;
    private static UnityCameraCallback callback;
    public static final String CAMERA = "CAMERA";

    private static NativeBridgeActivity activity;
    private static Intent intent;


    public static void receiveShow(final Intent intent, UnityCameraCallback callback) {
        Log.d(TAG, "[HMS] receiveShow");

        CameraBridge.callback = callback;
        CameraBridge.intent = intent;

        NativeBridgeActivity.start(CAMERA);
    }


    public static void launchShow(final NativeBridgeActivity activity) {
        Log.d(TAG, "[HMS] launchShow");
        activity.startActivityForResult(intent, CODE);
    }


    public static void RequestForPermission(final NativeBridgeActivity activity) {

        boolean permissionAllFiles = activity.checkCallingOrSelfPermission("android.settings.MANAGE_APP_ALL_FILES_ACCESS_PERMISSION") == PackageManager.PERMISSION_GRANTED;

        if (!permissionAllFiles) {

            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {

                String[] permissions = {"android.settings.MANAGE_APP_ALL_FILES_ACCESS_PERMISSION"};

                activity.requestPermissions(
                        permissions,
                        BridgeType.FILE_PICKER);
            }
        }

        launchShow(activity);
    }

    public static void returnShow(Intent data, Bitmap myBitmap) {
        Log.d(TAG, "[HMS] returnShow");
        callback.onSuccess(data, myBitmap);
    }
}