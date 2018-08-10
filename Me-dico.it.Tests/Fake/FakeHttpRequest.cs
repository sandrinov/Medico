using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Me_dico.it.Tests.Fake
{
    class FakeHttpRequest : HttpRequestBase
    {
        public override string this[string Key]
        {
            get { return null; }
        }
        public override NameValueCollection Headers
        {
            get
            {
                return new NameValueCollection();
            }
        }
    }
}
