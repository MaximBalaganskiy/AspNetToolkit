using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetToolkit.Email {
	public interface IEmailSender {
		Task Send(string from, string fromEmail, string toEmail, string subject, string body, SmtpSettings smtpSettings, CancellationToken cancellationToken = default);
	}
}
