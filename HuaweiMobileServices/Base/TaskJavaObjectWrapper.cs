﻿namespace HuaweiMobileServices.Base
{
    using HuaweiMobileServices.Utils;
    using UnityEngine;

    internal class TaskJavaObjectWrapper<T> : TaskWrapper<T> where T : JavaObjectWrapper
    {
        public TaskJavaObjectWrapper(AndroidJavaObject javaObject) : base(javaObject, AndroidJavaObjectExtensions.AsWrapper<T>) { }
    }
}
