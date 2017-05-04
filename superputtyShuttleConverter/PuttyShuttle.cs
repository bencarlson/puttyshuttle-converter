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
                String outfilePath = "shuttle.json";
                if (args.Length > 1)
                {
					if (!args[1].Trim().Equals(null))
					{
                        outfilePath = args[1].Trim(); 
                    }
                }
                IList sessions = new ArrayList();


	            if(File.Exists(infilePath)) {

	                XDocument doc = XDocument.Load(infilePath);

                    foreach(XElement el in doc.Root.Elements()) {
                        //Console.WriteLine("Processing {0}", el.Attribute("SessionId").Value);

                        Session s = new Session();
    
                        s.sessionId = el.Attribute("SessionId").Value;
                        s.sessionName = el.Attribute("SessionName").Value;
						s.imageKey = el.Attribute("ImageKey").Value;
						s.host = el.Attribute("Host").Value;
                        s.port = el.Attribute("Port").Value;
                        s.protocol = el.Attribute("Proto").Value;
                        s.puttySession = el.Attribute("PuttySession").Value;
                        s.username = el.Attribute("Username").Value;
                        s.extraArgs = el.Attribute("ExtraArgs").Value;

                        sessions.Add(s);


                    }


                    JObject root = new JObject(
                        new JProperty("_comments",
                                      new JArray(
                                          "Valid terminals include: 'Terminal.app' or 'iTerm'",
    "In the editor value change 'default' to 'nano', 'vi', or another terminal based editor.",
    "Hosts will also be read from your ~/.ssh/config or /etc/ssh_config file, if available",
    "For more information on how to configure, please see http://fitztrev.github.io/shuttle/"
                                         )),
                        new JProperty("editor", "default"),
                        new JProperty("launch_at_login", false),
                        new JProperty("terminal", "Terminal.app"),
                        new JProperty("iTerm_version", "nightly"),
                        new JProperty("default_theme", "Homebrew"),
                        new JProperty("open_in", "new"),
                        new JProperty("show_ssh_config_hosts", false),
                        new JProperty("ssh_config_ignore_hosts", new JArray()),
                        new JProperty("ssh_config_ignore_keywords", new JArray()),
                        new JProperty("hosts", GetHostsArray(sessions))
                  
                    );


					//Console.WriteLine(root.ToString());


                    File.WriteAllText(outfilePath, root.ToString());
                    Console.WriteLine("Saved to: " + outfilePath);


	            } else {
	                Console.WriteLine("path " + infilePath + " not found");
	            }

            } else {
                Console.WriteLine("usage: PuttyShuttle.exe <puttySessions.xml> [optional <output filename>]");
            }

        }




        public static JArray GetHostsArray(IList sessions)
		{


            foreach (Session s in sessions.Cast<Session>())
            {
                String[] sub = s.sessionId.Split('/');
                Console.WriteLine("->");
                foreach (string su in sub)
                    Console.Write(" " + su + " | ");
                Console.WriteLine(s.sessionId);
                s.path = sub;
            }

			return new JArray(
				from ses in sessions.Cast<Session>()
				  select new JObject(

                      
					  new JProperty("cmd",
                                    ses.protocol.ToLower() + " " + (!(String.IsNullOrEmpty(ses.username.Trim())) ? ses.username
                                                                    + "@" : "") + ses.host
						 ),
					  new JProperty("inTerminal", "tab"),
					  new JProperty("name", ses.sessionName),
					  new JProperty("theme", "basic"),
					  new JProperty("title", ses.sessionName)
					 )
				 );
		}

        
    }



    class Session 
    {
        public String[] path;
        public string sessionId;
        public string sessionName;
        public string imageKey;
        public string host;
        public string port;
        public string protocol;
        public string puttySession;
        public string username;
        public string extraArgs;

        public override string ToString()
        {
            return "\n" + sessionId + ": " + host + ":" + port + ", " + protocol;
        }
    }





}
