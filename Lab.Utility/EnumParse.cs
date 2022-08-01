using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Utility
{
	public class EnumParse
	{
		public bool Main()
		{
			HttpStatusCode statusCode;
			HttpStatusCode.TryParse("500", out statusCode);
			switch (statusCode)
			{
				case HttpStatusCode.OK:
					return true;

				case HttpStatusCode.Continue:
				case HttpStatusCode.SwitchingProtocols:
				case HttpStatusCode.Created:
				case HttpStatusCode.Accepted:
				case HttpStatusCode.NonAuthoritativeInformation:
				case HttpStatusCode.NoContent:
				case HttpStatusCode.ResetContent:
				case HttpStatusCode.PartialContent:
				case HttpStatusCode.MultipleChoices:
				case HttpStatusCode.MovedPermanently:
				case HttpStatusCode.Found:
				case HttpStatusCode.SeeOther:
				case HttpStatusCode.NotModified:
				case HttpStatusCode.UseProxy:
				case HttpStatusCode.Unused:
				case HttpStatusCode.TemporaryRedirect:
				case HttpStatusCode.BadRequest:
				case HttpStatusCode.Unauthorized:
				case HttpStatusCode.PaymentRequired:
				case HttpStatusCode.Forbidden:
				case HttpStatusCode.NotFound:
				case HttpStatusCode.MethodNotAllowed:
				case HttpStatusCode.NotAcceptable:
				case HttpStatusCode.ProxyAuthenticationRequired:
				case HttpStatusCode.RequestTimeout:
				case HttpStatusCode.Conflict:
				case HttpStatusCode.Gone:
				case HttpStatusCode.LengthRequired:
				case HttpStatusCode.PreconditionFailed:
				case HttpStatusCode.RequestEntityTooLarge:
				case HttpStatusCode.RequestUriTooLong:
				case HttpStatusCode.UnsupportedMediaType:
				case HttpStatusCode.RequestedRangeNotSatisfiable:
				case HttpStatusCode.ExpectationFailed:
				case HttpStatusCode.UpgradeRequired:
				case HttpStatusCode.InternalServerError:
				case HttpStatusCode.NotImplemented:
				case HttpStatusCode.BadGateway:
				case HttpStatusCode.ServiceUnavailable:
				case HttpStatusCode.GatewayTimeout:
				case HttpStatusCode.HttpVersionNotSupported:
					return false;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
