﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser
{
    internal interface IBrowserFactory
    {
        IBrowser GetBrowser();
    }
}
