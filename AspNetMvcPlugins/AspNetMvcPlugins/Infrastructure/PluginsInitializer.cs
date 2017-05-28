using Autofac;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class PluginsInitializer
    {
        public static void Initialize(this ContainerBuilder builder)
        {
            var pluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/Plugins"));
            var pluginAssemblies = pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories);
            foreach (var pluginAssemblyFile in pluginAssemblies)
            {
                var asm = Assembly.LoadFrom(pluginAssemblyFile.FullName);
                builder.RegisterAssemblyTypes(asm).AsImplementedInterfaces();
            }

            TestIt();
            bool result = TestTxt();
            bool resul2 = TestXml();
        }

        private static bool TestTxt()
        {
            String path = @"c:\test\254.txt";
            String findString = "test string";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(path))
                {
                    String fileContents = sr.ReadToEnd();
                    if (fileContents.Contains(findString))
                        return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
            }
            return false;
        }


        private static bool TestXml()
        {
            //String sourceXml = "<book genre='novel' ISBN='1-861001-57-5' misc='sale item'>" +
            //          "<title>The Handmaid's Tale</title>" +
            //          "<author>" +
            //                "<first-name>Margaret</first-name>" +
            //                "<last-name>Atwood</last-name>" +
            //          "</author>" +
            //          "<price>14.95</price>" +
            //          "</book>";
            String path = @"C:\test\1234.xml";
            String findString = "The Sundered Grail";

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            //doc.LoadXml(sourceXml);
            XmlNode root = doc.DocumentElement;
            var lst = root.SelectNodes(".//text()");
            
            foreach (XmlNode item in lst)
            {
                if(item.Value.Equals(findString))
                    return true;
            }
            return false;
        }

        private static void TestIt()
        {
            String path = @"C:\test\";
            SearchOption searchOption = SearchOption.AllDirectories;
            String searchPattern = "*";
            long fileLength = 2048;
            DateTime creationDate = DateTime.Now;
            FileAttributes fileAttributes = FileAttributes.Normal;

            //var modules = DependencyResolver.Current.GetServices<IPluginModule>();
            //foreach (var item in modules)
            //{
                
            //}

            List<String> result = new List<String>();
            var folder = new DirectoryInfo(path);
            if (!folder.Exists)
                return;
            var files = folder.GetFiles(searchPattern, searchOption);
            foreach (var item in files)
            {
                if (item.Length < fileLength || item.CreationTime < creationDate || item.Attributes == fileAttributes)
                    result.Add(item.Name);
                
            }
        }

    }
}