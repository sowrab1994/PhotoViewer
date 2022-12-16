using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImageSearcher
{
	[XmlRoot(ElementName = "photo")]
	public class Photo
	{

		[XmlAttribute(AttributeName = "id")]
		public double Id { get; set; }

		[XmlAttribute(AttributeName = "owner")]
		public string Owner { get; set; }

		[XmlAttribute(AttributeName = "secret")]
		public string Secret { get; set; }

		[XmlAttribute(AttributeName = "server")]
		public int Server { get; set; }

		[XmlAttribute(AttributeName = "farm")]
		public int Farm { get; set; }

		[XmlAttribute(AttributeName = "title")]
		public string Title { get; set; }

		[XmlAttribute(AttributeName = "ispublic")]
		public int Ispublic { get; set; }

		[XmlAttribute(AttributeName = "isfriend")]
		public int Isfriend { get; set; }

		[XmlAttribute(AttributeName = "isfamily")]
		public int Isfamily { get; set; }
	}

	[XmlRoot(ElementName = "photos")]
	public class Photos
	{

		[XmlElement(ElementName = "photo")]
		public List<Photo> Photo { get; set; }

		[XmlAttribute(AttributeName = "page")]
		public int Page { get; set; }

		[XmlAttribute(AttributeName = "pages")]
		public int Pages { get; set; }

		[XmlAttribute(AttributeName = "perpage")]
		public int Perpage { get; set; }

		[XmlAttribute(AttributeName = "total")]
		public int Total { get; set; }
	}

	[XmlRoot(ElementName = "rsp")]
	public class Rsp
	{

		[XmlElement(ElementName = "photos")]
		public Photos Photos { get; set; }

		[XmlAttribute(AttributeName = "stat")]
		public string Stat { get; set; }
	}


}
