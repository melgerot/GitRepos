using System;
using System.Net;

namespace SAP.CRM.Core.SAL
{
	public class CustomWebClient : WebClient
	{
		private int _timeout;
		/// <summary>
		/// Time in milliseconds
		/// </summary>
		public int Timeout {
			get {
				return _timeout;
			}
			set {
				_timeout = value;
			}
		}

		public CustomWebClient ()
		{
			this.Encoding = System.Text.Encoding.UTF8;
			this._timeout = 60000;
		}

		public CustomWebClient (int timeout)
		{
			this._timeout = timeout;
		}

		protected override WebRequest GetWebRequest (Uri address)
		{
			var result = base.GetWebRequest (address);
			result.Timeout = this._timeout;
			return result;
		}
	}
}

