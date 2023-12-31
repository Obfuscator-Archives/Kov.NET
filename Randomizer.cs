﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace kov.NET.Utils
{
    /// <summary>
    /// This class is the one that generates random integers and strings.
    /// </summary>
    public class Randomizer
    {
        private static readonly RandomNumberGenerator csp = RandomNumberGenerator.Create();
        private static readonly char[] chars = "duckduckduckduckduckduckduckduckduckduckduck".ToCharArray();

        public static int Next(int maxValue, int minValue = 0)
        {
            if (minValue >= maxValue) throw new ArgumentOutOfRangeException(nameof(minValue));
            long diff = (long)maxValue - minValue;
            long upperBound = uint.MaxValue / diff * diff;
            uint ui;
            do { ui = RandomUInt(); } while (ui >= upperBound);
            return (int)(minValue + (ui % diff));
        }

        public static string GenerateRandomString(int size)
        {
            byte[] data = new byte[4 * size];
            csp.GetNonZeroBytes(data);
            StringBuilder sb = new StringBuilder(size);
            for (int i = 0; i < size; i++) sb.Append(chars[BitConverter.ToUInt32(data, i * 4)
                % chars.Length]);
            return sb.ToString();
        }

        private static uint RandomUInt()
        {
            return BitConverter.ToUInt32(RandomBytes(sizeof(uint)), 0);
        }

        private static byte[] RandomBytes(int bytesNumber)
        {
            byte[] buffer = new byte[bytesNumber];
            csp.GetNonZeroBytes(buffer);
            return buffer;
        }
    }
}
