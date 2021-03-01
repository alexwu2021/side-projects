using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace ServiceTestHarness
{
    public class TodosPostTester
    {
        private static readonly string GET_ONE_TODO_URL = "https://localhost:44371/api/todos/get/?";
        private static readonly string GET_ALL_TODO_URL = "https://localhost:44371/api/todos/getall";
        private static readonly string UPDATE_TODO_URL = "https://localhost:44371/api/todos/update";
        private static readonly string ADD_TODO_URL = "https://localhost:44371/api/todos/add";
        private static readonly string DELETE_TODO_URL = "https://localhost:44371/api/todos/del/?";
        private static readonly string APPLICAITON_JSON = "application/json";
        private readonly Dictionary<CRUDOperationEnum, string> _enumToUrl;
        private Random _random;


        public TodosPostTester()
        {
            _enumToUrl = new Dictionary<CRUDOperationEnum, string>();
            _enumToUrl.Add(CRUDOperationEnum.Create, ADD_TODO_URL);
            _enumToUrl.Add(CRUDOperationEnum.Delete, DELETE_TODO_URL);
            _enumToUrl.Add(CRUDOperationEnum.ReadAll, GET_ALL_TODO_URL);
            _enumToUrl.Add(CRUDOperationEnum.ReadOne, GET_ONE_TODO_URL);
            _enumToUrl.Add(CRUDOperationEnum.Update, UPDATE_TODO_URL);
            _random = new Random(DateTime.Now.Millisecond);
        }

        public async void TestCrud()
        {
            foreach(var entry in this._enumToUrl)
            {
                var url = entry.Value;
                string result;
                switch (entry.Key)
                {
                    //case CRUDOperationEnum.Create:
                    //    await TestAddAsync(url);
                    //    break;
                    case CRUDOperationEnum.Delete:
                        int idToDelete = 1 + this._random.Next(3);
                        idToDelete = 3;
                        url = url.Replace("?", idToDelete.ToString());
                        await TestDeleteAsync(url);
                        break;
                    //case CRUDOperationEnum.ReadAll:
                    //    result = await TestGetAll(url);
                    //    Debug.WriteLine(string.Format("read all result {0}", result));
                    //    break;
                    //case CRUDOperationEnum.ReadOne:
                    //    int idToGet = 1 + this._random.Next(3);
                    //    url = url.Replace("?", idToGet.ToString());
                    //    result = await TestGetOne(url);
                    //    Debug.WriteLine(string.Format("read one result {0}", result));
                    //    break;
                    //case CRUDOperationEnum.Update:
                    //    await TestUpdateAsync(url);
                    //    break;
                }
            }
            return;
        }

        internal async System.Threading.Tasks.Task<bool> TestAddAsync(string url)
        {
            var todo = Utilities.GetTodoFromFile("todo1.json");
            todo.startTime = DateTime.Now.AddDays(1);
            todo.name += "_" + 1 + _random.Next(1000);
            var json = Utilities.SerializeFromTodoObject(todo);

            var httpContent = new StringContent(json, Encoding.UTF8, APPLICAITON_JSON);
            using (HttpClient httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.PostAsync(url, httpContent);
                if (httpResponse.Content != null)
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine(string.Format("response for add = {0}", response));
                    return true;
                }
            }
            return false;
        }

        internal async System.Threading.Tasks.Task<bool> TestUpdateAsync(string url)
        {
            var todo = Utilities.GetTodoFromFile("todo2.json");
            todo.startTime = DateTime.Now.AddDays(1);
            todo.name += "_" + 1 + _random.Next(1000);
            var json = Utilities.SerializeFromTodoObject(todo);

            var httpContent = new StringContent(json, Encoding.UTF8, APPLICAITON_JSON);
            using (HttpClient httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.PostAsync(url, httpContent);
                if (httpResponse.Content != null)
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine(string.Format("response for update = {0}", response));
                    return true;
                }
            }
            return false;

        }

        internal async System.Threading.Tasks.Task<string> TestDeleteAsync(string url)
        {
            using var client = new HttpClient();
            var res = await client.GetStringAsync(url);//.ConfigureAwait(false);
            Debug.WriteLine(string.Format("result = {0}", res));
            return res;
        }


        internal async System.Threading.Tasks.Task<string> TestGetOne(string url)
        {
            using var client = new HttpClient();
            var res = await client.GetStringAsync(url);//.ConfigureAwait(false);
            Debug.WriteLine(string.Format("result = {0}", res));
            return res;
        }

        internal async System.Threading.Tasks.Task<string> TestGetAll(string url)
        {
            using var client = new HttpClient();
            var res = await client.GetStringAsync(url).ConfigureAwait(false);
            Debug.WriteLine(string.Format("result = {0}", res));
            return res;
        }
    }
}
