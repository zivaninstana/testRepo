using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost\\MyInstance,1434;Data Source=190.190.200.100\\MyInstance,1433;PORT=1234;SHORT=112;Integrated Security=SSPI;";

            //string port = ExtractPortNumber(connectionString);
            string port2 = ExtractPortNumber2(connectionString);

            Match match = Regex.Match(connectionString, @"Port=(?<portNumber>[0-9]+);|(Data Source|Server)=[\S]+,(?<portNumber>[0-9]+);", RegexOptions.IgnoreCase);
            while(match.Success)
            {
                port2 = match.Groups["portNumber"].Value;
                match = match.NextMatch();
                Console.WriteLine($"Port: {port2}");
            }

            //Console.WriteLine($"Port: {port2}");
            Console.ReadKey();
        }

        private static string ExtractPortNumber2(string connectionString)
        {
            string s = "string";
            string s1 = s.Split(',').LastOrDefault();
            return s1;
        }

        private static string ExtractPortNumber(string connectionString)
        {
            SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder(connectionString);

            var portIndexOf = connectionString.IndexOf("port", StringComparison.OrdinalIgnoreCase);
            string portNum = string.Empty;
            if (portIndexOf >= 0)
            {
                var portEndIndex = connectionString.IndexOf(';', portIndexOf);
                if (portEndIndex < 0)
                    portEndIndex = connectionString.Length;
                portNum = connectionString.Substring(portIndexOf + 5, portEndIndex - portIndexOf - 5);
            }

            return portNum;
        }
    }
}
