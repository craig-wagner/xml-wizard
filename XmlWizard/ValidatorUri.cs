namespace Wagner.XmlWizard
{
    #region using
    using System;
    using System.IO;
    using System.Net;
    #endregion

    /// <summary>
	/// Subclass for validating a schema referenced by a URI.
	/// </summary>
	public class ValidatorUri : Validator
	{
        #region Public Methods
        public override Stream GetSchemaStream( string uri )
        {
            WebRequest schemaRequest = null;
            WebResponse schemaResponse = null;

            schemaRequest = WebRequest.Create( uri );
            schemaResponse = schemaRequest.GetResponse();

            return schemaResponse.GetResponseStream();
        }

        public override void ValidateSchema( string uri )
        {
            schemaLocation = uri;

            ValidateSchema( GetSchemaStream( uri ) );
        }
        #endregion
	}
}
