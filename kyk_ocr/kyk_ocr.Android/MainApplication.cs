﻿using Android.App;
using Android.Runtime;
using System;

namespace kyk_ocr.Droid
{
    public class MainApplication : Application
    {

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
               : base(javaReference, transfer) { }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}