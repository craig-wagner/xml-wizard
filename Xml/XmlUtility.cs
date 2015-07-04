namespace Wagner.Xml
{
    #region using
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    #endregion

    /// <summary>
    /// Provides a place to put generic functionality related to XML documents. 
    /// </summary>
    public class XmlUtility
    {
        #region Public Static Methods
        /// <summary>
        /// Formats a string of XML with indenting. If the string is already
        /// formatted this will essentially "double format" it so you may want
        /// to call PackXml first to ensure no extra formatting (whitespace) 
        /// exists in the document.
        /// </summary>
        /// <param name="xml">
        /// The string of XML to be formatted.
        /// </param>
        /// <returns>
        /// A string of XML with line breaks and indentation.
        /// </returns>
        public static string FormatXml( string xml )
        {
            XmlTextReader reader = null;
            XmlTextWriter writer = null;

            try
            {
                // Create a StringWriter to hold the formatted XML string
                StringWriter sw = new StringWriter();
                // Create XmlTextReader and XmlTextWriter to handle input and
                // output
                reader = new XmlTextReader( xml, XmlNodeType.Document, null );
                writer = new XmlTextWriter( sw );
                // Make sure the XmlTextWriter will format the data with indents,
                // the whole point of this exercise
                writer.Formatting = Formatting.Indented;

                // This loop should only execute once as we're at the root node of
                // the document, but just to be on the safe side we'll do a loop
                while( !reader.EOF )
                    writer.WriteNode( reader, true );
            
                // Send back the formatted string
                return sw.ToString();
            }
            finally
            {
                // Clean up
                if( reader != null )
                    reader.Close();

                if( writer != null )
                    writer.Close();
            }
        }

        /// <summary>
        /// Removes all carriage returns, line feeds and extra whitespace from
        /// a string of XML.
        /// </summary>
        /// <param name="xml">
        /// The XML string to be packed.
        /// </param>
        /// <returns>
        /// A string of XML with all carraige returns, line feeds and extra
        /// whitespace removed.
        /// </returns>
        public static string PackXml( string xml )
        {
            string xmlToReturn = xml;

            try
            {
                // Load the XML string into an XmlDocument and extract it back
                // out. This has the effect of removing all non-significant
                // whitespace. Whitespace in the data will be retained.
                XmlDocument packedXml = new XmlDocument();
                packedXml.LoadXml( xml );
                
                xmlToReturn = packedXml.OuterXml;
            }
            catch {}

            return xmlToReturn;
        }

        /// <summary>
        /// Remove the empty nodes from an XmlDocument. This does a
        /// depth-first traversal of the document to ensure nodes that only
        /// contain other empty nodes are also removed.
        /// </summary>
        /// <param name="doc">
        /// The XmlDocument from which to prune the empty nodes. The original
        /// document is modified.
        /// </param>
        public static void PruneEmptyNodes( XmlDocument doc )
        {
            XmlNode root = doc.DocumentElement;

            Prune( root );
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Given the parentNode passed in to this method, a list of child
        /// nodes is obtained and the method is called recursively for each
        /// child. If the parentNode has no children it is removed.
        /// </summary>
        /// <param name="parentNode">
        /// The node to be removed if it has no children.
        /// </param>
        private static void Prune( XmlNode parentNode )
        {
            XmlNode[] nodeList = new XmlNode[parentNode.ChildNodes.Count];
            int i = 0;

            // "freeze" the list of child nodes.  When I start deleting
            // empty child nodes, the parentNode.ChildNodes collection
            // attribute appears to get rearranged. That makes walking
            // through a list of child nodes that we're also in the
            // process of deleting a little tricky.
            foreach( XmlNode node in parentNode.ChildNodes )
                nodeList[i++] = node;

            // Now walk through the "frozen" list of child nodes and
            // get rid of any that are empty.
            for( i = nodeList.Length - 1; i >= 0; i-- )
            {
                if( nodeList[i].HasChildNodes )
                    Prune( nodeList[i] );

                if( nodeList[i].InnerText.Length == 0 )
                    parentNode.RemoveChild( nodeList[i] );
            }
        }
        #endregion
    }
}
