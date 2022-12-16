using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher
{
    public interface ImageSearcher
    {
        void SetApiKey(int key);
        void SetPage(int page);
        ArrayList GetImagesUrl(string text);
    }
}
