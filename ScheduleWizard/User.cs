using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ScheduleWizard
{
    public class User
    {
        public static User activeUser = User.ReadFromXML("activeuser.xml");

        // A user has a first and last name and a list of the classes they take, as well as a list of deadlines.
        public List<Class> ClassList = new List<Class>();

        public string Name { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User()
        {

        }

        public User(string first, string last)
        {
            FirstName = first;
            LastName = last;
            Name = first + " " + last;
        }

        public void addClass(Class c)
        {
            ClassList.Add(c);
        }

        public List<ClassTimeSlot> ClassesToday()
        {
            List<ClassTimeSlot> classes = new List<ClassTimeSlot>();
            int curDayOfWeek = (int)DateTime.Now.DayOfWeek;
            foreach (Class c in ClassList)
            {
                foreach (ClassTimeSlot cts in c.TimeSlots)
                {
                    if ((int)cts.Day == curDayOfWeek)
                    {
                        classes.Add(cts);
                    }
                }
            }
            return classes;
        }

        public void CreateXML(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        public static User ReadFromXML(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            /* If the XML document has been altered with unknown
            nodes or attributes, handle them with the
            UnknownNode and UnknownAttribute events.*/
            serializer.UnknownNode += new
            XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new
            XmlAttributeEventHandler(serializer_UnknownAttribute);

            using (var fs = File.Open(filename, FileMode.Open))
            {
                User newUser = (User)serializer.Deserialize(fs);
                foreach (Class c in newUser.ClassList)
                {
                    foreach (ClassTimeSlot cts in c.TimeSlots)
                    {
                        cts.Parent = c;
                    }
                }
                return newUser;
            }
        }

        private static void serializer_UnknownNode (object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        private static void serializer_UnknownAttribute
        (object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}
