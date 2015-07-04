#region using
using System;
using System.Runtime.Serialization;
#endregion

namespace Wagner.Xml.Schema
{
	/// <summary>
	/// Exception that gets thrown when a well-formed XML document is found to
	/// be missing required nodes, has nodes out of sequence, or has other 
	/// structural problems.
	/// </summary>
    [Serializable]
    public class InvalidChildElementException : ApplicationException
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the InvalidChildElementException 
        /// class.
        /// </summary>
        public InvalidChildElementException() : base() {}

        /// <summary>
        /// Initializes a new instance of the InvalidChildElementException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// A message that describes the error.
        /// </param>
        public InvalidChildElementException( string message ) : base( message ) {}

        /// <summary>
        /// Initializes a new instance of the InvalidChildElementException 
        /// class with a specified error message and a reference to the inner 
        /// exception that is the cause of this exception.
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
        public InvalidChildElementException( string message, Exception inner ) : 
            base( message, inner ) {}

        /// <summary>
        /// Initializes a new instance of the InvalidChildElementException 
        /// class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The SerializationInfo that holds the serialized object data about
        /// the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The StreamingContext that contains contextual information about the
        /// source or destination. 
        /// </param>
        protected InvalidChildElementException( SerializationInfo info, StreamingContext context ) : base( info, context ) {}
        #endregion
    }
}
