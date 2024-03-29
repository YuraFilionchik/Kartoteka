﻿/* The MIT License (MIT)
*
* Copyright (c) 2014 FriendlyX
* Permission is hereby granted, free of charge, to any person obtaining a copy of
* this software and associated documentation files (the "Software"), to deal in
* the Software without restriction, including without limitation the rights to
* use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
* the Software, and to permit persons to whom the Software is furnished to do so,
* subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
* FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
* COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
* IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
* CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;

namespace FX.Configuration.Attributes
{
    /// <summary>
    /// Defines what type of deserializer to use
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DeserializerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeserializerAttribute"/> class.
        /// </summary>
        /// <param name="deserializerType">Type of the deserializer.</param>
        public DeserializerAttribute(Type deserializerType)
        {
            this.DeserializerType = deserializerType;
        }

        /// <summary>
        /// Gets the type of the deserializer
        /// </summary>
        public Type DeserializerType { get; private set; }

        /// <summary>
        /// Creates the setting deserializer
        /// </summary>
        /// <returns>A deserializer</returns>
        public virtual object CreateSettingDeserializer()
        {
            return Activator.CreateInstance(this.DeserializerType);
        }
    }
}