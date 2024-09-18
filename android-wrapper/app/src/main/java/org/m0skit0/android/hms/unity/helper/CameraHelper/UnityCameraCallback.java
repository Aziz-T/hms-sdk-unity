package org.m0skit0.android.hms.unity.helper.CameraHelper;

import android.content.Intent;
import android.graphics.Bitmap;

public interface UnityCameraCallback {
    void onSuccess(Intent data, Bitmap myBitmap);
    void onFailure(Exception e);
}
