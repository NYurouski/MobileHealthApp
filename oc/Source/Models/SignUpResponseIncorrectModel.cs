using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace oc
{
	class SignUpResponseIncorrectModel : DataResponseModel
	{
		[JsonProperty(ApiConstants.FirstName)]
		public List<string> FirstName { get; set; }

		[JsonProperty(ApiConstants.LastName)]
		public List<string> LastName { get; set; }

		[JsonProperty(ApiConstants.UserName)]
		public List<string> Email { get; set; }

		[JsonProperty(ApiConstants.Password)]
		public List<string> Password { get; set; }

		public override string ToString()
		{
			string result = "";
			if (FirstName != null && FirstName.Count > 0)
			{
				foreach (var item in FirstName)
				{
					result += item + Environment.NewLine;
				}
			}
			result += Environment.NewLine;

			if (LastName != null && LastName.Count > 0)
			{
				foreach (var item in LastName)
				{
					result += item + Environment.NewLine;
				}
			}
			result += Environment.NewLine;
			if (Password != null && Password.Count > 0)
			{
				foreach (var item in Password)
				{
					result += item + Environment.NewLine;
				}
			}
			result += Environment.NewLine;
			if (Email != null && Email.Count > 0)
			{
				foreach (var item in Email)
				{
					result += item + Environment.NewLine;
				}
			}
			result += Environment.NewLine;
			return result;

		}
	}
}