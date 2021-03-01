using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Todos.Api.Models;

namespace ServiceTestHarness
{
    public class Utilities
    {

        public static Todos.Api.Models.Todo GetTodoFromFile(string fileName)
        {
            var rawJson = GetFileContent(fileName);
            var todo = Utilities.DeserializeToTodoObject(rawJson);
            return todo;
        }
        

        public static Todos.Api.Models.Todo DeserializeToTodoObject(string jsonString)
        {
            var setting = new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            Todos.Api.Models.Todo ret = null;
            try
            {
                ret = (Todos.Api.Models.Todo)JsonConvert.DeserializeObject(jsonString, typeof(Todos.Api.Models.Todo));//, setting);
            }catch(Exception ex)
            {
                Debug.WriteLine(string.Format("Exception encountered in {0}, ex={1}", MethodInfo.GetCurrentMethod(), ex));
            }
            return ret;
        }

        public static string SerializeFromTodoObject(Todos.Api.Models.Todo todo)
        {
            string ret = JsonConvert.SerializeObject(todo);
            return ret;
        }


        public static string GetFileContent(string fileName)
        {
            var ret = string.Empty;
            var file = Path.Combine(AssemblyDirectory, "Data", fileName);
            using (var sw = new StreamReader(file))
            {
                ret = sw.ReadToEnd();
            }
            return ret;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
