using AspNetToolkit.Configuration;

namespace AspNetToolkit.Email {
	public class SmtpSettings {
		public string Server { get; set; }

		[SettingInfo("Port", ClientEditor = ClientEditor.Integer)]
		public int Port { get; set; }

		public string User { get; set; }

		[SettingInfo("Password", ClientEditor = ClientEditor.Password)]
		public string Password { get; set; }
	}
}
