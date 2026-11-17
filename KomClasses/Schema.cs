namespace KomatsuAPI
{
    public class Schema
    {
        public static string query_data_start = string.Empty;
        public static string query_data_end = string.Empty;

        public static string Komatsu_Equipment(string schema, string query_data_middle)
        {
            query_data_start = $"MERGE [{schema}].[Komatsu_Equipment] AS Target {System.Environment.NewLine}";
            query_data_start += $"USING ( VALUES ";
            query_data_start += $"{System.Environment.NewLine}";

            query_data_end = $") AS Source ( [OEMName], [Model], [EquipmentID], [SerialNumber] ) {System.Environment.NewLine}";
            query_data_end += $"ON Source.[SerialNumber] = Target.[SerialNumber] ";
            query_data_end += $"{System.Environment.NewLine}";

            query_data_end += $"WHEN MATCHED THEN {System.Environment.NewLine}" +
                $"UPDATE SET ";
            query_data_end += $"Target.[OEMName] = Source.[OEMName], " +
                $"Target.[Model] = Source.[Model], " +
                $"Target.[SerialNumber] = Source.[SerialNumber], " +
                $"Target.[EquipmentID] = Source.[EquipmentID] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN NOT MATCHED BY Target THEN {System.Environment.NewLine}" +
                $"INSERT ([OEMName], [Model], [EquipmentID], [SerialNumber])";
            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"VALUES ( Source.[OEMName], Source.[Model], Source.[EquipmentID], Source.[SerialNumber] );";

            string query_data_all = query_data_start + query_data_middle + query_data_end;

            return query_data_all;

        }

        public static string Komatsu_Locations(string schema, string query_data_middle)
        {
            query_data_start = $"MERGE [{schema}].[Komatsu_Locations] AS Target {System.Environment.NewLine}";
            query_data_start += $"USING ( VALUES ";
            query_data_start += $"{System.Environment.NewLine}";

            query_data_end = $") AS Source ( [SerialNumber], [Datetime], [Latitude], [Longitude] ) {System.Environment.NewLine}";
            query_data_end += $"ON Source.[SerialNumber] = Target.[SerialNumber] and Source.[Datetime] = Target.[Datetime] ";

            query_data_end += $"WHEN MATCHED THEN {System.Environment.NewLine}UPDATE SET ";
            query_data_end += $"Target.[Latitude] = Source.[Latitude], " +
                $"Target.[Longitude] = Source.[Longitude] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN NOT MATCHED BY Target THEN {System.Environment.NewLine}INSERT ([SerialNumber], [Datetime], [Latitude], [Longitude])";
            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"VALUES ( Source.[SerialNumber], Source.[Datetime], Source.[Latitude], Source.[Longitude] );";

            string query_data_all = query_data_start + query_data_middle + query_data_end;

            return query_data_all;

        }

        public static string Komatsu_CumulativeOperatingHours(string schema, string query_data_middle)
        {
            query_data_start = $"MERGE [{schema}].[Komatsu_CumulativeOperatingHours] AS Target {System.Environment.NewLine}";
            query_data_start += $"USING ( VALUES ";
            query_data_start += $"{System.Environment.NewLine}";

            query_data_end = $") AS Source ( [SerialNumber], [Datetime], [Hour] ) {System.Environment.NewLine}";
            query_data_end += $"ON Source.[SerialNumber] = Target.[SerialNumber] AND Source.[Datetime] = Target.[Datetime] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN MATCHED THEN {System.Environment.NewLine}UPDATE SET ";
            query_data_end += $"Target.[Hour] = Source.[Hour] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN NOT MATCHED BY Target THEN {System.Environment.NewLine}";
            query_data_end += $"INSERT ([SerialNumber], [Datetime], [Hour])";
            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"VALUES ( Source.[SerialNumber], Source.[Datetime], Source.[Hour] ); ";

            string query_data_all = query_data_start + query_data_middle + query_data_end;

            return query_data_all;
        }

        public static string Komatsu_CumulativeFuelUsed(string schema, string query_data_middle)
        {
            query_data_start = $"MERGE [{schema}].[Komatsu_CumulativeFuelUsed] AS Target {System.Environment.NewLine}";
            query_data_start += $"USING ( VALUES ";
            query_data_start += $"{System.Environment.NewLine}";

            query_data_end = $") AS Source ( [SerialNumber], [Datetime], [FuelUnits], [FuelConsumed] ) {System.Environment.NewLine}";
            query_data_end += $"ON Source.[SerialNumber] = Target.[SerialNumber] AND Source.[Datetime] = Target.[Datetime] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN MATCHED THEN {System.Environment.NewLine}UPDATE SET ";
            query_data_end += $"Target.[FuelUnits] = Source.[FuelUnits], Target.[FuelConsumed] = Source.[FuelConsumed] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN NOT MATCHED BY Target THEN {System.Environment.NewLine}";
            query_data_end += $"INSERT ([SerialNumber], [Datetime], [FuelUnits], [FuelConsumed])";
            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"VALUES ( Source.[SerialNumber], Source.[Datetime], Source.[FuelUnits], Source.[FuelConsumed] ); ";

            string query_data_all = query_data_start + query_data_middle + query_data_end;

            return query_data_all;
        }

        public static string Komatsu_FuelUsedInThePreceding24Hours(string schema, string query_data_middle)
        {
            query_data_start = $"MERGE [{schema}].[Komatsu_FuelUsedInThePreceding24Hours] AS Target {System.Environment.NewLine}";
            query_data_start += $"USING ( VALUES ";
            query_data_start += $"{System.Environment.NewLine}";

            query_data_end = $") AS Source ( [SerialNumber], [Datetime], [FuelUnits], [FuelConsumed] ) {System.Environment.NewLine}";
            query_data_end += $"ON Source.[SerialNumber] = Target.[SerialNumber] AND Source.[Datetime] = Target.[Datetime] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN MATCHED THEN {System.Environment.NewLine}UPDATE SET ";
            query_data_end += $"Target.[FuelUnits] = Source.[FuelUnits], Target.[FuelConsumed] = Source.[FuelConsumed] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN NOT MATCHED BY Target THEN {System.Environment.NewLine}";
            query_data_end += $"INSERT ([SerialNumber], [Datetime], [FuelUnits], [FuelConsumed])";
            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"VALUES ( Source.[SerialNumber], Source.[Datetime], Source.[FuelUnits], Source.[FuelConsumed] ); ";

            string query_data_all = query_data_start + query_data_middle + query_data_end;

            return query_data_all;
        }

        public static string Komatsu_FuelRemainingRatio(string schema, string query_data_middle)
        {
            query_data_start = $"MERGE [{schema}].[Komatsu_FuelRemainingRatio] AS Target {System.Environment.NewLine}";
            query_data_start += $"USING ( VALUES ";
            query_data_start += $"{System.Environment.NewLine}";

            query_data_end = $") AS Source ( [SerialNumber], [Datetime], [Percent] ) {System.Environment.NewLine}";
            query_data_end += $"ON Source.[SerialNumber] = Target.[SerialNumber] AND Source.[Datetime] = Target.[Datetime] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN MATCHED THEN {System.Environment.NewLine}UPDATE SET ";
            query_data_end += $"Target.[Percent] = Source.[Percent] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN NOT MATCHED BY Target THEN {System.Environment.NewLine}";
            query_data_end += $"INSERT ([SerialNumber], [Datetime], [Percent])";
            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"VALUES ( Source.[SerialNumber], Source.[Datetime], Source.[Percent] ); ";

            string query_data_all = query_data_start + query_data_middle + query_data_end;

            return query_data_all;
        }

        public static string Komatsu_CumulativeNonProductiveIdleHours(string schema, string query_data_middle)
        {
            query_data_start = $"MERGE [{schema}].[Komatsu_CumulativeNonProductiveIdleHours] AS Target {System.Environment.NewLine}";
            query_data_start += $"USING ( VALUES ";
            query_data_start += $"{System.Environment.NewLine}";

            query_data_end = $") AS Source ( [SerialNumber], [Datetime], [Hour] ) {System.Environment.NewLine}";
            query_data_end += $"ON Source.[SerialNumber] = Target.[SerialNumber] AND Source.[Datetime] = Target.[Datetime] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN MATCHED THEN {System.Environment.NewLine}UPDATE SET ";
            query_data_end += $"Target.[Hour] = Source.[Hour] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN NOT MATCHED BY Target THEN {System.Environment.NewLine}";
            query_data_end += $"INSERT ([SerialNumber], [Datetime], [Hour])";
            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"VALUES ( Source.[SerialNumber], Source.[Datetime], Source.[Hour] ); ";

            string query_data_all = query_data_start + query_data_middle + query_data_end;

            return query_data_all;
        }

        public static string Komatsu_Faults(string schema, string query_data_middle)
        {
            query_data_start = $"MERGE [{schema}].[Komatsu_Faults] AS Target {System.Environment.NewLine}";
            query_data_start += $"USING ( VALUES ";
            query_data_start += $"{System.Environment.NewLine}";

            query_data_end = $") AS Source ( [SerialNumber], [Datetime], [CodeIdentifier], [CodeDescription], [CodeSeverity], [CodeSource] ) {System.Environment.NewLine}";
            query_data_end += $"ON Source.[SerialNumber] = Target.[SerialNumber] AND Source.[Datetime] = Target.[Datetime] AND Source.[CodeIdentifier] = Target.[CodeIdentifier] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN MATCHED THEN {System.Environment.NewLine}UPDATE SET ";
            query_data_end += $"Target.[CodeDescription] = Source.[CodeDescription], " +
                $"Target.[CodeSeverity] = Source.[CodeSeverity], Target.[CodeSource] = Source.[CodeSource] ";

            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"WHEN NOT MATCHED BY Target THEN {System.Environment.NewLine}";
            query_data_end += $"INSERT ([SerialNumber], [Datetime], [CodeIdentifier], [CodeDescription], [CodeSeverity], [CodeSource])";
            query_data_end += $"{System.Environment.NewLine}";
            query_data_end += $"VALUES ( Source.[SerialNumber], Source.[Datetime], Source.[CodeIdentifier], Source.[CodeDescription], Source.[CodeSeverity], Source.[CodeSource] ); ";

            string query_data_all = query_data_start + query_data_middle + query_data_end;

            return query_data_all;
        }




    }
}
