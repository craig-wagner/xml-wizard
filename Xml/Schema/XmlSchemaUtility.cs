#region using
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
#endregion

namespace Wagner.Xml.Schema
{
    /// <summary>
	/// Provides a place to put generic functionality related to the 
	/// manipulation of XML and XML Schemas.
	/// </summary>
	public class XmlSchemaUtility
	{
        #region Constants
        private const int initialCapacity = 1024 * 5;
        #endregion

        #region Fields
        private XmlSchema schema;
        private XmlTextWriter xmlTextWriter;
        private bool includeSampleData;
        private bool includeChoiceComments;
        private StringBuilder xmlText;
        private bool validationSuccess;
        private StringBuilder validationMessage;
        private ArrayList elementNames;
        private Stack itemsBeingExpanded;
        private int choice;
        private Random random;
        /// <summary>
        /// XslTransform object used by this instance. Only created once per
        /// transformation document.
        /// </summary>
        private XslTransform xslTransform;
        private string xsltStylesheet;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the XSLT stylesheet that the instance will use to
        /// perform transforms when the TransformXmlDoc method is called.
        /// </summary>
        public string XsltStylesheet
        {
            get { return xsltStylesheet; }
            set
            {
                if( xsltStylesheet != value )
                {
                    xslTransform = null;
                    xsltStylesheet = value; 
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Populate an array containing the names of all the root elements in
        /// the provided schema.
        /// </summary>
        /// <param name="schema">
        /// An XmlSchema object for which you want the root element names.
        /// </param>
        /// <returns>
        /// Returns a string array containing all the root element names in the
        /// schema.
        /// </returns>
        public string[] GetRootNodeNames( XmlSchema schema )
        {
            string [] elementNames = null;

            if( !schema.IsCompiled )
                schema.Compile( null );

            elementNames = new string[schema.Elements.Values.Count];

            int i = 0;

            foreach( XmlSchemaElement element in schema.Elements.Values )
                elementNames[i++] = element.QualifiedName.Name;

            return elementNames;
        }

        /// <summary>
        /// Generates an empty XML document. The only root node present will be
        /// the one specified by the rootNodeName argument.
        /// </summary>
        /// <remarks>
        /// There is a limitation to this method. If the schema contains
        /// "choice" elements this method will treat them as a sequence
        /// and generate a node for each option. This implementation was
        /// chosen due to the difficulty in trying to specify which node
        /// in a "choice" structure the developer may want.
        /// </remarks>
        /// <param name="schema">
        /// Schema to use as the template for generating the empty document.
        /// </param>
        /// <param name="rootNodeName">
        /// The name of the root node for which to generate the empty XML
        /// document.
        /// </param>
        /// <returns>
        /// Returns a string containing the XML document.
        /// </returns>
        public string GetXmlString( XmlSchema schema, string rootNodeName )
        {
            return GetXmlString( schema, rootNodeName, false, false );
        }

        /// <summary>
        /// Generates an XML document and optionally provides sample data for 
        /// each element. The XML document will only contain a single root node
        /// as specified in the rootNodeName argument. The caller can specify
        /// whether the document should contain comments to indicate where the
        /// choice elements were expanded.
        /// </summary>
        /// <remarks>
        /// There is a limitation to this method. If the schema contains
        /// "choice" elements this method will treat them as a sequence
        /// and generate a node for each option. This implementation was
        /// chosen due to the difficulty in trying to specify which node
        /// in a "choice" structure the developer may want.
        /// </remarks>
        /// <param name="schema">
        /// Schema to use as the template for generating the empty document.
        /// </param>
        /// <param name="rootNodeName">
        /// The name of the root node for which to generate the empty XML
        /// document.
        /// </param>
        /// <param name="includeSampleData">
        /// Boolean indicating whether the caller wants sample data generated 
        /// for each element.
        /// </param>
        /// <param name="includeChoiceComments">
        /// Boolean indicating whether the caller wants comments generated 
        /// for each choice element in the schema.
        /// </param>
        /// <returns>
        /// Returns a string containing the XML document.
        /// </returns>
        public string GetXmlString( 
            XmlSchema schema, string rootNodeName, bool includeSampleData, bool includeChoiceComments )
        {
            GetXmlStringSetup( schema, includeSampleData, includeChoiceComments );

            foreach( XmlSchemaElement element in schema.Elements.Values )
            {
                if( element.QualifiedName.Name == rootNodeName )
                {
                    AddElement( element );
                    break;
                }
            }

            return xmlText.ToString();
        }

        /// <summary>
        /// Generates an empty XML document using the supplied schema. The
        /// empty XML document may contain multiple root nodes if the schema
        /// contains several global elements, complex types or groups.
        /// </summary>
        /// <remarks>
        /// There is a limitation to this method. If the schema contains
        /// "choice" elements this method will treat them as a sequence
        /// and generate a node for each option. This implementation was
        /// chosen due to the difficulty in trying to specify which node
        /// in a "choice" structure the developer may want.
        /// </remarks>
        /// <param name="schema">
        /// Schema to use as the template for generating the empty document.
        /// </param>
        /// <returns>
        /// Returns a string containing the XML document.
        /// </returns>
        public string GetXmlString( XmlSchema schema )
        {
            return GetXmlString( schema, false, false );
        }

        /// <summary>
        /// Generates an XML document and optionally provides sample data for 
        /// each element. The XML document may contain multiple root nodes if
        /// the schema contains several global elements, complex types or 
        /// groups. The caller can specify whether the document should contain
        /// comments to indicate where the choice elements were expanded.
        /// </summary>
        /// <remarks>
        /// There is a limitation to this method. If the schema contains
        /// "choice" elements this method will treat them as a sequence
        /// and generate a node for each option. This implementation was
        /// chosen due to the difficulty in trying to specify which node
        /// in a "choice" structure the developer may want.
        /// </remarks>
        /// <param name="schema">
        /// Schema to use as the template for generating the empty document.
        /// </param>
        /// <param name="includeSampleData">
        /// Boolean indicating whether the caller wants sample data generated 
        /// for each element.
        /// </param>
        /// <param name="includeChoiceComments">
        /// Boolean indicating whether the caller wants comments generated 
        /// for each choice element in the schema.
        /// </param>
        /// <returns>
        /// Returns a string containing the XML document.
        /// </returns>
        public string GetXmlString( 
            XmlSchema schema, bool includeSampleData, bool includeChoiceComments )
        {
            GetXmlStringSetup( schema, includeSampleData, includeChoiceComments );

            foreach( XmlSchemaElement element in schema.Elements.Values )
                AddElement( element );

            return xmlText.ToString();
        }

        /// <summary>
        /// Runs a transform against the incoming XML. The transformation 
        /// document must have been previously set using this object's 
        /// XsltStylesheet property, otherwise this method will throw an 
        /// exception.
        /// </summary>
        /// <param name="inputXml">
        /// The XML document to transform.
        /// </param>
        /// <returns>
        /// StringWriter containing the transformed document.
        /// </returns>
        public StringWriter TransformXmlDoc( string inputXml )
        {
            StringWriter xslOutput;
            XmlUrlResolver urlResolver = null;
			
            if( xsltStylesheet.Length == 0 )
                throw new ApplicationException( "Must set XsltStylesheet property before calling TransformXmlDoc." );

            // Check to see if the Xsl transform object needs creating
            if( xslTransform == null )
            {
                xslTransform = new XslTransform();
                // Load the style sheet. Note this has to be loaded from 
                // the database.
                urlResolver = new XmlUrlResolver();

                StringReader xslReader = new StringReader( xsltStylesheet );
                XPathDocument xslToProcess = new XPathDocument( xslReader );
                // Create an XPathNavigator to use in the Xsl transform
                XPathNavigator xslNavigator = xslToProcess.CreateNavigator();
                xslTransform.Load( xslNavigator, urlResolver, null );
            }				

            XPathDocument xmlToProcess = new XPathDocument( new StringReader( inputXml ) );
		
            // Create an XPathNavigator to use for optimized Xsl processing
            XPathNavigator xmlNavigator = xmlToProcess.CreateNavigator();

            // Create a stringWriter to hold the output
            xslOutput = new StringWriter();	
									
            // Do the transform by applying the transform to XML doc
            xslTransform.Transform( xmlNavigator, null, xslOutput, urlResolver );

            xslOutput.Close();

            return xslOutput;
        }

        /// <summary>
        /// Validate the XML string passed in the xmlData argument against the
        /// schema passed in the schema argument.
        /// </summary>
        /// <param name="xmlData">
        /// String containing an XML document to be validated against a
        /// schema.
        /// </param>
        /// <param name="schema">
        /// An XmlSchema object that has already been loaded with the
        /// necessary schema document.
        /// </param>
        public void ValidateXml( string xmlData, XmlSchema schema )
        {
            XmlValidatingReader validatingReader = null;

            InitializeClassMembers();

            // Create the namespace manager.
            XmlNamespaceManager namespaceManager = GetNamespaceManager( xmlData );

            // Create the validating reader from the incoming XML document
            // and namespace manager.
            validatingReader = GetValidatingReader( xmlData, namespaceManager );

            // Add the specified schema to the validating reader's schema
            // collection.
            validatingReader.Schemas.Add( schema );

            // Run the validating reader to validate the document against
            // the schema.
            RunValidation( validatingReader );
        }

        /// <summary>
        /// Validate an incoming string of XML. It is assumed the XML
        /// contains a reference to the schema against which to validate.
        /// </summary>
        /// <param name="xmlData">
        /// A string of XML that contains a reference to the schema
        /// against which to validate.
        /// </param>
        public void ValidateXml( string xmlData )
        {
            XmlValidatingReader validatingReader = null;

            InitializeClassMembers();

            // Create the validating reader from the incoming XML document.
            validatingReader = GetValidatingReader( xmlData, null );

            // Run the validating reader to validate the document against
            // the schema.
            RunValidation( validatingReader );
        }

        /// <summary>
        /// Validate the XML contained in the file referenced by xmlFilepath. 
        /// It is assumed the XML contains a reference to the schema against
        /// which to validate.
        /// </summary>
        /// <param name="xmlFilepath">
        /// The name of the file containing the XML to validate.
        /// </param>
        public void ValidateXmlFile( string xmlFilepath )
        {
            XmlValidatingReader validatingReader = null;
            XmlTextReader reader = null;

            InitializeClassMembers();

            // Create the validating reader from the incoming XML document.
            reader = new XmlTextReader( xmlFilepath );
            validatingReader = GetValidatingReader( reader );

            // Run the validating reader to validate the document against
            // the schema.
            RunValidation( validatingReader );
        }

        /// <summary>
        /// Validate the XML string passed in the xmlData argument against the
        /// schema passed in the schema argument.
        /// </summary>
        /// <param name="xmlData">
        /// String containing an XML document to be validated against a
        /// schema.
        /// </param>
        /// <param name="schema">
        /// An XmlSchema object that has already been loaded with the
        /// necessary schema document.
        /// </param>
        /// <param name="namespaceManager">
        /// An existing namespace manager object.
        /// </param>
        public void ValidateXml( string xmlData, XmlSchema schema, XmlNamespaceManager namespaceManager )
        {
            XmlValidatingReader validatingReader = null;

            InitializeClassMembers();

            // Create the validating reader from the incoming XML document
            // and namespace manager.
            validatingReader = GetValidatingReader( xmlData, namespaceManager );

            // Add the specified schema to the validating reader's schema
            // collection.
            validatingReader.Schemas.Add( schema );

            // Run the validating reader to validate the document against
            // the schema.
            RunValidation( validatingReader );
        }

        /// <summary>
        /// Given a URI to a schema and the contents of an XML document,
        /// perform a validation and report any errors.
        /// </summary>
        /// <param name="xmlData">
        /// String containing an XML document to be validated against a
        /// schema.
        /// </param>
        /// <param name="schemaText">
        /// The schema as a text string.
        /// </param>
        public void ValidateXml( string xmlData, string schemaText )
        {
            XmlValidatingReader validatingReader = null;

            InitializeClassMembers();

            // Create the namespace manager.
            XmlNamespaceManager namespaceManager = GetNamespaceManager( xmlData );

            XmlSchema schema = CreateXmlSchema( schemaText );

            // Create the validating reader from the incoming XML document
            // and the namespace manager we created from the document.
            validatingReader = GetValidatingReader( xmlData, namespaceManager );
            validatingReader.Schemas.Add( schema );

            // Run the validating reader to validate the document against
            // the schema.
            RunValidation( validatingReader );
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Helper method to turn a string containing an XML Schema into an
        /// XmlSchema object to be used for validating an XML document.
        /// </summary>
        /// <param name="schemaText">
        /// Text string containing a schema.
        /// </param>
        private XmlSchema CreateXmlSchema( string schemaText )
        {
            // Initialize status variables and create a buffer for the validation errors
            validationMessage = new StringBuilder( initialCapacity );
            validationSuccess = true;

            // Check to make sure all required arguments contain a value
            if( schemaText.Length == 0 || schemaText == null )
                throw new ArgumentException( "Argument cannot be null or empty", "schemaText" );

            // Parse the text version of the schema into an XmlSchema object
            StringReader stringReader = new StringReader( schemaText );
            XmlSchema xmlSchema = XmlSchema.Read( stringReader, null );
            xmlSchema.Compile( new ValidationEventHandler( ValidationErrorLogger ) );

            // Validation is finished but unsuccessful, so take the error message that was built up in
            // ValidationErrorLogger and throw a new exception with that message.
            if( !validationSuccess )
                throw new ApplicationException( validationMessage.ToString() );

            return xmlSchema;
        }

        /// <summary>
        /// Helper method that contains code common to all versions of 
        /// GetXmlString.
        /// </summary>
        /// <param name="schema">
        /// The schema used to generate the XML document.
        /// </param>
        /// <param name="includeSampleData">
        /// Boolean indicating whether the caller wants sample data generated 
        /// for each element.
        /// </param>
        /// <param name="includeChoiceComments">
        /// Boolean indicating whether the caller wants comments generated 
        /// for each choice element in the schema.
        /// </param>
        private void GetXmlStringSetup( XmlSchema schema, bool includeSampleData, bool includeChoiceComments )
        {
            // Store the values in instance variables to be used later in the 
            // processing.
            this.includeSampleData = includeSampleData;
            this.includeChoiceComments = includeChoiceComments;
            this.schema = schema;

            // If the caller wants sample data then create a random number 
            // generator.
            if( includeSampleData )
                random = new Random();

            // Make sure the schema has been compiled.
            if( !schema.IsCompiled )
                schema.Compile( null );

            // Create the XmlTextWriter to hold the output.
            xmlText = new StringBuilder( initialCapacity );
            xmlTextWriter = new XmlTextWriter( new StringWriter( xmlText ) );
            xmlTextWriter.Formatting = Formatting.Indented;

            // Create a stack used to ensure we don't end up in an infinite
            // loop due to recurive complex types or groups in the schema.
            itemsBeingExpanded = new Stack();
        }

        /// <summary>
        /// Take an XML document in string form and generate a namespace
        /// manager for its NameTable.
        /// </summary>
        /// <param name="xmlData">
        /// An XML document as a string.
        /// </param>
        /// <returns>
        /// An XmlNamespaceManager object.
        /// </returns>
        private XmlNamespaceManager GetNamespaceManager( string xmlData )
        {
            // Load the XML string into an XmlDocument so we can extract
            // the NameTable and use it to create a namespace manager.
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml( xmlData );

            // Create the namespace manager.
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager( xmlDocument.NameTable );
            namespaceManager.AddNamespace( "ns", xmlDocument.DocumentElement.NamespaceURI );

            return namespaceManager;
        }

        /// <summary>
        /// Validate the document identified by the validatingReader. If a 
        /// validation error occurs this method will throw an exception with
        /// the body of the exception being the list of validation error 
        /// messages that were generated.
        /// </summary>
        /// <param name="validatingReader">
        /// An XmlValidatingReader that has been set up with an XML document
        /// and a schema collection.
        /// </param>
        private void RunValidation( XmlValidatingReader validatingReader )
        {
            try
            {
                // Validate the Xml by reading it into the validating reader. If 
                // an error occurs during this process, the delegate assigned 
                // previously will be called once for each error encountered.
                while( validatingReader.Read() );

                // Validation is finished but unsuccessful, so take the error 
                // message that was built up in ValidationErrorLogger and throw 
                // a new exception with that message.
                if( !validationSuccess )
                    throw new InvalidElementValueException( validationMessage.ToString(), (string [])elementNames.ToArray( typeof( String ) ) );
            }
            finally
            {
                // If the validatingReader got created, close it as a final 
                // clean-up step
                if( validatingReader != null )
                    validatingReader.Close();
            }
        }

        /// <summary>
        /// Initialize status variables and create a buffer for the validation 
        /// errors
        /// </summary>
        private void InitializeClassMembers()
        {
            elementNames = new ArrayList();
            validationMessage = new StringBuilder( initialCapacity );
            validationSuccess = true;
        }

        /// <summary>
        /// Create an XmlValidatingReader from a string of XML and an existing
        /// namespace manager.
        /// </summary>
        /// <param name="xmlData">
        /// The string of XML that needs to be validated.
        /// </param>
        /// <param name="namespaceManager">
        /// The namespace manager used to resolve references in xmlData.
        /// </param>
        /// <returns>
        /// An XmlValidatingReader ready to validate a document.
        /// </returns>
        private XmlValidatingReader GetValidatingReader( string xmlData, XmlNamespaceManager namespaceManager )
        {
            // Load the contents of the xmlDocument into an 
            // XmlValidatingReader. We need to go through an XmlTextReader
            // because the XmlValidatingReader does not currently support 
            // being created directly from an XmlDocument (although it 
            // should because the class hierarchy should allow 
            // initialization from an XmlNodeReader).
            XmlParserContext context = new XmlParserContext( null, namespaceManager, null, XmlSpace.None );
            XmlTextReader textReader = new XmlTextReader( xmlData, XmlNodeType.Document, null );

            return GetValidatingReader( textReader );
        }

        /// <summary>
        /// Create an XmlValidatingReader object from the XmlReader passed in.
        /// </summary>
        /// <param name="reader">
        /// An existing XmlReader object.
        /// </param>
        /// <returns>
        /// An XmlValidatingReader with the ValidationEventHandler callback
        /// set.
        /// </returns>
        private XmlValidatingReader GetValidatingReader( XmlReader reader )
        {
            XmlValidatingReader validatingReader = null;

            // Initialize the XmlValidatingReader from the XmlReader.
            validatingReader = new XmlValidatingReader( reader );

            // Finish initializing the XmlValidatingReader with a 
            // delegate to the error handling routine.
            validatingReader.ValidationEventHandler += new ValidationEventHandler( ValidationErrorLogger );

            return validatingReader;
        }

        /// <summary>
        /// This method gets called if there is an error while validating the 
        /// XML document. Should this method be called, we log that an error 
        /// occurred and append the message to a buffer to be used later when 
        /// throwing an exception or logging the error.
        /// </summary>
        private void ValidationErrorLogger( object sender, ValidationEventArgs e )
        {
            int textIndex;

            validationSuccess = false;

            // Append this error to the message
            validationMessage.Append( Environment.NewLine + e.Message );

            // Determine if the validation error was caused by a document that
            // has elements out of sequence (possibly due to a required element
            // being missing). This type of error indicates we cannot process
            // the document, so throw an exception and stop validating.
            textIndex = e.Message.IndexOf( "has invalid child element" );

            if( textIndex > -1 )
                throw new InvalidChildElementException( e.Message );

            // Determine if the validation error was caused by an element whose
            // value does not conform to the schema definition; if so, add the
            // name of the element to the list of bad elements
            textIndex = e.Message.IndexOf( "element has an invalid value according to its data type" );

            if( textIndex > -1 )
            {
                int startIndex = e.Message.IndexOf( "'" ) + 1;
                int endIndex = e.Message.IndexOf( "'", startIndex );

                string elementName = e.Message.Substring( startIndex, endIndex - startIndex );
                elementNames.Add( elementName );
            }
        }

        #region Document Generation Methods
        /// <summary>
        /// Adds an element to the XML document.
        /// </summary>
        /// <param name="element">
        /// Element to add to the XML document.
        /// </param>
        private void AddElement( XmlSchemaElement element )
        {
            xmlTextWriter.WriteStartElement( element.QualifiedName.Name, element.QualifiedName.Namespace );

            object type = element.ElementType;

            if( type is XmlSchemaComplexType || type is XmlSchemaGroupRef )
            {
                string itemName = ((XmlSchemaType)type).Name;
                if( itemName == null || itemName.Length == 0 )
                    itemName = element.QualifiedName.Name;

                if( !itemsBeingExpanded.Contains( itemName ) )
                {
                    itemsBeingExpanded.Push( itemName );
                    if( type is XmlSchemaComplexType )
                    {
                        XmlSchemaComplexType complexType = (XmlSchemaComplexType)type;
                        if( complexType.ContentModel != null )
                            AddContent( complexType );
                        else if( complexType.Particle != null )
                        {
                            AddAttributes( complexType.Attributes );
                            AddParticle( complexType.Particle );
                        }
                    }
                    else if( type is XmlSchemaGroupRef )
                    {
                        XmlSchemaGroupRef groupRef = (XmlSchemaGroupRef)type;
                        AddParticle( groupRef.Particle );
                    }
                    itemsBeingExpanded.Pop();
                }
                else
                {
                    xmlTextWriter.WriteComment( "*********************************************************" );
                    xmlTextWriter.WriteComment( "Potential Recursive Relationship Detected" );
                    xmlTextWriter.WriteComment( "This node will not be expanded any further to prevent an infinite loop." );
                    xmlTextWriter.WriteComment( "*********************************************************" );
                }
            }
            else if( type is XmlSchemaSimpleType || type is XmlSchemaDatatype )
            {
                if( includeSampleData )
                    AddSampleValue( element.ElementType );
            }

            xmlTextWriter.WriteEndElement();
        }

        /// <summary>
        /// Handle complexTypes that have a content model.
        /// </summary>
        /// <param name="complexType">
        /// The complex type containing the content model.
        /// </param>
        private void AddContent( XmlSchemaComplexType complexType )
        {
            XmlSchemaContent content = complexType.ContentModel.Content;

            if( content is XmlSchemaSimpleContentExtension )
            {
                AddAttributes( ((XmlSchemaSimpleContentExtension)content).Attributes );
                if( includeSampleData )
                    AddSampleValue( complexType.Datatype );
            }
            else if( content is XmlSchemaSimpleContentRestriction )
                throw new NotImplementedException( "XmlSchemaSimpleContentRestriction not yet implemented." );
            else if( content is XmlSchemaComplexContentExtension )
                AddParticle( complexType.ContentTypeParticle );
            else if( content is XmlSchemaComplexContentRestriction )
                throw new NotImplementedException( "XmlSchemaComplexContentRestriction not yet implemented." );
        }

        /// <summary>
        /// Iterates over the attributes in the collection and adds them one-by-one to
        /// the XML document.
        /// </summary>
        /// <param name="attributes">
        /// An XmlSchemaObjectCollection of attributes.
        /// </param>
        private void AddAttributes( XmlSchemaObjectCollection attributes )
        {
            foreach( object o in attributes )
            {
                if( o is XmlSchemaAttribute )
                    AddAttribute( (XmlSchemaAttribute)o );
                else
                {
                    XmlSchemaAttributeGroup group = (XmlSchemaAttributeGroup)schema.Groups[((XmlSchemaAttributeGroupRef)o).RefName];
                    AddAttributes( group.Attributes );
                }
            }
        }

        /// <summary>
        /// Adds an attribute to the XML document.
        /// </summary>
        /// <param name="attribute">
        /// Attribute to add to the XML document.
        /// </param>
        private void AddAttribute( XmlSchemaAttribute attribute )
        {
            xmlTextWriter.WriteStartAttribute( attribute.QualifiedName.Name, attribute.QualifiedName.Namespace );
            
            if( includeSampleData )
                AddSampleValue( attribute.AttributeType );

            xmlTextWriter.WriteEndAttribute();
        }

        /// <summary>
        /// If the particle is an element it is added to the XML document, if
        /// it is a choice or sequence this method is called recursively until 
        /// an element is found.
        /// </summary>
        /// <param name="particle">
        /// An XmlSchemaParticle representing an element, choice or sequence. 
        /// Other particle types are unsupported and will cause an exception 
        /// to be thrown.
        /// </param>
        private void AddParticle( XmlSchemaParticle particle )
        {
            if( particle != null )
            {
                if( particle is XmlSchemaElement )
                    AddElement( (XmlSchemaElement)particle );
                else if ( particle is XmlSchemaGroupRef )
                    AddParticle( ((XmlSchemaGroupRef)particle).Particle );
                else if( particle is XmlSchemaSequence )
                {
                    foreach( XmlSchemaParticle particle1 in ((XmlSchemaSequence)particle).Items )
                        AddParticle( particle1 );
                }
                else if( particle is XmlSchemaChoice )
                {
                    int i = 1;

                    if( includeChoiceComments )
                    {
                        choice++;
                        xmlTextWriter.WriteComment(
                            String.Format( "========== Choice Element {0}: Begin ==========", choice ) );
                    }

                    foreach( XmlSchemaParticle particle1 in ((XmlSchemaChoice)particle).Items )
                    {
                        if( includeChoiceComments )
                            xmlTextWriter.WriteComment(
                                String.Format( "========== Choice Element {0}: Option #{1} Begin ==========", choice, i ) );

                        AddParticle( particle1 );
                        
                        if( includeChoiceComments )
                        {
                            xmlTextWriter.WriteComment(
                                String.Format( "========== Choice Element {0}: Option #{1} End ==========", choice, i ) );
                            i++;
                        }
                    }
                    
                    if( includeChoiceComments )
                    {
                        xmlTextWriter.WriteComment(
                            String.Format( "========== Choice Element {0}: End ==========", choice ) );
                        choice--;
                    }
                }
                else
                    // For any other particle type that we don't handle let 
                    // someone know about it
                    throw new NotImplementedException( "Not implemented for this type: " + particle.ToString() );
            }
        }

        /// <summary>
        /// Based on the data type passed in, this method generates dummy 
        /// data for inclusion in the XML document.
        /// </summary>
        /// <param name="schemaType">
        /// A valid XSD data type.
        /// </param>
        private void AddSampleValue( object schemaType )
        {
            const string vowels = "aeiouy";
            const string consonants = "bcdfghjklmnpqrstvwxz";
            const int stringLength = 8;

            XmlSchemaDatatype datatype = (schemaType is XmlSchemaSimpleType) ? ((XmlSchemaSimpleType)schemaType).Datatype : (XmlSchemaDatatype)schemaType;

            if( datatype != null )
            {
                // Consult the XSD to CLR conversion table for the correct type mappings
                Type type = datatype.ValueType;

                if( type == typeof( bool ) )
                    xmlTextWriter.WriteString( "true" );
                else if( type == typeof( int ) || type == typeof( long ) )
                    xmlTextWriter.WriteString( random.Next().ToString() );
                else if( type == typeof( float ) || type == typeof( decimal ) )
                    xmlTextWriter.WriteString( ( (float)random.Next() / (float)random.Next() ).ToString( "#.00" ) );
                else if( type == typeof( XmlQualifiedName ) )
                    xmlTextWriter.WriteString( "qualified_name" + random.Next().ToString());
                else if( type == typeof( DateTime ) )
                    xmlTextWriter.WriteString( DateTime.Today.ToString( "yyyy-MM-dd" ) );
                else if( type == typeof( SByte ) )
                    xmlTextWriter.WriteString( random.Next( 127 ).ToString() );
                else if( type == typeof( Byte ) )
                    xmlTextWriter.WriteString( random.Next( 255 ).ToString() );
                else if( type == typeof( string ) )
                {
                    // Generate a random string of alternating consonants and 
                    // vowels
                    StringBuilder sample = new StringBuilder( stringLength );
                
                    for( int i = 0; i < stringLength; i++ )
                    {
                        if( ( i % 2 ) == 0 )
                            sample.Append( consonants.Substring( random.Next( 0, consonants.Length ), 1 ) );
                        else
                            sample.Append( vowels.Substring( random.Next( 0, vowels.Length ), 1 ) );
                    }

                    xmlTextWriter.WriteString( sample.ToString() );
                }
                else if( type == typeof( UInt64 ) )
                    xmlTextWriter.WriteString( random.Next().ToString() );
                else
                    xmlTextWriter.WriteString(
                        String.Format( "Not implemented for schema datatype {0}, framework datatype {1}", datatype.ToString(), type.ToString() ) );
            }
        }
        #endregion
        #endregion
    }
}
