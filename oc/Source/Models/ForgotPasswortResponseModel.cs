using System;
using System.Collections.Generic;

namespace oc.Source.Models
{
	public class ForgotPassworResponseModel : DataResponseModel
	{
		public ForgotPassworResponseModel()
		{
		}


		public class ErrorModel
		{
			
			public string Message { get; set; }

			
			public int Code { get; set; }

		}


		
		public bool Success { get; set; }


		

		
		public bool Error { get; set; }

		
		public List<Object> data { get; set; }


		public class DataModel
		{

		
			public string participantid { get; set; }
		
			public string email { get; set; }

		}
	}




}