using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backender.Translator
{
    public static class WebHelper
    {
        public static bool Exists(Uri uri)
        {
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "HEAD";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode.ToString() == "OK")
                {
                    return true;
                }
                return false;
            }
            catch (WebException ex)
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }
    }
}
