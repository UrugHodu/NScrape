﻿using System;

namespace NScrape {
    /// <summary>
	/// Represents a web response for a request that returned binary data.
	/// </summary>
    public class BinaryWebResponse : WebResponse {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryWebResponse"/> class.
        /// </summary>
        /// <param name="success"><b>true</b> if the response is considered successful, <b>false</b> otherwise.</param>
        /// <param name="url">The URL of the response.</param>
        /// <param name="data">The data that was returned by the web server.</param>
        public BinaryWebResponse( bool success, Uri url, byte[] data )
            : base( url, WebResponseType.Binary, success ) {
            this.Data = data;
        }

        /// <summary>
		/// Gets the binary data.
		/// </summary>
        public byte[] Data { get; private set; }
    }
}
