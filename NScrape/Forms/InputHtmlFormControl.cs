using System;
using System.Globalization;
using System.Web;

namespace NScrape.Forms {

	/// <summary>
	/// Represents an HTML <b>input</b> control.
	/// </summary>
	public class InputHtmlFormControl : HtmlFormControl {

		internal InputHtmlFormControl( string html ) {
			var match = RegexCache.Instance.Regex( RegexLibrary.ParseInput, RegexLibrary.ParseInputOptions ).Match( html );

			if ( match.Success ) {
				AddAttributes( match.Groups[RegexLibrary.ParseInputAttributesGroup].Value );

				// Initalize value if default provided
				if ( Attributes.ContainsKey( "value" ) ) {
					Value = Attributes["value"];
				}

				// Determine input type
				if ( !Attributes.ContainsKey( "type" ) ) {
					// Default if not present is text.
					ControlType = InputHtmlFormControlType.Text;
				}
				else if ( Attributes["type"].ToUpperInvariant() == "DATETIME-LOCAL" ) {
					ControlType = InputHtmlFormControlType.DateTimeLocal;
				}
				else {
					try {
						ControlType = ( InputHtmlFormControlType )Enum.Parse( typeof( InputHtmlFormControlType ), Attributes["type"], true );
					}
					catch ( ArgumentException ) {
						ControlType = InputHtmlFormControlType.Unknown;
					}
				}
			}
			else {
				throw new ArgumentException( string.Format( CultureInfo.CurrentCulture, NScrapeResources.NotAnInputHtmlControl, html ) );
			}
		}

		/// <summary>
		/// Gets the type of the control.
		/// </summary>
		public InputHtmlFormControlType ControlType { get; private set; }

		/// <summary>
		/// Gets the value of the control in <b>application/x-www-form-urlencoded</b> format.
		/// </summary>
		public override string EncodedData {
			get {
				if ( Name.Length > 0 ) {
					return string.Format( CultureInfo.InvariantCulture, "{0}={1}", HttpUtility.UrlEncode( Name ), HttpUtility.UrlEncode( Value ) );
				}

				return string.Empty;
			}
		}

		/// <summary>
		/// Gets or sets the value of the control.
		/// </summary>
		public string Value { get; set; }
	}
}