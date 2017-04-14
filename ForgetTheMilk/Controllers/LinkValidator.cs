using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ForgetTheMilk.Controllers
{
    public class LinkValidator : ILinkValidator
    {
        public void Validate(string link)
        {
            try
            {
                var request = WebRequest.CreateHttp(link);
                request.Method = "HEAD";
                request.GetResponse();
            }
            catch (WebException ex)
            {
                throw new ApplicationException($"Invalid Link {link}");
            }
        }
    }
}
