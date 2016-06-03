using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    /// XML序列化与反序列化
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// 静态扩展
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns></returns>
        public static string SerializeToXmlStr<T>(this T obj, bool omitXmlDeclaration = false)
        {
            return XmlSerialize<T>(obj, omitXmlDeclaration);
        }
        #region XML序列化反序列化相关的静态方法
        /// <summary>
        /// 使用XmlSerializer序列化对象
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns>序列化后的字符串</returns>
        public static string XmlSerialize<T>(T obj, bool omitXmlDeclaration = false)
        {
            var stream = new MemoryStream();//var writer = new StringWriter();
            var xmlwriter = XmlWriter.Create(stream/*writer*/, new XmlWriterSettings { OmitXmlDeclaration = omitXmlDeclaration, Encoding = new System.Text.UTF8Encoding(false) });
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(String.Empty, String.Empty); //在XML序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
            var ser = new XmlSerializer(typeof(T));
            ser.Serialize(xmlwriter, obj, xmlns);

            return Encoding.UTF8.GetString(stream.ToArray());//writer.ToString();
        }
        /// <summary>
        /// 使用XmlSerializer序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <param name="removeDefaultNamespace">是否移除默认名称空间(如果对象定义时指定了:XmlRoot(Namespace = "http://www.xxx.com/xsd")则需要传false值进来)</param>
        /// <returns>序列化后的字符串</returns>
        public static void XmlSerialize<T>(string path, T obj, bool omitXmlDeclaration = false, bool removeDefaultNamespace = true)
        {
            using (var xmlwriter = XmlWriter.Create(path, new XmlWriterSettings { OmitXmlDeclaration = omitXmlDeclaration }))
            {
                var xmlns = new XmlSerializerNamespaces();
                if (removeDefaultNamespace)
                    xmlns.Add(String.Empty, String.Empty); //在XML序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(xmlwriter, obj, xmlns);
                xmlwriter.Flush();
            }
        }
        public static byte[] ShareReadFile(string filePath)
        {
            byte[] bytes;
            //避免文件被同时操作造成异常 共享锁 flieShare必须为ReadWrite 
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                bytes = new byte[fs.Length];
                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);
                    if (n == 0)
                        break;
                    numBytesRead += n;
                    numBytesToRead -= n;
                }
            }
            return bytes;
        }

        public static XmlDocument ShareReadFileXml(string path, XmlDocument doc = null)
        {
            byte[] bytes = ShareReadFile(path);
            if (bytes.Length < 1) //当文件正在被写入数据时，可能读出为0
                for (int i = 0; i < 5; i++)
                {
                    bytes = ShareReadFile(path);
                    if (bytes.Length > 0)
                        break;
                    System.Threading.Thread.Sleep(50);
                }
            if (doc == null)
                doc = new XmlDocument();
            doc.Load(new MemoryStream(bytes));
            return doc;
        }

        /// <summary>
        /// 文件反序列化
        /// </summary>
        /// <typeparam name="T">返回的对象类型</typeparam>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static T XmlFileDeserialize<T>(string path)
        {
            var doc = ShareReadFileXml(path);
            if (doc.DocumentElement != null)
                return (T) new XmlSerializer(typeof (T)).Deserialize(new XmlNodeReader(doc.DocumentElement));
            return default(T);

            //using (XmlReader xmlReader = XmlReader.Create(path, new XmlReaderSettings() {CloseInput = true}))
            //{
            //    var obj = (T) new XmlSerializer(typeof (T)).Deserialize(xmlReader);
            //    return obj;
            //}
        }

        /// <summary>
        /// 使用XmlSerializer反序列化对象
        /// </summary>
        /// <param name="xmlStr">需要反序列化的xml字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T XmlDeserialize<T>(string xmlStr) where T : class
        {
            using (var xmlReader = XmlReader.Create(new StringReader(xmlStr), new XmlReaderSettings()))
            {
                return (T) new XmlSerializer(typeof (T)).Deserialize(xmlReader);
            }
        }

        #endregion
    }
}
