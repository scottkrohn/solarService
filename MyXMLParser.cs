using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace SolarService
{
	// This class is used to parse specific data out of an xml file passed to the constructor.
	public class MyXMLParser
	{
		XmlDocument xmldoc;
		public MyXMLParser(Stream xmlStream)
		{
			xmldoc = new XmlDocument();
			xmldoc.Load(xmlStream);
		}

		// parse the annual average solar intensity for a location from xml file.
		public double getAverageIntensity()
		{
			XmlNodeList nodes = xmldoc.DocumentElement.SelectNodes("//response/outputs/avg-dni");
			double average = -1;
			foreach(XmlNode node in nodes)
			{
				average = Convert.ToDouble(node.SelectSingleNode("annual").InnerText);
			}
			return average;
		}

		// parse the latitude of a zipcode from xml file.
		public double getLatitude()
		{
			XmlNodeList nodes = xmldoc.DocumentElement.SelectNodes("//GeocodeResponse/result/geometry/location");
			double latitude = -1;
			foreach(XmlNode node in nodes)
			{ 
				latitude = Convert.ToDouble(node.SelectSingleNode("lat").InnerText);
			}
			return latitude;
		}

		// parse the longitude of a zipcode from xml file.
		public double getLongitude()
		{
			XmlNodeList nodes = xmldoc.DocumentElement.SelectNodes("//GeocodeResponse/result/geometry/location");
			double longitude = -1;
			foreach(XmlNode node in nodes)
			{ 
				longitude = Convert.ToDouble(node.SelectSingleNode("lng").InnerText);
			}
			return longitude;
		}

		// parse the location information (city, county, state, country) from xml file.
		public string getLocationData()
		{
			XmlNodeList nodes = xmldoc.DocumentElement.SelectNodes("//GeocodeResponse/result/address_component");
			string locationString = "";
			foreach(XmlNode node in nodes)
			{
				locationString += string.Format("{0}, " ,node.SelectSingleNode("long_name").InnerText);
			}
			return locationString.Substring(0, locationString.Length-2);
		}

	}
}
