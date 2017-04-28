using System;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Linq;



namespace superputtyShuttleConverter
{
    class PuttyShuttle
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            if(args.Any()) {

	            String infilePath = args[0];

	            if(File.Exists(infilePath)) {

	                XDocument doc = XDocument.Load(infilePath);

                    foreach(XElement el in doc.Root.Elements()) {
                        Console.WriteLine("Processing {1}", el.Name, el.Attribute("SessionId").Value);
    
                        String sessionId = el.Attribute("SessionId").Value;
                        String sessionName = el.Attribute("SessionName").Value;
                        //String imageKey = el.Attribute("imageKey").Value;
                        String host = el.Attribute("Host").Value;
                        String port = el.Attribute("Port").Value;
                        String protocol = el.Attribute("Proto").Value;
                        String puttySession = el.Attribute("PuttySession").Value;
                        String username = el.Attribute("Username").Value;
                        String extraArgs = el.Attribute("ExtraArgs").Value;


                    
                    
                    
                    }


	            } else {
	                Console.WriteLine("path " + infilePath + " not found");
	            }

            } else {
                Console.WriteLine("usage: PuttyShuttle.exe <puttySessions.xml>");
            }

        }
    }



}
