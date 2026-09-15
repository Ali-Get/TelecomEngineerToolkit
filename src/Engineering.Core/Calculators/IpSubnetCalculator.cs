using System;
using System.Linq;
using System.Net;

namespace Engineering.Core.Calculators
{
    public static class IpSubnetCalculator
    {
        public class SubnetInfo
        {
            public string NetworkAddress { get; set; } = "";
            public string BroadcastAddress { get; set; } = "";
            public string FirstHost { get; set; } = "";
            public string LastHost { get; set; } = "";
            public int NumberOfHosts { get; set; }
            public string SubnetMask { get; set; } = "";
            public string WildcardMask { get; set; } = "";
            public int Cidr { get; set; }
        }

        public static SubnetInfo Calculate(string ipAddress, int cidr)
        {
            if (!IPAddress.TryParse(ipAddress, out var ip))
                throw new ArgumentException("Invalid IP address");
            if (cidr < 0 || cidr > 32)
                throw new ArgumentException("CIDR must be between 0 and 32");

            uint ipValue = BitConverter.ToUInt32(ip.GetAddressBytes().Reverse().ToArray(), 0);
            uint mask = cidr == 0 ? 0 : uint.MaxValue << (32 - cidr);
            uint wildcard = ~mask;
            uint network = ipValue & mask;
            uint broadcast = network | wildcard;

            uint firstHost = cidr >= 31 ? network : network + 1;
            uint lastHost = cidr >= 31 ? broadcast : broadcast - 1;
            int numberOfHosts = cidr >= 31 ? (cidr == 32 ? 1 : 2) : (int)(broadcast - network - 1);

            return new SubnetInfo
            {
                NetworkAddress = UIntToIp(network),
                BroadcastAddress = UIntToIp(broadcast),
                FirstHost = UIntToIp(firstHost),
                LastHost = UIntToIp(lastHost),
                NumberOfHosts = numberOfHosts,
                SubnetMask = UIntToIp(mask),
                WildcardMask = UIntToIp(wildcard),
                Cidr = cidr
            };
        }

        private static string UIntToIp(uint ip)
        {
            byte[] bytes = BitConverter.GetBytes(ip);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return new IPAddress(bytes).ToString();
        }
    }
}