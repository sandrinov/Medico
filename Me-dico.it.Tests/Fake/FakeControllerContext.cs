using System.Web;
using System.Web.Mvc;

namespace Me_dico.it.Tests.Fake
{
    class FakeControllerContext : ControllerContext
    {
        HttpContextBase _context = new FakeHttpContext();

        public override HttpContextBase HttpContext
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }
    }
}
