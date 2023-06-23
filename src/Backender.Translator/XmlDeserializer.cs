using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace Backender.Translator
{
    public class XmlDeserializer
    {
        public async static Task<XmlDocument> GetXmlDocumentAsync(string FileName)
        {
            Uri uriResult;
            bool IsUrl = Uri.TryCreate(FileName, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (IsUrl)
            {
                return await GetXmlDocumentFromUriAsync(uriResult);
            }
            else
            {
                return await GetXmlDocumentFromPhysicalFileAsync(FileName);
            }
        }

        public static XmlDocument GetXmlDocument(string FileName)
        {

                Uri uriResult;
                bool IsUrl = Uri.TryCreate(FileName, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (IsUrl)
                {
                    return GetXmlDocumentFromUri(uriResult);
                }
                else
                {
                    return GetXmlDocumentFromPhysicalFile(FileName);
                }

        }

        public static Blueprint ConvertXmlToBlueprint(XmlDocument xmlDocument)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Blueprint));
            using (XmlNodeReader reader = new XmlNodeReader(xmlDocument.DocumentElement))
            {
                return (Blueprint)serializer.Deserialize(reader);
            }
        }

        private async static Task<XmlDocument> GetXmlDocumentFromPhysicalFileAsync(string FileName)
        {
            var xmlDocument = new XmlDocument();
            var xmlContent = await System.IO.File.ReadAllTextAsync(FileName);
            if (Path.GetExtension(FileName).ToLower() == ".xml")
            {
                xmlDocument.LoadXml(xmlContent);
            }
            return xmlDocument;
        }

        private async static Task<XmlDocument> GetXmlDocumentFromUriAsync(Uri FileUri)
        {
            var xmlDocument = new XmlDocument();
            using (WebClient webClient = new WebClient())
            {
                var xmlContent = await webClient.DownloadStringTaskAsync(FileUri);
                if (Path.GetExtension(FileUri.AbsolutePath).ToLower() == ".xml")
                {
                    xmlDocument.LoadXml(xmlContent);
                }
            }
            return xmlDocument;
        }

        private static XmlDocument GetXmlDocumentFromPhysicalFile(string FileName)
        {
            var xmlDocument = new XmlDocument();
            var xmlContent = System.IO.File.ReadAllText(FileName);
            if (Path.GetExtension(FileName).ToLower() == ".xml")
            {
                xmlDocument.LoadXml(xmlContent);
            }
            else
            {
                throw new InvalidDataException("Invalid Extension: File must be a xml!");
            }
            return xmlDocument;
        }

        private static XmlDocument GetXmlDocumentFromUri(Uri FileUri)
        {
            var xmlDocument = new XmlDocument();
            using (WebClient webClient = new WebClient())
            {
                var xmlContent = webClient.DownloadString(FileUri);
                if (Path.GetExtension(FileUri.AbsolutePath).ToLower() == ".xml")
                {
                    xmlDocument.LoadXml(xmlContent);
                }
            }
            return xmlDocument;
        }

    }
}
