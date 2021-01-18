// ------------------------------------------------------------------------------------
// Creation Date: 15/01/21
// Author: bgreaney
// Description: Attribute for bit packing. Add to a byte, sbyte, short,
// ushort, int or uint to enable the inspector. Takes either an enum
// (recommended) or a string[] to add as labels.
// ------------------------------------------------------------------------------------
#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace LughNut.BitPacking
{
    /// <summary>
    /// Property drawer for BitField attributes. Shows the labels based on
    /// the enum/string[] supplied to the attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(BitFieldAttribute))]
    public class BitfieldDrawer : PropertyDrawer
    {
        BitFieldAttribute bitfieldAttribute { get { return (BitFieldAttribute)attribute; } }

        //The FieldType enum doubles as a container length as well as a way to avoid
        //using reflection excessively.
        private enum FieldType
        {
            Byte = 8,
            SByte = 7,
            UShort = 16,
            Short = 15,
            Int = 31,
            Unsupported = 0
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (bitfieldAttribute.totalBits == 0)
                return EditorGUIUtility.singleLineHeight * 2;
            if (bitfieldAttribute.totalBits > 16)
                return EditorGUIUtility.singleLineHeight * 2 + 280;
            else return (EditorGUIUtility.singleLineHeight * 2 + 140);
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.DrawRect(new Rect(position.x, position.y, position.width, position.height - 10), new Color(0.18f, 0.18f, 0.18f));
            EditorGUI.BeginProperty(position, label, property);
            float doubleLine = EditorGUIUtility.singleLineHeight * 2;
            FieldType thisFieldType = GetFieldType();
            var bitCount = (int)thisFieldType;
            bitfieldAttribute.totalBits = bitCount;
            bool[] bitVals = new bool[bitCount];
            DrawHeader(thisFieldType, new Rect(position.x, position.y, position.width,
                    doubleLine), property.name, (uint)property.intValue);

            for (int i = 0; i < bitCount; i++)
            {
                int yOffset = i / 16;
                Rect togglerect = new Rect(position.x + 20 * (i % 16), position.y + doubleLine + yOffset * 140, 15, 15);
                EditorGUI.BeginChangeCheck();
                bitVals[i] = (property.intValue).GetBit(i);
                bitVals[i] = EditorGUI.Toggle(togglerect, bitVals[i]);
                if (EditorGUI.EndChangeCheck())
                {
                    //A bitfield has changed, reassign to the property
                    int changedflag = 1 << i;
                    property.intValue ^= changedflag;
                }
                EditorGUIUtility.RotateAroundPivot(90, new Vector2(position.x, position.y));
                Rect textRect = new Rect(position.x + 20 + doubleLine + (yOffset * 140), position.y - (i % 16) * 20 - 15, 110, 15);
                if (string.IsNullOrEmpty(bitfieldAttribute.labels[i]) == false)
                    EditorGUI.LabelField(textRect, i + ": " + bitfieldAttribute.labels[i]);
                else
                    EditorGUI.LabelField(textRect, i + ": (unused)");
                EditorGUIUtility.RotateAroundPivot(-90, new Vector2(position.x, position.y));
            }
            EditorGUI.EndProperty();

        }


        /// <summary>
        /// Draws the header box containing the overall info for the BitField
        /// </summary>
        /// <param name="fieldType">Field Type (byte/short/int etc)</param>
        /// <param name="pos">Rect Position</param>
        /// <param name="name">Variable name</param>
        /// <param name="val">Raw BitField Value</param>
        void DrawHeader(FieldType fieldType, Rect pos, string name, uint val)
        {
            string binaryFormat = "";
            switch (fieldType)
            {
                case FieldType.SByte:
                    sbyte sb = (sbyte)val;
                    binaryFormat = sb.ToStringBinary();
                    break;
                case FieldType.Byte:
                    byte b = (byte)val;
                    binaryFormat = b.ToStringBinary();
                    break;
                case FieldType.Short:
                    short s = (short)val;
                    binaryFormat = s.ToStringBinary();
                    break;
                case FieldType.UShort:
                    ushort us = (ushort)val;
                    binaryFormat = us.ToStringBinary();
                    break;
                case FieldType.Int:
                    binaryFormat = val.ToStringBinary();
                    break;
            }
            if (fieldType == FieldType.Unsupported)
            {
                EditorGUI.HelpBox(
                    pos, name + ": Invalid field type. " +
                    "Supported types are sbyte, byte, short, ushort, int and uint.",
                    MessageType.Warning);
            }
            else
            {
                EditorGUI.HelpBox(
                    pos, "BitField: " + name + " (" + fieldType + ") - Value: " + val +
                    "\n" + binaryFormat, MessageType.None
                    );
            }
        }


        /// <summary>
        /// Converts the reflection to a direct enumerator
        /// </summary>
        FieldType GetFieldType()
        {
            if (fieldInfo.FieldType == typeof(byte))
                return FieldType.Byte;
            else if (fieldInfo.FieldType == typeof(sbyte))
                return FieldType.SByte;
            else if (fieldInfo.FieldType == typeof(short))
                return FieldType.Short;
            else if (fieldInfo.FieldType == typeof(ushort))
                return FieldType.UShort;
            else if (fieldInfo.FieldType == typeof(uint) || fieldInfo.FieldType == typeof(int))
                return FieldType.Int;
            else
                return FieldType.Unsupported;
        }
    }

}
#endif