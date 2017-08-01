namespace Концелярский_Нож
{
    using System.Xml.Serialization;

    public class ValuteCurs
    { 

        [XmlElementAttribute("ValuteCursOnDate")]
        public ValuteCursOnDate[] Valuets;

    }
}