namespace Timetable.Framework.Records;

public record class UserRecord
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}
