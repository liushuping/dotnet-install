using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, "project.json");
            var content = File.ReadAllText(path);
            var project = JObject.Parse(content);
            var dependencies = (JObject)project["dependencies"];
            foreach (var arg in args)
            {
                var parts = arg.Split('@');
                var version = "*";
                if (parts.Length > 1) {
                    version = parts[1];
                }

                dependencies.Add(parts[0], new JObject(
                    new JProperty("version", version)
                ));
            }

            // new JObject("{\"version\":\"*\"}");

            File.WriteAllText(path, project.ToString());
            return 0;
     
            // var nuget = typeof(NuGet.CommandLine.XPlat.Program).GetTypeInfo().Assembly;
            // var main = nuget.EntryPoint;
            // var newArgs = new List<string>(args);
            // newArgs.Insert(0, "--help");
            // foreach (var item in newArgs)
            // {
            //     System.Console.WriteLine(item);
            // }
            // return (int)main.Invoke(null, new object[] { newArgs.ToArray() });
        }
    }
}