using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImageSearcher.Flicker
{
    public class FlickerResponseProcessor
    {
        private string imageUrlPrefix = "https://live.staticflickr.com/";
        Rsp rspObj;

        private string FormImageUrl(Photo photoObj)
        {
            return imageUrlPrefix + photoObj.Server + "/" + photoObj.Id + "_" + photoObj.Secret + ".jpg";
        }

        public bool SerializeResponse(string response)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Rsp));
                using (StringReader reader = new StringReader(response))
                {
                    rspObj = (Rsp)serializer.Deserialize(reader);
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public int GetResponsePages()
        {
            return rspObj?.Photos?.Pages?? 0;
        }

        public ArrayList GetImagesUrl()
        {
            ArrayList imgList = new ArrayList();
            foreach(var photo in rspObj?.Photos?.Photo)
            {
                imgList.Add(FormImageUrl(photo));
            }
            return imgList;
        }
    }
}
