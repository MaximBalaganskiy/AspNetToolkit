using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace AspNetToolkit.Log {
	public class TelemetryLogger : ITelemetryLogger {
		private readonly TelemetryClient _telemetry;

		public TelemetryLogger(TelemetryClient telemetry) {
			_telemetry = telemetry;
		}

		public TrackDependencyHandler TrackDependency(string dependencyTypeName, string dependencyName, string command) {
			return new TrackDependencyHandler(_telemetry, dependencyTypeName, dependencyName, command);
		}

		public void TrackException(Exception exception) {
			_telemetry.TrackException(exception);
		}

		public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null) {
			_telemetry.TrackEvent(eventName, properties, metrics);
		}
		
		public void Flush() {
			_telemetry.Flush();
		}
	}
}
