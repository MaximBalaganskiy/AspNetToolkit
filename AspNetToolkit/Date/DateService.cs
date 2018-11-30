using System;

namespace AspNetToolkit.Date {
	public class DateService : IDateService {
		private TimeSpan? _override;

		public DateTime MinValue => new DateTime(1800, 1, 1);

		public DateTime MaxValue => new DateTime(2500, 1, 1);

		public DateTime Now() => DateTime.Now - (_override ?? TimeSpan.Zero);

		public DateTime Today() => Now().Date;

		public void Override(DateTime? dateTime) {
			_override = DateTime.Now - dateTime;
		}
	}
}
