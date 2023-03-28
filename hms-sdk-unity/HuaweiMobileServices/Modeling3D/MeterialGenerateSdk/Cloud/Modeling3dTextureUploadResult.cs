using System;
using HuaweiMobileServices.Utils;
using UnityEngine;

namespace  HuaweiMobileServices.Modeling3D.MeterialGenerateSdk.Cloud
{
        
    /// <summary>
    /// Wrapper for com.huawei.hms.materialgeneratesdk.cloud.Modeling3dTextureUploadResult.
    /// <see cref="Modeling3dTextureUploadResult" link=" https://developer.huawei.com/consumer/en/doc/development/graphics-References/modeling3dtextureuploadresult-0000001106613204"/>
    /// </summary>
    public class Modeling3dTextureUploadResult : JavaObjectWrapper
    {
        public Modeling3dTextureUploadResult(AndroidJavaObject javaObject) : base(javaObject) { }

        /// <summary>
        /// Obtains the ID of a material generation task.
        /// <param name="taskId">Task ID.</param>
        /// </summary>
        /// <returns>ID of a material generation task. A unique task ID is generated each time the material generation API is called.</returns>
        public string TaskId
        {
            get => CallAsString("getTaskId");
            set => Call("setTaskId", value.AsJavaString());
        }
        /// <summary>
        /// Obtains the image upload result.
        /// <param name="isComplate">Whether the task is complete.</param>
        /// </summary>
        /// <returns>
        /// Model Upload  result. true: The model is uploaded successfully. false: The model fails to be uploaded.
        /// </returns>
        public bool Complate
        {
            get => Call<bool>("isComplate");
            set => Call("setComplate", value);
        }
    }

}