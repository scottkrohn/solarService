using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SolarService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
	public class Service1 : IService1
	{
		public double getSolarIntensity(double lat, double lon)
		{
			string sURL = string.Format("https://developer.nrel.gov/api/solar/solar_resource/v1.xml?api_key=3e2vJlG6bRBWFQeFzwbYZFIYfDoU6Fk2XqhfoWeP&lat={0}&lon={1}", lat, lon);
			WebRequest request_solar = WebRequest.Create(sURL);
			Stream solarStream;
			solarStream = request_solar.GetResponse().GetResponseStream();
			MyXMLParser myParser = new MyXMLParser(solarStream);
			return myParser.getAverageIntensity();
		}

		public Tuple<double, double> getLatLong(string zipcode)
		{
			// Convert the zip code into a latitude and logitide pair using the Google Geocode API.
			string zURL = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}", zipcode);
			WebRequest request_latlong = WebRequest.Create(zURL);
			Stream latLongStream = request_latlong.GetResponse().GetResponseStream();
			MyXMLParser zipcodeParser = new MyXMLParser(latLongStream);
			double lat = zipcodeParser.getLatitude();
			double lon = zipcodeParser.getLongitude();
			return new Tuple<double,double>(lat, lon);
		}

		public string getLocationData(string zipcode)
		{
			string zURL = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}", zipcode);
			WebRequest request_location = WebRequest.Create(zURL);
			Stream locationStream = request_location.GetResponse().GetResponseStream();
			MyXMLParser locationParser = new MyXMLParser(locationStream);
			return locationParser.getLocationData();
		}

		public string GetData(int value)
		{
			return string.Format("You entered: {0}", value);
		}

		public CompositeType GetDataUsingDataContract(CompositeType composite)
		{
			if (composite == null)
			{
				throw new ArgumentNullException("composite");
			}
			if (composite.BoolValue)
			{
				composite.StringValue += "Suffix";
			}
			return composite;
		}
	}
}
