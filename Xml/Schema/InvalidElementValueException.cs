#region using
using System;
using System.Runtime.Serialization;
using System.Text;
#endregion

namespace Wagner.Xml.Schema
{
	/// <summary>
	/// Exception that gets thrown when elements with invalid data are found
	/// in the document.
	/// </summary>
    [Serializable]
    public class InvalidElementValueException : ApplicationException
    {
        #region Fields
        string [] elementNames;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets an array containing the names of the elements in the 
        /// XML document that did not pass validation.
        /// </summary>
        public string [] ElementNames
        {
            get { return elementNames; }
            set { elementNames = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the InvalidElementValueException 
        /// class.
        /// </summary>
        public InvalidElementValueException() : base() {}

        /// <summary>
        /// Initializes a new instance of the InvalidElementValueException 
        /// class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// A message that describes the error.
        /// </param>
        public InvalidElementValueException( string message ) : base( message ) {}

        /// <summary>
        /// Initializes a new instance of the InvalidElementValueException 
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
        public InvalidElementValueException( string message, Exception inner ) : base( message, inner ) {}

        /// <summary>
        /// Initializes a new instance of the InvalidElementValueException 
        /// class with a specified error message and an array of the element 
        /// names that were in error.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="elementNames">
        /// An array of strings containing the element names that were reported
        /// as being in error. Only element names resulting in the following
        /// message will be included in the array:
        /// <para>
        /// The 'element' element has an invalid value according to its data 
        /// type" messages
        /// </para>
        /// </param>
        public InvalidElementValueException( string message, string [] elementNames ) : 
            base( message )
        {
            this.elementNames = elementNames;
        }

        /// <summary>
        /// Initializes a new instance of the InvalidElementValueException 
        /// class with a specified error message, an array of the element names
        /// that were in error, and a reference to the inner exception that is 
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="elementNames">
        /// An array of strings containing the element names that were reported
        /// as being in error. Only element names resulting in the following
        /// message will be included in the array:
        /// <para>
        /// The 'element' element has an invalid value according to its data 
        /// type" messages
        /// </para>
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the 
        /// innerException parameter is not a null reference, the current 
        /// exception is raised in a catch block that handles the inner 
        /// exception.
        /// </param>
        public InvalidElementValueException( string message, string [] elementNames, Exception inner ) : base( message, inner )
        {
            this.elementNames = elementNames;
        }

        /// <summary>
        /// Initializes a new instance of the InvalidElementValueException 
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
        protected InvalidElementValueException( SerializationInfo info, StreamingContext context ) : base( info, context ) {}
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates and returns a string representation of the current 
        /// exception.
        /// </summary>
        /// <returns>
        /// A string representation of the current exception.
        /// </returns>
        public override string ToString()
        {
            StringBuilder message = new StringBuilder();

            message.Append( this.GetType().FullName );
            message.Append( ": " );
            message.Append( Environment.NewLine ); 
            message.Append( "Elements that failed validation: " );
            for( int i = 0; i < elementNames.Length; i++ )
            {
                if( i > 0 )
                    message.Append( ", " );

                message.Append( elementNames[i] );
            }
            message.Append( this.Message );
            message.Append( Environment.NewLine );
            message.Append( this.StackTrace );

            return message.ToString();
        }
        #endregion
    }
}
