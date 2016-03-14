///////////////////////////////////////////////////////////////
// Session.cs - Thread-safe storage using identifier key     //
//                                                           //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2014   //
///////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace final1.Models
{
  public class Session
  {
    static object synch = new object();              // lock object
    static Dictionary<string, FileStream> streams;   // session storage
    static int id;                                   // session identifier
    static DateTime time;                            // lease time
    /*
     *  I've changed controller to remove session from streams when file is closed
     *  so we don't need to manage lifetime anymore.  I haven't gotten around to
     *  removing the small amount of code to manage lifetime.
     */

    static Session() 
    { 
      streams = new Dictionary<string,FileStream>();
      id = -1;
    }

    public string incrSessionId()
    {
      lock (synch) { return ((++id).ToString()); }
    }

    public string getSessionId()
    {
      lock(synch)
      {
        return id.ToString();
      }
    }
    public void saveStream(FileStream stream, string sessionId)
    {
      lock(synch)
      {
        DateTime lease = time.AddDays(1);
        if (lease < DateTime.Now)
        {
          streams = new Dictionary<string, FileStream>();  // prune old streams
          time = DateTime.Now.AddDays(1);
        }
        streams[sessionId] = stream;
      }
    }
    public FileStream getStream(string sessionId)
    {
      lock(synch)
      {
        return streams[sessionId];
      }
    }

    public void removeStream(string sessionId)
    {
      streams.Remove(sessionId);
    }
  }
}