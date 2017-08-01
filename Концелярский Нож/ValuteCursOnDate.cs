namespace Концелярский_Нож
{
    using System;
    using System.IO;
    using System.Windows.Forms.VisualStyles;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("ValuteCursOnDate")]
    public class ValuteCursOnDate
    {
        [XmlElement("VName")]
        public string VName { get; set; }

        [XmlElement("VNome")]
        public string VNome { get; set; }

        [XmlElement("VCurse")]
        public string VCurse { get; set; }

        [XmlElement("VCod")]
        public string VCod { get; set; }

        [XmlElement("VchCode")]
        public string VchCode { get; set; }

    }
}