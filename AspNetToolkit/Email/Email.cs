using AspNetToolkit.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetToolkit.Email {
	public class Email : Entity {
		public int Id { get; set; }
		public string From { get; set; }
		public string FromEmail { get; set; }
		public string To { get; set; }
		public string ToEmail { get; set; }
		public string ToUserId { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public EmailStatus Status { get; set; }
	}
}
