using AspNetToolkit.Configuration;
using MailKit.Security;

namespace AspNetToolkit.Email {
	public class SmtpSettings {
		public string Server { get; set; }

		[SettingInfo("Port", ClientEditor = ClientEditor.Integer)]
		public int Port { get; set; }

		[SettingInfo("SecureSocketOptions", ClientEditor = ClientEditor.Select)]
		[SelectOption(SecureSocketOptions.None, "None")]
		[SelectOption(SecureSocketOptions.Auto, "Auto")]
		[SelectOption(SecureSocketOptions.SslOnConnect, "SslOnConnect")]
		[SelectOption(SecureSocketOptions.StartTls, "StartTls")]
		[SelectOption(SecureSocketOptions.StartTlsWhenAvailable, "StartTlsWhenAvailable")]
		public SecureSocketOptions SecureSocketOptions { get; set; } = SecureSocketOptions.Auto;

		public string User { get; set; }

		[SettingInfo("Password", ClientEditor = ClientEditor.Password)]
		public string Password { get; set; }
	}
}
