using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestLoginWebApi.Models;
using System.Web.Http.Cors;
using TestLoginWebApi.ICUTech;
using System.Text.Json;

namespace TestLoginWebApi.Controllers
{
	[EnableCors("*", "*", "*")]
	public class LoginController : ApiController
	{
		ICUTechClient client = new ICUTechClient();
		public void Get()
		{
		}
		// POST: api/Login
		public HttpResponseMessage Post(LoginModel creds)
		{
			HttpResponseMessage response;
			if (creds != null)
			{
				response = Request.CreateResponse(HttpStatusCode.OK);
				string body;
				string result = client.Login(creds.username, creds.password, "");
				LoginSuccess Success = JsonSerializer.Deserialize<LoginSuccess>(result);
				if (Success.Email != null)
					body = "success";
				else
					body = "fail";
				response.Content = new StringContent(body);
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.InternalServerError);
			}
			return response;
		}
    }
}
