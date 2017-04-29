using System;
using System.Collections;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;



namespace superputtyShuttleConverter
{
    class PuttyShuttle
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            if(args.Any()) {

	            String infilePath = args[0];
                ArrayList sessions = new ArrayList();


	            if(File.Exists(infilePath)) {

	                XDocument doc = XDocument.Load(infilePath);

                    foreach(XElement el in doc.Root.Elements()) {
                        Console.WriteLine("Processing {0}", el.Attribute("SessionId").Value);

                        Session s = new Session();
    
                        s.sessionId = el.Attribute("SessionId").Value;
                        s.sessionName = el.Attribute("SessionName").Value;
						s.imageKey = el.Attribute("imageKey").Value;
						s.host = el.Attribute("Host").Value;
                        s.port = el.Attribute("Port").Value;
                        s.protocol = el.Attribute("Proto").Value;
                        s.puttySession = el.Attribute("PuttySession").Value;
                        s.username = el.Attribute("Username").Value;
                        s.extraArgs = el.Attribute("ExtraArgs").Value;

                        sessions.Add(s);

                        Console.WriteLine(sessions);
                    }

                    JArray array = new JArray();


	            } else {
	                Console.WriteLine("path " + infilePath + " not found");
	            }

            } else {
                Console.WriteLine("usage: PuttyShuttle.exe <puttySessions.xml>");
            }

        }
    }

    class Session 
    {
        public string sessionId;
        public string sessionName;
        public string imageKey;
        public string host;
        public string port;
        public string protocol;
        public string puttySession;
        public string username;
        public string extraArgs;
    }


}
