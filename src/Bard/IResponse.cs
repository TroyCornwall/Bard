using System.Net;

namespace Bard
{
    /// <summary>
    ///     Wrapper interface around an API Response
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        ///     Gateway property for fluent interface
        /// </summary>
        IShouldBe ShouldBe { get; }

        /// <summary>
        /// Gateway to check the response headers.
        /// </summary>
        IHeaders Headers { get; }
        
        internal bool Log { get; set; }

        /// <summary>
        ///     Assert that the response Http Code is correct
        /// </summary>
        /// <param name="statusCode">The required Http Status code</param>
        void StatusCodeShouldBe(HttpStatusCode statusCode);

        /// <summary>
        ///     Convert the response to the specified class
        /// </summary>
        /// <typeparam name="T">The class to be converted to.</typeparam>
        /// <returns>The converted class instance</returns>
        T Content<T>();

        /// <summary>
        /// Write out the http response to the console.
        /// </summary>
        void WriteResponse();

        /// <summary>
        /// Assert the elapsed time of the API response
        /// </summary>
        ITime Time { get; }
    }
}