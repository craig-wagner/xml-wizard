namespace Wagner.XmlWizard
{
    #region using
    using System;
    using System.IO;
    #endregion

    /// <summary>
    /// Subclass for validating a schema referenced by a filepath.
    /// </summary>
	public class ValidatorFilepath : Validator
	{
        #region Public Methods
        public override Stream GetSchemaStream( string filePath )
        {
            if( !File.Exists( filePath ) )
                throw new FileNotFoundException( "Specified file not found: " + filePath );

            return new FileStream( filePath, FileMode.Open );
        }

        public override void ValidateSchema( string filePath )
        {
            schemaLocation = filePath;

            ValidateSchema( GetSchemaStream( filePath ) );
        }
        #endregion
	}
}
