﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Omex.Extensions.Logging.Replayable
{
	internal class ReplayableActivity : Activity
	{
		public ReplayableActivity(string operationName) : base(operationName) =>
			m_logEvents = new ConcurrentQueue<LogMessageInformation>();


		public void AddLogEvent(LogMessageInformation logEvent) =>
			m_logEvents.Enqueue(logEvent);


		public IEnumerable<LogMessageInformation> GetLogEvents() =>
			m_logEvents;


		private readonly ConcurrentQueue<LogMessageInformation> m_logEvents;
	}
}
