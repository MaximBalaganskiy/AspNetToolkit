using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetToolkit.Db {
	public class Entity : IEntity {
		[Timestamp]
		public byte[] Timestamp { get; set; }

		public string UserAdd { get; set; }
		public DateTime DateAdd { get; set; }
		public string UserMod { get; set; }
		public DateTime DateMod { get; set; }
	}
}
