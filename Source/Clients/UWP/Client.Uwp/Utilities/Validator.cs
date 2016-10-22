﻿using System;

namespace Andead.Chat.Client.Uwp.Utilities
{
    public static class Validator
    {
        public static void IsNotNull(this object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}