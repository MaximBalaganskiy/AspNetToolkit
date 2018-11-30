using System;

namespace AspNetToolkit.Date {
	public interface IDateService {
		DateTime Now();

		DateTime MinValue { get; }

		DateTime MaxValue { get; }

		DateTime Today();

		void Override(DateTime? dateTime);
	}
}