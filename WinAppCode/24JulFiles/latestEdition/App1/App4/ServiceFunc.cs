using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
namespace App4
{
    class ServiceFunc
    {
        public  static async Task<int> GetDataSevice(string user, string pass, int function)
        {

            return await App.ShelfMobile.InvokeApiAsync<int>("shelf/Action", HttpMethod.Get, new Dictionary<string, string>() {
               { "user", user.ToString()} ,{ "pass", pass.ToString() },{ "function",function.ToString()} }
                );
        }

    }
}
