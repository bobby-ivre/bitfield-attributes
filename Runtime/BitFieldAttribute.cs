// ----------------------------------------------------------------------------------------------------------------------------------------
// Creation Date:   15/01/21
// Author:              bgreaney
// ----------------------------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace LughNut.BitPacking
{

    /// <summary>
    /// Attribute for bit packing. Add to a byte, sbyte, short, ushort, int or 
    /// uint to enable the inspector. Takes either an enum (recommended)
    /// or a string[] to add as labels.
    /// </summary>
    public class BitFieldAttribute : PropertyAttribute
    {
        public string[] labels = new string[32];
        public int totalBits;

        public BitFieldAttribute(string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
            {
                labels[i] = fieldNames[i];
            }
        }
        public BitFieldAttribute(Type enumType)
        {
            string[] enumNames = Enum.GetNames(enumType);
            for (int i = 0; i < enumNames.Length; i++)
            {
                labels[i] = enumNames[i];
            }
        }
    }

    /// <summary>
    /// Extends the base byte, sbyte, ushort, short, int and uint classes
    /// to be able to reference bits directly.
    /// </summary>
    public static class BitFieldExtensions
    {
        //Gets a binary bit by index as a true or false
        #region GetBit
        public static bool GetBit(this byte b, int index)
        {
            int val = 1 << index;
            int tempInt = b;
            return ((tempInt & val) >> index) == 1 ? true : false;
        }

        public static bool GetBit(this sbyte b, int index)
        {
            int val = 1 << index;
            int tempInt = b;
            return ((tempInt & val) >> index) == 1 ? true : false;
        }

        public static bool GetBit(this ushort b, int index)
        {
            int val = 1 << index;
            int tempInt = b;
            return ((tempInt & val) >> index) == 1 ? true : false;
        }

        public static bool GetBit(this short b, int index)
        {
            int val = 1 << index;
            int tempInt = b;
            return ((tempInt & val) >> index) == 1 ? true : false;
        }

        public static bool GetBit(this uint b, int index)
        {
            int val = 1 << index;
            uint tempInt = b;
            return ((tempInt & val) >> index) == 1 ? true : false;
        }

        public static bool GetBit(this int b, int index)
        {
            int val = 1 << index;
            int tempInt = b;
            return ((tempInt & val) >> index) == 1 ? true : false;
        }

        public static bool GetBit(this long b, int index)
        {
            int val = 1 << index;
            long tempInt = b;
            return ((tempInt & val) >> index) == 1 ? true : false;
        }

        #endregion

        //Sets a binary bit to 1 or 0
        #region SetBit
        public static void SetBit(this ref byte b, int index, bool value)
        {
            if (b.GetBit(index) != value)
                b.ToggleBit(index);
        }

        public static void SetBit(this ref sbyte b, int index, bool value)
        {
            if (b.GetBit(index) != value)
                b.ToggleBit(index);
        }

        public static void SetBit(this ref ushort b, int index, bool value)
        {
            if (b.GetBit(index) != value)
                b.ToggleBit(index);
        }

        public static void SetBit(this ref short b, int index, bool value)
        {
            if (b.GetBit(index) != value)
                b.ToggleBit(index);
        }

        public static void SetBit(this ref uint b, int index, bool value)
        {
            if (b.GetBit(index) != value)
                b.ToggleBit(index);
        }

        public static void SetBit(this ref int b, int index, bool value)
        {
            if (b.GetBit(index) != value)
                b.ToggleBit(index);
        }

        public static void SetBit(this ref long b, int index, bool value)
        {
            if (b.GetBit(index) != value)
                b.ToggleBit(index);
        }

        #endregion

        //Toggles a bit to its opposite state
        #region ToggleBit
        public static void ToggleBit(this ref byte b, int index)
        {
            int changedflag = 1 << index;
            b ^= (byte)changedflag;
        }

        public static void ToggleBit(this ref sbyte b, int index)
        {
            int changedflag = 1 << index;
            b ^= (sbyte)changedflag;
        }

        public static void ToggleBit(this ref ushort b, int index)
        {
            int changedflag = 1 << index;
            b ^= (ushort)changedflag;
        }

        public static void ToggleBit(this ref short b, int index)
        {
            int changedflag = 1 << index;
            b ^= (short)changedflag;
        }

        public static void ToggleBit(this ref uint b, int index)
        {
            int changedflag = 1 << index;
            b ^= (uint)changedflag;
        }

        public static void ToggleBit(this ref int b, int index)
        {
            int changedflag = 1 << index;
            b ^= changedflag;
        }

        public static void ToggleBit(this ref long b, int index)
        {
            int changedflag = 1 << index;
            b ^= changedflag;
        }

        #endregion

        //Returns a number in binary format, e.g. 5 = 00000101
        #region ToStringBinary
        public static string ToStringBinary(this ref byte b)
        {
            string output = "";
            for (int i = 7; i >= 0; i--)
            {
                output += b.GetBit(i) ? "1" : "0";
            }
            return output;
        }

        public static string ToStringBinary(this ref sbyte b)
        {
            string output = "";
            for (int i = 7; i >= 0; i--)
            {
                output += b.GetBit(i) ? "1" : "0";
            }
            return output;
        }
        public static string ToStringBinary(this ref ushort b)
        {
            string output = "";
            for (int i = 15; i >= 0; i--)
            {
                output += b.GetBit(i) ? "1" : "0";
            }
            return output;
        }
        public static string ToStringBinary(this ref short b)
        {
            string output = "";
            for (int i = 15; i >= 0; i--)
            {
                output += b.GetBit(i) ? "1" : "0";
            }
            return output;
        }

        public static string ToStringBinary(this ref uint b)
        {
            string output = "";
            for (int i = 31; i >= 0; i--)
            {
                output += b.GetBit(i) ? "1" : "0";
            }
            return output;
        }

        public static string ToStringBinary(this ref int b)
        {
            string output = "";
            for (int i = 31; i >= 0; i--)
            {
                output += b.GetBit(i) ? "1" : "0";
            }
            return output;
        }

        public static string ToStringBinary(this ref long b)
        {
            string output = "";
            for (int i = 64; i >= 0; i--)
            {
                output += b.GetBit(i) ? "1" : "0";
            }
            return output;
        }

        #endregion
    }
}