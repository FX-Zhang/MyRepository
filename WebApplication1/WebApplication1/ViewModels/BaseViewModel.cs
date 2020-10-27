using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class BaseViewModel                               //可封装布局页所需要的所有值
    {
        public string UserName { get; set; }
        public FooterViewModel FooterData { get; set; }
        
    }
}