using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using System.Web;

namespace CodeVaildation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Validation" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Validation.svc or Validation.svc.cs at the Solution Explorer and start debugging.
    public class Validation : IValidation
    {
        public int VaildateCode(XmlElement xml)
        {
         var IsXmlVaild = VaildateXml(xml);

            if (IsXmlVaild)
            {
                var VaildateCommandValue = CheckForNodeCommand(xml, "Command","//DeclarationList");
                var VaildateSiteId = CheckForNodeValue(xml, "//SiteID");

                if(VaildateCommandValue != 0)
                {
                    return VaildateCommandValue;
                }

                if(VaildateSiteId != 0)
                {
                    return VaildateSiteId;
                }
                return 0;
            }
            else
            {
                return -3;
            }
            return -3;
        }

        private bool VaildateXml(XmlElement xml)
        {
            try
            {
                string xsdPath = HttpContext.Current.Server.MapPath(@"~\Input.xsd");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml.OuterXml);
                doc.Schemas.Add(null, xsdPath);

                doc.Validate(null);

                return true;

            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        private int CheckForNodeCommand(XmlElement xml, string command,string node)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.OuterXml);

            var referenceList = doc.SelectNodes(node);

            foreach (XmlNode a in referenceList)
            {
                if(a.FirstChild.Attributes[0].Name == command && a.FirstChild.Attributes[0].Value != "")
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }

                return 0;
        }

        private int CheckForNodeValue(XmlElement xml, string node)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.OuterXml);

            var referenceList = doc.SelectNodes(node);

            foreach (XmlNode a in referenceList)
            {
                if(a.InnerText == "")
                {
                    return -2;
                }else
                {
                    return 0;
                }
            }

            return 0;
        }


    }
}
