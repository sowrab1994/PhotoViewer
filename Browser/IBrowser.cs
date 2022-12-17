using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Browser
{
    public interface IBrowser
    {
        void LoadWebPage(string webpage);

        void InvokeScriptAsync(string methodName, params object[] args);

        UIElement GetUIElement { get; }
    }
}
