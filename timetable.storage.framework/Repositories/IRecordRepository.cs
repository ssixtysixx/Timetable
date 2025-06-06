﻿using Timetable.Framework.Records;

namespace Timetable.Framework;

public interface IRecordRepository
{
	Task<DayRecord> GetDayRecordsByDate(DateTime date, CancellationToken cancellationToken);
}

public interface IRecordMutationRepository : IRecordRepository
{
	Task<List<GroupByDayRecords>> GiveMeRecordForAllGroups(DateTime date, CancellationToken cancellationToken);

	Task<List<string>> GetAllGroupsNames();

	Task<bool> AddGroupIfNotExistAsync(string groupName);

	Task<bool> AddNewGroupRecord(DayScheduleDto dayScheduleDto);

	Task<bool> ExistsScheduleForGroupOnDate(DayScheduleDto dayScheduleDto);
}
