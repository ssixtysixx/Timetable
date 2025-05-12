namespace Timetable.Storage.Framework;

public interface IRecordRepository
{

}

public interface IRecordMutationRepository : IRecordRepository
{

}
public class RecordRepository : IRecordMutationRepository { }