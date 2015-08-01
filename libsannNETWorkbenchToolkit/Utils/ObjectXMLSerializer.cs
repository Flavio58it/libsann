// Code from: http://www.codeproject.com/Articles/4491/Load-and-save-objects-to-XML-using-serialization
using System;
using System.Xml.Serialization;  // For serialization of an object to an XML Document file.
using System.Runtime.Serialization.Formatters.Binary; // For serialization of an object to an XML Binary file.
using System.IO;                 // For reading/writing data to an XML file.
using System.IO.IsolatedStorage; // For accessing user isolated data.

namespace libsannNETWorkbenchToolkit.Utils
{
    /// <summary>
    /// Serialization format types.
    /// </summary>
    public enum SerializedFormat
    {
        /// <summary>
        /// Binary serialization format.
        /// </summary>
        Binary,

        /// <summary>
        /// Document serialization format.
        /// </summary>
        Document
    }

    /// <summary>
    /// Facade to XML serialization and deserialization of an object to/from an XML file.
    /// 
    /// References: XML Serialization at http://samples.gotdotnet.com/:
    /// http://samples.gotdotnet.com/QuickStart/howto/default.aspx?url=/quickstart/howto/doc/xmlserialization/rwobjfromxml.aspx
    /// </summary>
    public class ObjectXMLSerializer
    {
        /// <summary>
        /// Private serializer to prevent instanciation for 'static' class.
        /// </summary>
        private ObjectXMLSerializer()
        {

        }

        #region Load methods

        /// <summary>
        /// Loads an object from an XML file in Document format.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, @"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static Object Load(Object serializableObject, string path)
        {
            serializableObject = LoadFromDocumentFormat(serializableObject, null, path, null);
            return serializableObject;
        }

        /// <summary>
        /// Loads an object from an XML file using a specified serialized format.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, @"C:\XMLObjects.xml", SerializedFormat.Binary);
        /// </code>
        /// </example>        
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <param name="serializedFormat">XML serialized format used to load the object.</param>
        /// <returns>Object loaded from an XML file using the specified serialized format.</returns>
        public static Object Load(Object serializableObject, string path, SerializedFormat serializedFormat)
        {
            switch(serializedFormat)
            {
                case SerializedFormat.Binary:
                    serializableObject = LoadFromBinaryFormat(serializableObject, path, null);
                    break;

                case SerializedFormat.Document:
                default:
                    serializableObject = LoadFromDocumentFormat(serializableObject, null, path, null);
                    break;
            }

            return serializableObject;
        }

        /// <summary>
        /// Loads an object from an XML file in Document format, supplying extra data types to enable deserialization of custom types within the object.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, @"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <param name="extraTypes">Extra data types to enable deserialization of custom types within the object.</param>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static Object Load(Object serializableObject, string path, System.Type[] extraTypes)
        {
            serializableObject = LoadFromDocumentFormat(serializableObject, extraTypes, path, null);
            return serializableObject;
        }

        /// <summary>
        /// Loads an object from an XML file in Document format, located in a specified isolated storage area.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly());
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to load the object from.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to load the object from.</param>
        /// <returns>Object loaded from an XML file in Document format located in a specified isolated storage area.</returns>
        public static Object Load(Object serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            serializableObject = LoadFromDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
            return serializableObject;
        }

        /// <summary>
        /// Loads an object from an XML file located in a specified isolated storage area, using a specified serialized format.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), SerializedFormat.Binary);
        /// </code>
        /// </example>        
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to load the object from.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to load the object from.</param>
        /// <param name="serializedFormat">XML serialized format used to load the object.</param>        
        /// <returns>Object loaded from an XML file located in a specified isolated storage area, using a specified serialized format.</returns>
        public static Object Load(Object serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory, SerializedFormat serializedFormat)
        {
            switch(serializedFormat)
            {
                case SerializedFormat.Binary:
                    serializableObject = LoadFromBinaryFormat(serializableObject, fileName, isolatedStorageDirectory);
                    break;

                case SerializedFormat.Document:
                default:
                    serializableObject = LoadFromDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
                    break;
            }

            return serializableObject;
        }

        /// <summary>
        /// Loads an object from an XML file in Document format, located in a specified isolated storage area, and supplying extra data types to enable deserialization of custom types within the object.
        /// </summary>
        /// <example>
        /// <code>
        /// // Always create a new object prior to passing to ObjectXMLSerializer.Load method.
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// serializableObject = (SerializableObject)ObjectXMLSerializer.Load(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>        
        /// <param name="serializableObject">Serializable object to be loaded from file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to load the object from.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to load the object from.</param>
        /// <param name="extraTypes">Extra data types to enable deserialization of custom types within the object.</param>
        /// <returns>Object loaded from an XML file located in a specified isolated storage area, using a specified serialized format.</returns>
        public static Object Load(Object serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory, System.Type[] extraTypes)
        {
            serializableObject = LoadFromDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
            return serializableObject;
        }

        #endregion

        #region Save methods

        /// <summary>
        /// Saves an object to an XML file in Document format.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, @"C:\XMLObjects.xml");
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        public static void Save(Object serializableObject, string path)
        {
            SaveToDocumentFormat(serializableObject, null, path, null);
        }

        /// <summary>
        /// Saves an object to an XML file using a specified serialized format.
        /// </summary>
        /// <example>
        /// <code>
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, @"C:\XMLObjects.xml", SerializedFormat.Binary);
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        /// <param name="serializedFormat">XML serialized format used to save the object.</param>
        public static void Save(Object serializableObject, string path, SerializedFormat serializedFormat)
        {
            switch(serializedFormat)
            {
                case SerializedFormat.Binary:
                    SaveToBinaryFormat(serializableObject, path, null);
                    break;

                case SerializedFormat.Document:
                default:
                    SaveToDocumentFormat(serializableObject, null, path, null);
                    break;
            }
        }

        /// <summary>
        /// Saves an object to an XML file in Document format, supplying extra data types to enable serialization of custom types within the object.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, @"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        /// <param name="extraTypes">Extra data types to enable serialization of custom types within the object.</param>
        public static void Save(Object serializableObject, string path, System.Type[] extraTypes)
        {
            SaveToDocumentFormat(serializableObject, extraTypes, path, null);
        }

        /// <summary>
        /// Saves an object to an XML file in Document format, located in a specified isolated storage area.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly());
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to save the object to.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to save the object to.</param>
        public static void Save(Object serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

        /// <summary>
        /// Saves an object to an XML file located in a specified isolated storage area, using a specified serialized format.
        /// </summary>
        /// <example>
        /// <code>        
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), SerializedFormat.Binary);
        /// </code>
        /// </example>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to save the object to.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to save the object to.</param>
        /// <param name="serializedFormat">XML serialized format used to save the object.</param>        
        public static void Save(Object serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory, SerializedFormat serializedFormat)
        {
            switch(serializedFormat)
            {
                case SerializedFormat.Binary:
                    SaveToBinaryFormat(serializableObject, fileName, isolatedStorageDirectory);
                    break;

                case SerializedFormat.Document:
                default:
                    SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
                    break;
            }
        }

        /// <summary>
        /// Saves an object to an XML file in Document format, located in a specified isolated storage area, and supplying extra data types to enable serialization of custom types within the object.
        /// </summary>
        /// <example>
        /// <code>
        /// SerializableObject serializableObject = new SerializableObject();
        /// 
        /// ObjectXMLSerializer.Save(serializableObject, "XMLObjects.xml", IsolatedStorageFile.GetUserStoreForAssembly(), new Type[] { typeof(MyCustomType) });
        /// </code>
        /// </example>        
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="fileName">Name of the file in the isolated storage area to save the object to.</param>
        /// <param name="isolatedStorageDirectory">Isolated storage area directory containing the XML file to save the object to.</param>
        /// <param name="extraTypes">Extra data types to enable serialization of custom types within the object.</param>
        public static void Save(Object serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory, System.Type[] extraTypes)
        {
            SaveToDocumentFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

        #endregion

        #region Private

        private static FileStream CreateFileStream(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            FileStream fileStream = null;

            if(isolatedStorageFolder == null)
                fileStream = new FileStream(path, FileMode.OpenOrCreate);
            else
                fileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder);

            return fileStream;
        }

        private static Object LoadFromBinaryFormat(Object serializableObject, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using(FileStream fileStream = CreateFileStream(isolatedStorageFolder, path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                serializableObject = binaryFormatter.Deserialize(fileStream);
            }

            return serializableObject;
        }

        private static Object LoadFromDocumentFormat(Object serializableObject, System.Type[] extraTypes, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using(TextReader textReader = CreateTextReader(isolatedStorageFolder, path))
            {
                XmlSerializer xmlSerializer = CreateXmlSerializer(serializableObject, extraTypes);
                serializableObject = xmlSerializer.Deserialize(textReader);
            }

            return serializableObject;
        }

        private static TextReader CreateTextReader(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextReader textReader = null;

            if(isolatedStorageFolder == null)
                textReader = new StreamReader(path);
            else
                textReader = new StreamReader(new IsolatedStorageFileStream(path, FileMode.Open, isolatedStorageFolder));

            return textReader;
        }

        private static TextWriter CreateTextWriter(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            TextWriter textWriter = null;

            if(isolatedStorageFolder == null)
                textWriter = new StreamWriter(path);
            else
                textWriter = new StreamWriter(new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder));

            return textWriter;
        }

        private static XmlSerializer CreateXmlSerializer(Object serializableObject, System.Type[] extraTypes)
        {
            Type ObjectType = serializableObject.GetType();

            XmlSerializer xmlSerializer = null;

            if(extraTypes != null)
                xmlSerializer = new XmlSerializer(ObjectType, extraTypes);
            else
                xmlSerializer = new XmlSerializer(ObjectType);

            return xmlSerializer;
        }

        private static void SaveToDocumentFormat(Object serializableObject, System.Type[] extraTypes, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using(TextWriter textWriter = CreateTextWriter(isolatedStorageFolder, path))
            {
                XmlSerializer xmlSerializer = CreateXmlSerializer(serializableObject, extraTypes);
                xmlSerializer.Serialize(textWriter, serializableObject);
            }
        }

        private static void SaveToBinaryFormat(Object serializableObject, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using(FileStream fileStream = CreateFileStream(isolatedStorageFolder, path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, serializableObject);
            }
        }

        #endregion
    }
}