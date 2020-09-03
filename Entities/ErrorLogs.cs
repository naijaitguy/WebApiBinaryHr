using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiBinaryHr.Entities
{
	public class ErrorLogs
	{
		[Key]
		public int Id { get; set; }
		  
		public string Level { get; set;}

		public string Type { get; set; }


		public string Message { get; set; }

		public string StackTrace { get; set; }

		public string CallSite { get; set; }

		public string innerException
		{ get; set; }

		public string additionalInfo
		{ get; set; }

		public DateTime LoggedDate { get; set; }


	} 



}
