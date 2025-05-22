using Microsoft.Extensions.DependencyInjection;
using Timetable.Storage.Framework;

namespace Timetable.Storage.Database;

public sealed class ContextFactory(IServiceProvider serviceProvider) : IContextFactory
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;

	public TimetableDBContext CreateContext()
	{
		var scope = _serviceProvider.CreateScope();

		return scope.ServiceProvider.GetRequiredService<TimetableDBContext>();
	}
}
