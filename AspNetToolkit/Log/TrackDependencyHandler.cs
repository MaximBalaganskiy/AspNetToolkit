using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AspNetToolkit.Log {
	public class TrackDependencyHandler : IDisposable {
		private readonly TelemetryClient _telemetry;
		private readonly string _dependencyTypeName;
		private readonly string _dependencyName;
		private readonly string _command;
		private readonly DateTime _startTime;
		private readonly Stopwatch _timer;
		private bool _success;

		public TrackDependencyHandler(TelemetryClient telemetry, string dependencyTypeName, string dependencyName, string command) {
			_telemetry = telemetry;
			_dependencyTypeName = dependencyTypeName;
			_dependencyName = dependencyName;
			_command = command;
			_startTime = DateTime.UtcNow;
			_timer = Stopwatch.StartNew();
		}

		public void Success() {
			_success = true;
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
			_timer.Stop();
			_telemetry.TrackDependency(_dependencyTypeName, _dependencyName, _command, _startTime, _timer.Elapsed, _success);
		}
	}
}
