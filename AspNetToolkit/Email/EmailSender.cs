using AspNetToolkit.Log;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetToolkit.Email {
	public class EmailSender : IEmailSender {
		private readonly ITelemetryLogger _telemetryLogger;
		private readonly ILogger<EmailSender> _logger;

		public EmailSender(ITelemetryLogger telemetryLogger, ILogger<EmailSender> logger) {
			_telemetryLogger = telemetryLogger;
			_logger = logger;
		}

		public async Task Send(string from, string fromEmail, string to, string toEmail, string subject, string body, SmtpSettings smtpSettings, CancellationToken cancellationToken = default) {
			var msg = new MimeMessage();
			msg.From.Add(new MailboxAddress(from ?? fromEmail, fromEmail));
			msg.To.Add(new MailboxAddress(to ?? toEmail, toEmail));
			msg.Subject = subject;
			msg.Body = new TextPart("html") { Text = body };

			using var client = new SmtpClient();
			using (var depTrack = _telemetryLogger.TrackDependency("Email", nameof(EmailSender), nameof(client.ConnectAsync))) {
				await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, MailKit.Security.SecureSocketOptions.Auto, cancellationToken);
				depTrack.Success();
			}
			using (var depTrack = _telemetryLogger.TrackDependency("Email", nameof(EmailSender), nameof(client.AuthenticateAsync))) {
				await client.AuthenticateAsync(smtpSettings.User, smtpSettings.Password, cancellationToken);
				depTrack.Success();
			}
			using (var depTrack = _telemetryLogger.TrackDependency("Email", nameof(EmailSender), nameof(client.SendAsync))) {
				await client.SendAsync(msg, cancellationToken);
				depTrack.Success();
			}
			using (var depTrack = _telemetryLogger.TrackDependency("Email", nameof(EmailSender), nameof(client.DisconnectAsync))) {
				await client.DisconnectAsync(true, cancellationToken);
				depTrack.Success();
			}
		}
	}
}
