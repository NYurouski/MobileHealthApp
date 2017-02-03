using System;
using System.Text.RegularExpressions;

namespace oc
{
	public class Utils
	{
		 
		public static bool IsEmaiValid(String email){
			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = regex.Match(email);
			return match.Success;
		}

	}
}

