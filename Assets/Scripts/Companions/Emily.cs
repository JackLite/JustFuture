using System.Xml.Linq;
using System.Linq;


namespace Game.Companions
{
    public class Emily : Companion
    {
        public new float damage = 200f;
        public new float fireSpeed = 150f;

        public override Info GetInfo()
        {
            if (this.info.name == null)
            {
                XDocument xdoc = XDocument.Load("Assets/Info/CompanionsMenu.xml");

                var info = from xe in xdoc.Element("companions").Elements("companion")
                           where xe.Attribute("name").Value == "Emily"
                           select new Info
                           {
                               name = xe.Element("name").Value,
                               description = xe.Element("description").Value
                           };

                this.info = info.First();
            }
            return this.info;
        }
    }
}
