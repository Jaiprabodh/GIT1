using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Management;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace StreamingEasyTest
{
    class Program
    {
        public static bool IsWindows10orAbove;
        public static string Processorname;
        private static bool IsprocessorNameExist = false;
        public static string EncodedProcessorname;
        public static string DecodedProcessorname;
        static void Main(string[] args)
        {

            //IsWindows10orAbove = IsWindows10();
            // Processorname = SendBackProcessorName();
            //Processorname = "ProcessorName1";
            // ProcessorNameExist(Processorname);

            //EncodedProcessorname = Base64Encode("Intel(R) Core(TM) i5-5300U CPU @ 2.30GHz");
            //DecodedProcessorname = Base64Decode("SW50ZWwoUikgQ29yZShUTSkgaTUtNTMwMFUgQ1BVIEAgMi4zMEdIeg==");
            //string str = "Intel(R) Core(TM) i5-5300U CPU @ 2.30GHz";
            //string str1 = str.Substring(18,9).Trim();

            //Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            Process[] processes = Process.GetProcessesByName("obs64");
            int length = processes.Length;
        }

        static bool IsWindows10()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");
            string OPSystemVersion = System.Environment.MachineName.ToString();

            return productName.StartsWith("Windows 10");
        }

        public static string SendBackProcessorName()
        {
            ManagementObjectSearcher mosProcessor = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            string Procname = null;

            foreach (ManagementObject moProcessor in mosProcessor.Get())
            {
                if (moProcessor["name"] != null)
                {
                    Procname = moProcessor["name"].ToString();

                }

            }

            return Procname;
        }
        public static bool ProcessorNameExist(string Processorname)
        {

            string filepath = "../../Processors.json";
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<Processor>>(json);
                foreach (var item in items)
                {
                    if (Base64Decode(item.ProcessorName) == Processorname)
                    {
                        IsprocessorNameExist = true;

                    }

                }
            }
            return IsprocessorNameExist;
        }
        public class Processor
        {

            public string ProcessorName
            {
                get;
                set;
            }

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
