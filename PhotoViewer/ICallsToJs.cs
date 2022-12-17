using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer
{
    internal interface ICallsToJs
    {
        void LoadImages(ArrayList images);

        void ShowLoading();

        void ShowNoInternet();

        void ShowNoImages();
    }
}
