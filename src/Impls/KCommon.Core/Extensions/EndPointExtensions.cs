using System;
using System.Collections.Generic;
using System.Net;
using ECommon.Utilities;
using KCommon.Core.Utilities;
using KCommon.Core.Validations;

namespace KCommon.Core.Extensions
{
    public static class EndPointExtensions
    {
        public static string ToAddress(this EndPoint endpoint)
        {
            if (!new NotNull().IsValid(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            return ((IPEndPoint) endpoint).ToAddress();
        }

        public static string ToAddress(this IPEndPoint endpoint)
        {
            if (!new NotNull().IsValid(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            return string.Format("{0}:{1}", endpoint.Address, endpoint.Port);
        }

        public static IPEndPoint ToEndPoint(this string address)
        {
            if (!new NotNull().IsValid(address)) throw new ArgumentNullException(nameof(address));

            var array = address.Split(new string[] {":"}, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length != 2) throw new Exception("Invalid endpoint address: " + address);
            var ip = IPAddress.Parse(array[0]);
            var port = int.Parse(array[1]);
            return new IPEndPoint(ip, port);
        }

        public static IEnumerable<IPEndPoint> ToEndPoints(this string addresses)
        {
            if (!new NotNull().IsValid(addresses)) throw new ArgumentNullException(nameof(addresses));

            var array = addresses.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<IPEndPoint>();
            foreach (var item in array) list.Add(item.ToEndPoint());
            return list;
        }
    }
}