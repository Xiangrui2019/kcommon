﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace KCommon.Core.Extensions
{
    public static class ListExtensions
    {
        private static Random _rng = new Random();

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = _rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}