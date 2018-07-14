using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Tests.Fakes
{
    class FakeControllerContext : ControllerContext
    {
        HttpContextBase _context = new FakeHttpContext();
        public override HttpContextBase HttpContext
        { get => _context; set => _context = value; }
    }

    internal class FakeHttpContext : HttpContextBase
    {
        HttpRequestBase _request = new FakeHttpRequest();
        public override HttpRequestBase Request
        {
            get
            {
                return _request;
            }
        }
        
        
    }

    internal class FakeHttpRequest : HttpRequestBase
    {
        public override string this[string key] { get => null; }
        public override NameValueCollection Headers { get => new NameValueCollection();}
    }
}
