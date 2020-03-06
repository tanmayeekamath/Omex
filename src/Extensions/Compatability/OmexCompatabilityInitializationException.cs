﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace Microsoft.Omex.Extensions.Compatability
{
	internal class OmexCompatabilityInitializationException : Exception
	{
		internal OmexCompatabilityInitializationException()
			: base($"Compatability classes were not initialized. Please call AddOmexCompatabilityServices() on HostBuilder")
		{ }
	}
}
