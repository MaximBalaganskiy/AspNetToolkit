using System;

namespace AspNetToolkit.Log {
	public interface ITelemetryLogger {
		TrackDependencyHandler TrackDependency(string dependencyTypeName, string dependencyName, string command);
		void TrackException(Exception exception);
	}
}
