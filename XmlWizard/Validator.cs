namespace Wagner.XmlWizard
{
    #region using
    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Schema;
    #endregion

    /// <summary>
	/// Contains the primary logic for validating an XML schema.
	/// </summary>
	public abstract class Validator
	{
        #region Constants
        private const int initialCapacity = 1024 * 5;
        #endregion

        #region Fields
        private bool validationSuccess;
        private StringBuilder validationMessage;
        protected string schemaLocation;
        #endregion

        #region Public Methods
        public abstract void ValidateSchema( string filePathOrUri );
        public abstract Stream GetSchemaStream( string filePathOrUri );
        #endregion

        #region Protected Methods
        protected void ValidateSchema( Stream schemaSource )
        {
            XmlSchema schema = null;

            try
            {
                validationSuccess = true;
                validationMessage = new StringBuilder( initialCapacity );

                schema = new XmlSchema();
                schema = XmlSchema.Read( schemaSource, new ValidationEventHandler( ValidationErrorLogger ) );
                if( !validationSuccess )
                    throw new ApplicationException( validationMessage.ToString() );
                else
                {
                    schema.Compile( new ValidationEventHandler( ValidationErrorLogger ) );

                    if( !validationSuccess )
                        throw new ApplicationException( validationMessage.ToString() );
                }

            }
            finally
            {
                if( schemaSource != null )
                    schemaSource.Close();
            }
        }
        #endregion

        #region Private Methods
        private void ValidationErrorLogger( object sender, ValidationEventArgs e )
        {
            validationSuccess = false;

            validationMessage.Append( e.Message + Environment.NewLine );
        }
        #endregion
	}
}
