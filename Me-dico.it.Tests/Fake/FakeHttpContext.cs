using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Me_dico.it.Tests.Fake
{
    class FakeHttpContext : HttpContextBase
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
}
