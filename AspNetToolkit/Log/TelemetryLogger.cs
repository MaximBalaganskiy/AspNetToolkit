using System;
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
	}
}
