///////////////////////////////////////////////////////////////////////////
// TestClient.cs - a file handling service      //
// started with C# Console Application                                   //
///////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;       // need to add reference to System.Web
using System.Net;       // need to add reference to System.Net
using System.Net.Http;  // need to add reference to System.Net.Http
using Newtonsoft.Json;  // need to add reference to System.Json
using System.Threading;

namespace Admin
{
    class TestClient
    {
        private HttpClient client = new HttpClient();
        private HttpRequestMessage message;
        private HttpResponseMessage response = new HttpResponseMessage();
        private string urlBase;
        public string status { get; set; }

        //----< set destination url >------------------------------------------

        public TestClient(string url) { urlBase = url; }

        //----< get list of files available for download >---------------------

        public string[] getAvailableFiles()
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri(urlBase);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response1 = task.Result;
            response = task.Result;
            status = response.ReasonPhrase;
            string[] files = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(response1.Content.ReadAsStringAsync().Result);
            return files;
        }

        //----< delete >---------------------

        public bool deleteFile(string fileName)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Delete;
            string urlActn = "?fileName=" + fileName;
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response1 = task.Result;

            response = task.Result;
            status = response.ReasonPhrase;
            bool success = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(response1.Content.ReadAsStringAsync().Result);
            return success;
        }

        //----< authentication>---------------------

        public bool login(string username, string password)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            string urlActn = "?username=" + username + "&password=" + password;
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response1 = task.Result;

            response = task.Result;
            status = response.ReasonPhrase;
            bool success = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(response1.Content.ReadAsStringAsync().Result);
            return success;
        }

        //----< open file on server for reading >------------------------------

        public int openServerDownLoadFile(string fileName)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            string urlActn = "?fileName=" + fileName + "&open=download";
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            status = response.ReasonPhrase;
            return (int)response.StatusCode;
        }
        //----< open file on client for writing >------------------------------

        public FileStream openClientDownLoadFile(string path)
        {
            FileStream down;
            try
            {
                down = new FileStream(path, FileMode.OpenOrCreate);
            }
            catch
            {
                return null;
            }
            return down;
        }
        //----< read block from server file and write to client file >---------

        private byte[] getFileBlock(FileStream down, int blockSize)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            string urlActn = "?blockSize=" + blockSize.ToString();
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            Task<byte[]> taskb = response.Content.ReadAsByteArrayAsync();
            byte[] Block = taskb.Result;
            status = response.ReasonPhrase;
            return Block;
        }
        //----< close FileStream on server and FileStream on client >----------

        public void closeServerFile()
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            string urlActn = "?fileName=dontCare.txt&open=close";
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            status = response.ReasonPhrase;
        }
        //----< open file on server for writing >------------------------------

        public int openServerUpLoadFile(string fileName)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            string urlActn = "?fileName=" + fileName + "&open=upload";
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            status = response.ReasonPhrase;
            return (int)response.StatusCode;
        }
        //----< open file on client for Reading >------------------------------

        public FileStream openClientUpLoadFile(string path)
        {
            FileStream up;
            try
            {
                up = new FileStream(path, FileMode.Open);
            }
            catch
            {
                return null;
            }
            return up;
        }
        //----< post blocks to server >----------------------------------------
        private void putBlock(byte[] Block)
        {
            message = new HttpRequestMessage();
            message.Method = HttpMethod.Post;
            message.Content = new ByteArrayContent(Block);
            message.Content.Headers.Add("Content-Type", "application/http;msgtype=request");
            string urlActn = "?blockSize=" + Block.Count().ToString();
            message.RequestUri = new Uri(urlBase + urlActn);
            Task<HttpResponseMessage> task = client.SendAsync(message);
            HttpResponseMessage response = task.Result;
            status = response.ReasonPhrase;
        }
        //----< downLoad File >------------------------------------------------
        /*
         *  Open server file for reading
         *  Open client file for writing
         *  Get blocks from server
         *  Write blocks to local file
         *  Close server file
         *  Close client file
         */
        public string downLoadFile(string path)
        {
            try
            {
                FileStream down;
                string filename = System.IO.Path.GetFileName(path);
                int status = openServerDownLoadFile(filename);
                if (status >= 400)
                    return "failed";
                down = openClientDownLoadFile(path);
                if (down == null)
                    return "failed";

                while (true)
                {
                    int blockSize = 512;
                    byte[] Block = getFileBlock(down, blockSize);
                    if (Block.Length == 0 || blockSize <= 0)
                        break;
                    down.Write(Block, 0, Block.Length);
                    if (Block.Length < blockSize)    // last block
                        break;
                }
                closeServerFile();
                down.Close();
            }
            catch (Exception)
            {
                return "failed";
            }
            return "success";

        }
        //----< upLoad File >--------------------------------------------------
        /*
         *  Open server file for writing
         *  Open client file for reading
         *  Read blocks from local file
         *  Send blocks to server
         *  Close server file
         *  Close client file
         */
        public string upLoadFile(string path)
        {
            try
            {
                string fileName = System.IO.Path.GetFileName(path);
                int status = openServerUpLoadFile(fileName);
                if (status >= 400)
                    return "failed";
                FileStream up = openClientUpLoadFile(path);
                if (up == null)
                    return "failed";

                const int upBlockSize = 512;
                byte[] upBlock = new byte[upBlockSize];
                int bytesRead = upBlockSize;
                while (bytesRead == upBlockSize)
                {
                    bytesRead = up.Read(upBlock, 0, upBlockSize);
                    if (bytesRead < upBlockSize)
                    {
                        byte[] temp = new byte[bytesRead];
                        for (int i = 0; i < bytesRead; ++i)
                            temp[i] = upBlock[i];
                        upBlock = temp;
                    }
                    putBlock(upBlock);
                }
                closeServerFile();
                up.Close();
                return "success";//success
            }
            catch (Exception)
            {
                return "failed";//error
            }

        }
    }
}
