using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AspNetToolkit.Identity {
	static public class PermissionCodeExtensions {
		public static string GetPrettyName<T>(this T code) where T : struct, IConvertible {
			return Regex.Matches(Enum.GetName(typeof(T), code), "[A-Z][a-z]+").OfType<Match>().Select(match => match.Value).Aggregate((acc, b) => acc + " " + b).TrimStart(' ');
		}
	}
}
