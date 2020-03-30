using System;
using System.Collections.Generic;

namespace AspNetToolkit.Log {
	public interface ITelemetryLogger {
		TrackDependencyHandler TrackDependency(string dependencyTypeName, string dependencyName, string command);
		void TrackException(Exception exception);
		void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);
		void Flush();
	}
}
