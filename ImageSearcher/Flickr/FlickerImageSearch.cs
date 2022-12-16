using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher
{
    internal class FlickerImageSearch : ImageSearcher
    {
        private string apiKey = "c098ab4dc93e9a203a007ad613d1c414";
        private string urlPrefix = "https://www.flickr.com/services/rest/?";
        private string methodName = "flickr.photos.search";
        private string searchText;
        private string url;
        private Dictionary<string, string> urlQueryDict  = new Dictionary<string, string>();

        FlickerImageSearch() 
        {
            url = url + urlPrefix + "method" + "=" + methodName;
        }

        private string MergeKeyValue(string key, string val)
        {
            return "&" + key + "=" + val;
        }

        private void FormUrl()
        {
            foreach (var ele in urlQueryDict)
            {
                url += MergeKeyValue(ele.Key, ele.Value);
            }
        }

       
        public ArrayList GetImagesUrl(string text)
        {
            return new ArrayList();
        }

        public void SetApiKey(int key)
        {

        }

        public void SetPage(int page)
        {
            if (urlQueryDict.ContainsKey("page"))
            {
                urlQueryDict["page"] = page.ToString();
            }
            else
            {
                urlQueryDict.Add("page", page.ToString());
            }
        }
    }
}
