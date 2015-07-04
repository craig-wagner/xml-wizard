namespace Wagner.Local.Xml.Schema
{
    #region using
    using System;
    #endregion

	/// <summary>
	/// Exception that gets thrown when the validation of a business object's
	/// XML against its schema fails.
	/// </summary>
    public class XmlValidationException : ApplicationException
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the XmlValidationException class.
        /// </summary>
        public XmlValidationException() : base() {}

        /// <summary>
        /// Initializes a new instance of the XmlValidationException class with
        /// a specified error message.
        /// </summary>
        /// <param name="message">
        /// A message that describes the error.
        /// </param>
        public XmlValidationException( string message ) : base( message ) {}

        /// <summary>
        /// Initializes a new instance of the XmlValidationException class with
        /// a specified error message and a reference to the inner exception 
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the 
        /// innerException parameter is not a null reference, the current 
        /// exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
        public XmlValidationException( string message, Exception inner ) : base( message, inner ) {}
		#endregion
    }
}
