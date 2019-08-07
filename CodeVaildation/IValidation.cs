using CodeVaildation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace CodeVaildation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IValidation" in both code and config file together.
    [ServiceContract]
    public interface IValidation
    {   
        [OperationContract]        
        int VaildateCode(XmlElement xml);       

    }
}
