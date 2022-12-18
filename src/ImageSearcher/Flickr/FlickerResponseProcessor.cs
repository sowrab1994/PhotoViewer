using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using ProductConfig;

namespace ImageSearcher.Flicker
{
    public class FlickerResponseProcessor
    {
        Rsp rspObj;

        private string FormImageUrl(Photo photoObj)
        {
            return Config.imageUrlPrefix + photoObj.Server + "/" + photoObj.Id + "_" + photoObj.Secret + ".jpg";
        }

        /// <summary>
        ///     Serializes the XML response to a class
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Gets total number of page results
        /// </summary>
        /// <returns></returns>
        public int GetResponsePages()
        {
            return rspObj?.Photos?.Pages?? 0;
        }

        /// <summary>
        ///      Gets the ArrayList of Images based on XML response
        /// </summary>
        /// <returns></returns>
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
