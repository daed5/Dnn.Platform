﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information

namespace DotNetNuke.Services.FileSystem
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class FileLockedException : Exception
    {
        public FileLockedException()
        {
        }

        public FileLockedException(string message)
            : base(message)
        {
        }

        public FileLockedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public FileLockedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
