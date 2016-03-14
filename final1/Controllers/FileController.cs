///////////////////////////////////////////////////////////////////////////
// FileController.cs - Demonstrates how to start a file handling service //
//                                                                       //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2014               //
///////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;


using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using final1.Models;

namespace final1.Controllers
{
    public class FileController : ApiController
    {
        private string localRepositoryPath = "~\\Content\\pic";



        //----< GET api/File - get list of available files >---------------

        public IEnumerable<string> Get()
        {
            // available files
            string path = System.Web.HttpContext.Current.Server.MapPath(localRepositoryPath);
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; ++i)
                files[i] = Path.GetFileName(files[i]);
            return files;
        }

        //----< GET api/File?fileName=foobar.txt&open=true >---------------
        //----< attempt to open or close FileStream >----------------------

        public HttpResponseMessage Get(string fileName, string open)
        {
            string sessionId;
            var response = new HttpResponseMessage();
            Models.Session session = new Models.Session();

            CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            if (cookie == null)
            {
                sessionId = session.incrSessionId();
                cookie = new CookieHeaderValue("session-id", sessionId);
                cookie.Expires = DateTimeOffset.Now.AddDays(1);
                cookie.Domain = Request.RequestUri.Host;
                cookie.Path = "/";
            }
            else
            {
                sessionId = cookie["session-id"].Value;
            }

            try
            {
                FileStream fs;
                string path = System.Web.HttpContext.Current.Server.MapPath(localRepositoryPath);
                if (open == "download")  // attempt to open requested fileName
                {
                    //path = path + "DownLoad";
                    string currentFileSpec = path + "\\" + fileName;
                    fs = new FileStream(currentFileSpec, FileMode.Open);
                    session.saveStream(fs, sessionId);
                }
                else if (open == "upload")
                {
                    //path = path + "UpLoad";
                    string currentFileSpec = path + "\\" + fileName;
                    fs = new FileStream(currentFileSpec, FileMode.OpenOrCreate);
                    session.saveStream(fs, sessionId);
                }
                else  // close FileStream
                {
                    fs = session.getStream(sessionId);
                    session.removeStream(sessionId);
                    fs.Close();
                }
                response.StatusCode = (HttpStatusCode)200;
            }
            catch
            {
                response.StatusCode = (HttpStatusCode)400;
            }
          finally  // return cookie to save current sessionId
            {
                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            }
            return response;
        }

        //----< GET api/File?blockSize=2048 - get a block of bytes >-------

        public HttpResponseMessage Get(int blockSize)
        {
            // get FileStream and read block

            Models.Session session = new Models.Session();
            CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            string sessionId = cookie["session-id"].Value;
            FileStream down = session.getStream(sessionId);
            byte[] Block = new byte[blockSize];
            int bytesRead = down.Read(Block, 0, blockSize);
            if (bytesRead < blockSize)  // compress block
            {
                byte[] returnBlock = new byte[bytesRead];
                for (int i = 0; i < bytesRead; ++i)
                    returnBlock[i] = Block[i];
                Block = returnBlock;
            }
            // make response message containing block and cookie

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            message.Content = new ByteArrayContent(Block);
            return message;
        }
        public HttpResponseMessage Post()
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            message.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            Task<byte[]> taskb = Request.Content.ReadAsByteArrayAsync();
            byte[] Block = taskb.Result;
            Models.Session session = new Models.Session();
            string sessionId = session.getSessionId();
            FileStream upload = session.getStream(sessionId);
            upload.Write(Block, 0, Block.Length);

            return message;
        }
        // POST api/file
        public HttpResponseMessage Post(int blockSize)
        {
            Task<byte[]> task = Request.Content.ReadAsByteArrayAsync();
            byte[] Block = task.Result;
            CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            ////////////////////////////////////////////////////////////////////////////////////

            //HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);

            Models.Session session = new Models.Session();
            string sessionId = session.getSessionId();
            FileStream upload = session.getStream(sessionId);
            upload.Write(Block, 0, Block.Length);






            // This shows you how to extract the upload block
            // and get the session from the Request cookie.
            // Use that to get the stream, write the block,
            // then send back the response as below
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NoContent);
            message.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return message;
        }

        // PUT api/file/5
        //public void Put(int id, [FromBody]string value)
        //{
        //  string debug = "debug";
        //}

        //Get user information
        private UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //Authentication
        public bool Post(string username, string password)
        {
            var user = UserManager.Find(username, password);
            if (user != null)
            {
                if (UserManager.IsInRole(user.Id, "admin"))
                    return true;
                else
                    return false;
            }    
            else
                return false;
        }

        // DELETE api/file/5
        public bool Delete(string fileName)
        {
            //get target file name
            string path = System.Web.HttpContext.Current.Server.MapPath(localRepositoryPath);
            string currentFileSpec = path + "\\" + fileName;

            //check if the file exist
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file == currentFileSpec)
                {
                    File.Delete(file);
                    return true;
                }
            }
            return false;
        }
    }
}
