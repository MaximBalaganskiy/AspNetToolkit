using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetToolkit.Db {
	public interface IEntity {
		[Timestamp]
		byte[] Timestamp { get; set; }

		string UserAdd { get; set; }
		DateTime DateAdd { get; set; }
		string UserMod { get; set; }
		DateTime DateMod { get; set; }
	}
}
