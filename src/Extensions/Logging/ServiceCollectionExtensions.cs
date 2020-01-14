﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Omex.Extensions.Abstractions;
using Microsoft.Omex.Extensions.Logging.Replayable;

namespace Microsoft.Omex.Extensions.Logging
{
	/// <summary>Extension methods for the <see cref="ILoggerFactory"/> class</summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>Adds Omex event logger to the factory</summary>
		/// <param name="builder">The extension method argument</param>
		/// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained</returns>
		public static ILoggingBuilder AddOmexLogging<TServiceContext>(this ILoggingBuilder builder)
			where TServiceContext : class, IServiceContext
		{
			builder.Services.AddOmexLogging<TServiceContext>();
			return builder;
		}


		/// <summary>Adds Omex event logger to the factory</summary>
		/// <param name="serviceCollection">The extension method argument</param>
		/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained</returns>
		public static IServiceCollection AddOmexLogging<TServiceContext>(this IServiceCollection serviceCollection)
			where TServiceContext : class, IServiceContext
		{
			serviceCollection
				.AddOmexMachineInformation()
				.AddOmexServiceContext<TServiceContext>();

			serviceCollection.TryAddTransient<IActivityProvider, ReplayibleActivityProvider>();
			serviceCollection.TryAddTransient<IExternalScopeProvider, LoggerExternalScopeProvider>();
			serviceCollection.TryAddTransient<ILogReplayer, LogRepayer>();
			serviceCollection.TryAddTransient<OmexLogsEventSource, OmexLogsEventSource>();
			serviceCollection.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, OmexLoggerProvider>());
			return serviceCollection;
		}
	}
}