namespace Timetable.Storage.Database.Entities;

public sealed class UserAdminEntity : EntityBase
{
	public required string Login { get; set; }

	public required string Password { get; set; }
}
