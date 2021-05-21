//-----------------------------------------------------------------------
// <copyright file="UnsafeUtilities.cs" company="Sirenix IVS">
// Copyright (c) Sirenix IVS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Sirenix.Utilities.Unsafe
{
#pragma warning disable

    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Not yet documented.
    /// </summary>
    public static class UnsafeUtilities
    {
        /// <summary>
        /// Not yet documented.
        /// </summary>
        public static unsafe T[] StructArrayFromBytes<T>(byte[] bytes, int byteLength) where T : struct
        {
            return StructArrayFromBytes<T>(bytes, 0, 0);
        }

        /// <summary>
        /// Not yet documented.
        /// </summary>
        public static unsafe T[] StructArrayFromBytes<T>(byte[] bytes, int byteLength, int byteOffset) where T : struct
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            if (byteLength <= 0)
            {
                throw new ArgumentException("Byte length must be larger than zero.");
            }

            if (byteOffset < 0)
            {
                throw new ArgumentException("Byte offset must be larger than or equal to zero.");
            }

            int typeSize = Marshal.SizeOf(typeof(T));

            if (byteOffset % sizeof(ulong) != 0)
            {
                throw new ArgumentException("Byte offset must be divisible by " + sizeof(ulong) + " (IE, sizeof(ulong))");
            }

            if (byteLength + byteOffset >= bytes.Length)
            {
                throw new ArgumentException("Given byte array of size " + bytes.Length + " is not large enough to copy requested number of bytes " + byteLength + ".");
            }

            if ((byteLength - byteOffset) % typeSize != 0)
            {
                throw new ArgumentException("The length in the given byte array (" + bytes.Length + ", and " + (bytes.Length - byteOffset) + " minus byteOffset " + byteOffset + ") to convert to type " + typeof(T).Name + " is not divisible by the size of " + typeof(T).Name + " (" + typeSize + ").");
            }

            int elementCount = (bytes.Length - byteOffset) / typeSize;
            T[] array = new T[elementCount];
            MemoryCopy(bytes, array, byteLength, byteOffset, 0);
            return array;
        }

        /// <summary>
        /// Not yet documented.
        /// </summary>
        public static unsafe byte[] StructArrayToBytes<T>(T[] array) where T : struct
        {
            byte[] bytes = null;
            return StructArrayToBytes(array, ref bytes, 0);
        }

        /// <summary>
        /// Not yet documented.
        /// </summary>
        public static unsafe byte[] StructArrayToBytes<T>(T[] array, ref byte[] bytes, int byteOffset) where T : struct
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (byteOffset < 0)
            {
                throw new ArgumentException("Byte offset must be larger than or equal to zero.");
            }

            int typeSize = Marshal.SizeOf(typeof(T));
            int byteCount = typeSize * array.Length;

            if (bytes == null)
            {
                bytes = new byte[byteCount + byteOffset];
            }
            else if (bytes.Length + byteOffset > byteCount)
            {
                throw new ArgumentException("Byte array must be at least " + (bytes.Length + byteOffset) + " long with the given byteOffset.");
            }

            MemoryCopy(array, bytes, byteCount, 0, byteOffset);
            return bytes;
        }

        /// <summary>
        /// Not yet documented.
        /// </summary>
        public static unsafe string StringFromBytes(byte[] buffer, int charLength, bool needs16BitSupport)
        {
            int byteCount = needs16BitSupport ? charLength * 2 : charLength;

            if (buffer.Length < byteCount)
            {
                throw new ArgumentException("Buffer is not large enough to contain the given string; a size of at least " + byteCount + " is required.");
            }

            GCHandle toHandle = default(GCHandle);
            string result = new string(default(char), charLength); // Creaty empty string of required length

            try
            {
                toHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                if (needs16BitSupport)
                {
                    if (BitConverter.IsLittleEndian)
                    {
                        fixed (char* charPtr1 = result)
                        {
                            ushort* fromPtr1 = (ushort*)toHandle.AddrOfPinnedObject().ToPointer();
                            ushort* toPtr1 = (ushort*)charPtr1;

                            for (int i = 0; i < byteCount; i += sizeof(ushort))
                            {
                                *toPtr1++ = *fromPtr1++;
                            }
                        }
                    }
                    else
                    {
                        fixed (char* charPtr2 = result)
                        {
                            byte* fromPtr2 = (byte*)toHandle.AddrOfPinnedObject().ToPointer();
                            byte* toPtr2 = (byte*)charPtr2;

                            for (int i = 0; i < byteCount; i += sizeof(ushort))
                            {
                                *toPtr2 = *(fromPtr2 + 1);
                                *(toPtr2 + 1) = *fromPtr2;

                                fromPtr2 += 2;
                                toPtr2 += 2;
                            }
                        }
                    }
                }
                else
                {
                    if (BitConverter.IsLittleEndian)
                    {
                        fixed (char* charPtr3 = result)
                        {
                            byte* fromPtr3 = (byte*)toHandle.AddrOfPinnedObject().ToPointer();
                            byte* toPtr3 = (byte*)charPtr3;

                            for (int i = 0; i < byteCount; i += sizeof(byte))
                            {
                                *toPtr3++ = *fromPtr3++;
                                toPtr3++; // Skip every other string byte
                            }
                        }
                    }
                    else
                    {
                        fixed (char* charPtr4 = result)
                        {
                            byte* fromPtr4 = (byte*)toHandle.AddrOfPinnedObject().ToPointer();
                            byte* toPtr4 = (byte*)charPtr4;

                            for (int i = 0; i < byteCount; i += sizeof(byte))
                            {
                                toPtr4++; // Skip every other string byte
                                *toPtr4++ = *fromPtr4++;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (toHandle.IsAllocated)
                {
                    toHandle.Free();
                }
            }

            // Retrieve proper string reference from the intern pool.
            // This code removed for now, as the slight decrease in memory use is not considered worth the performance cost of the intern lookup and the potential extra garbage to be collected.
            // Might eventually become a global config option, if this is considered necessary.
            //result = string.Intern(result);

            return result;
        }

        /// <summary>
        /// Not yet documented.
        /// </summary>
        public static unsafe int StringToBytes(byte[] buffer, string value, bool needs16BitSupport)
        {
            int byteCount = needs16BitSupport ? value.Length * 2 : value.Length;

            if (buffer.Length < byteCount)
            {
                throw new ArgumentException("Buffer is not large enough to contain the given string; a size of at least " + byteCount + " is required.");
            }

            GCHandle toHandle = default(GCHandle);

            try
            {
                toHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                if (needs16BitSupport)
                {
                    if (BitConverter.IsLittleEndian)
                    {
                        fixed (char* charPtr1 = value)
                        {
                            ushort* fromPtr1 = (ushort*)charPtr1;
                            ushort* toPtr1 = (ushort*)toHandle.AddrOfPinnedObject().ToPointer();

                            for (int i = 0; i < byteCount; i += sizeof(ushort))
                            {
                                *toPtr1++ = *fromPtr1++;
                            }
                        }
                    }
                    else
                    {
                        fixed (char* charPtr2 = value)
                        {
                            byte* fromPtr2 = (byte*)charPtr2;
                            byte* toPtr2 = (byte*)toHandle.AddrOfPinnedObject().ToPointer();

                            for (int i = 0; i < byteCount; i += sizeof(ushort))
                            {
                                *toPtr2 = *(fromPtr2 + 1);
                                *(toPtr2 + 1) = *fromPtr2;

                                fromPtr2 += 2;
                                toPtr2 += 2;
                            }
                        }
                    }
                }
                else
                {
                    if (BitConverter.IsLittleEndian)
                    {
                        fixed (char* charPtr3 = value)
                        {
                            byte* fromPtr3 = (byte*)charPtr3;
                            byte* toPtr3 = (byte*)toHandle.AddrOfPinnedObject().ToPointer();

                            for (int i = 0; i < byteCount; i += sizeof(byte))
                            {
                                fromPtr3++; // Skip every other string byte
                                *toPtr3++ = *fromPtr3++;
                            }
                        }
                    }
                    else
                    {
                        fixed (char* charPtr4 = value)
                        {
                            byte* fromPtr4 = (byte*)charPtr4;
                            byte* toPtr4 = (byte*)toHandle.AddrOfPinnedObject().ToPointer();

                            for (int i = 0; i < byteCount; i += sizeof(byte))
                            {
                                *toPtr4++ = *fromPtr4++;
                                fromPtr4++; // Skip every other string byte
                            }
                        }
                    }
                }
            }
            finally
            {
                if (toHandle.IsAllocated)
                {
                    toHandle.Free();
                }
            }

            return byteCount;
        }

        /// <summary>
        /// Not yet documented.
        /// </summary>
        public static unsafe void MemoryCopy(object from, object to, int byteCount, int fromByteOffset, int toByteOffset)
        {
            GCHandle fromHandle = default(GCHandle);
            GCHandle toHandle = default(GCHandle);

            if (fromByteOffset % sizeof(ulong) != 0 || toByteOffset % sizeof(ulong) != 0)
            {
                throw new ArgumentException("Byte offset must be divisible by " + sizeof(ulong) + " (IE, sizeof(ulong))");
            }

            try
            {
                int restBytes = byteCount % sizeof(ulong);
                int ulongCount = (byteCount - restBytes) / sizeof(ulong);
                int fromOffsetCount = fromByteOffset / sizeof(ulong);
                int toOffsetCount = toByteOffset / sizeof(ulong);

                fromHandle = GCHandle.Alloc(from, GCHandleType.Pinned);
                toHandle = GCHandle.Alloc(to, GCHandleType.Pinned);

                ulong* fromUlongPtr = (ulong*)fromHandle.AddrOfPinnedObject().ToPointer();
                ulong* toUlongPtr = (ulong*)toHandle.AddrOfPinnedObject().ToPointer();

                if (fromOffsetCount > 0)
                {
                    fromUlongPtr += fromOffsetCount;
                }

                if (toOffsetCount > 0)
                {
                    toUlongPtr += toOffsetCount;
                }

                for (int i = 0; i < ulongCount; i++)
                {
                    *toUlongPtr++ = *fromUlongPtr++;
                }

                if (restBytes > 0)
                {
                    byte* fromBytePtr = (byte*)fromUlongPtr;
                    byte* toBytePtr = (byte*)toUlongPtr;

                    for (int i = 0; i < restBytes; i++)
                    {
                        *toBytePtr++ = *fromBytePtr++;
                    }
                }
            }
            finally
            {
                if (fromHandle.IsAllocated)
                {
                    fromHandle.Free();
                }

                if (toHandle.IsAllocated)
                {
                    toHandle.Free();
                }
            }
        }
    }
}