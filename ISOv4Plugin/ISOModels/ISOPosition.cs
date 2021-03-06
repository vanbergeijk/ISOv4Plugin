/*
 * ISO standards can be purchased through the ANSI webstore at https://webstore.ansi.org
*/

using AgGateway.ADAPT.ISOv4Plugin.ExtensionMethods;
using AgGateway.ADAPT.ISOv4Plugin.ISOEnumerations;
using System;
using System.Collections.Generic;
using System.Xml;

namespace AgGateway.ADAPT.ISOv4Plugin.ISOModels
{
    public class ISOPosition : ISOElement
    {
        //Attributes
        public decimal? PositionNorth { get; set; }
        public decimal? PositionEast { get; set; }
        public long? PositionUp { get; set; }
        public ISOPositionStatus? PositionStatus { get; set; }
        public decimal? PDOP { get; set; }
        public decimal? HDOP { get; set; }
        public byte? NumberOfSatellites { get; set; }
        public long? GpsUtcTime { get; set; }
        public int? GpsUtcDate { get; set; }

        public bool HasPositionNorth { get; set; }
        public bool HasPositionEast { get; set; }
        public bool HasPositionUp { get; set; }
        public bool HasPositionStatus { get; set; }
        public bool HasPDOP { get; set; }
        public bool HasHDOP { get; set; }
        public bool HasNumberOfSatellites { get; set; }
        public bool HasGpsUtcTime { get; set; }
        public bool HasGpsUtcDate { get; set; }


        public override XmlWriter WriteXML(XmlWriter xmlBuilder)
        {
            xmlBuilder.WriteStartElement("PTN");
            xmlBuilder.WriteXmlAttribute<decimal>("A", PositionNorth);
            xmlBuilder.WriteXmlAttribute<decimal>("B", PositionEast);
            xmlBuilder.WriteXmlAttribute("C", PositionUp);
            xmlBuilder.WriteXmlAttribute("D", ((int)PositionStatus).ToString());
            xmlBuilder.WriteXmlAttribute("E", PDOP);
            xmlBuilder.WriteXmlAttribute("F", HDOP);
            xmlBuilder.WriteXmlAttribute("G", NumberOfSatellites);
            xmlBuilder.WriteXmlAttribute("H", GpsUtcTime);
            xmlBuilder.WriteXmlAttribute("I", GpsUtcDate);
            xmlBuilder.WriteEndElement();

            return xmlBuilder;
        }

        public static ISOPosition ReadXML(XmlNode node)
        { 
            ISOPosition position = new ISOPosition();
            position.PositionNorth = node.GetXmlNodeValueAsNullableDecimal("@A");
            position.PositionEast = node.GetXmlNodeValueAsNullableDecimal("@B");
            position.PositionUp = node.GetXmlNodeValueAsNullableLong("@C");

            string status = node.GetXmlNodeValue("@D");
            if (status != string.Empty)
            {
                position.PositionStatus = (ISOPositionStatus)(Int32.Parse(node.GetXmlNodeValue("@D")));
            }
            else
            {
                position.PositionStatus = null; 
            }

            position.PDOP = node.GetXmlNodeValueAsNullableDecimal("@E");
            position.HDOP = node.GetXmlNodeValueAsNullableDecimal("@F");
            position.NumberOfSatellites = node.GetXmlNodeValueAsNullableByte("@G");
            position.GpsUtcTime = node.GetXmlNodeValueAsNullableLong("@H");
            position.GpsUtcDate = node.GetXmlNodeValueAsNullableInt("@I");

            position.HasPositionNorth = node.IsAttributePresent("A");
            position.HasPositionEast = node.IsAttributePresent("B");
            position.HasPositionUp = node.IsAttributePresent("C");
            position.HasPositionStatus = node.IsAttributePresent("D");
            position.HasPDOP = node.IsAttributePresent("E");
            position.HasHDOP = node.IsAttributePresent("F");
            position.HasNumberOfSatellites = node.IsAttributePresent("G");
            position.HasGpsUtcTime = node.IsAttributePresent("H");
            position.HasGpsUtcDate = node.IsAttributePresent("I");

            return position;
        }

        public static List<ISOPosition> ReadXML(XmlNodeList nodes)
        {
            List<ISOPosition> items = new List<ISOPosition>();
            foreach (XmlNode node in nodes)
            {
                items.Add(ISOPosition.ReadXML(node));
            }
            return items;
        }
    }
}