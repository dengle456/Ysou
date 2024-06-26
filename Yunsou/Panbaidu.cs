using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Rubyer;
using System.Diagnostics;
using System.Xml.Linq;
using System.Security.Policy;
using System.Windows.Interop;
using System.Configuration;
using System.Runtime.InteropServices;
using ReloadPanClinet.Views;
using Aria2NET;
using System.Security.Cryptography;
using System.Text.Encodings.Web;

namespace ReloadPanClinet
{
    public class ClassPaso
    {

        /// <summary>
        /// 网盘搜索
        /// </summary>
        /// <param name="text"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<string> seach(string text, int page)
        {

          
           
            try
            {
                var Clint = new HttpClient();
                // 发送 GET 请求并等待响应
                var response = Clint.GetAsync($"https://127.0.0.1:4540/tools/query?q={text}&page={page}&diy=1").Result;
                Debug.WriteLine($"https://127.0.0.1:4540/tools/query?q={text}&page={page}&diy=1");

                // 读取响应内容并转换为字符串
                var content =  response.Content.ReadAsStringAsync().Result;
              
      
                return content;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }



        }

     


        /// <summary>
        /// GET网址反正字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetAsStringAsync(string url)
        {
            try
            {
                var Clint = new HttpClient();
                // 发送 GET 请求并等待响应
                var response = Clint.GetAsync($"{url}").Result;
                // 读取响应内容并转换为字符串
                var content = response.Content.ReadAsStringAsync().Result;

                return content;


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
                return null;
            }
        }

    }







}


