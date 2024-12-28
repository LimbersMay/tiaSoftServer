namespace TiaSoftBackend.UseCases.Tables;

public record class TablesUseCases(
    GetTables GetTables,
    GetTable GetTable,
    CreateTable CreateTable,
    UpdateTable UpdateTable,
    SendTableToCashier SendTableToCashier,
    GetTableStatuses GetTableStatuses);