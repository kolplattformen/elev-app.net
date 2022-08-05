namespace SkolplattformenElevApi.Models.Internal.Sharepoint
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<PostQueryResponse>(myJsonResponse);
    internal class Cell
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string ValueType { get; set; }
    }

    internal class PrimaryQueryResult
    {
        public List<object> CustomResults { get; set; }
        public string QueryId { get; set; }
        public string QueryRuleId { get; set; }
        public object RefinementResults { get; set; }
        public RelevantResults RelevantResults { get; set; }
        public object SpecialTermResults { get; set; }
    }

    internal class Property
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }

    internal class RelevantResults
    {
        public object GroupTemplateId { get; set; }
        public object ItemTemplateId { get; set; }
        public List<Property> Properties { get; set; }
        public object ResultTitle { get; set; }
        public object ResultTitleUrl { get; set; }
        public int RowCount { get; set; }
        public Table Table { get; set; }
        public int TotalRows { get; set; }
        public int TotalRowsIncludingDuplicates { get; set; }
    }

    internal class PostQueryResponse
    {
        public int ElapsedTime { get; set; }
        public PrimaryQueryResult PrimaryQueryResult { get; set; }
        public List<Property> Properties { get; set; }
        public List<object> SecondaryQueryResults { get; set; }
        public object SpellingSuggestion { get; set; }
        public List<object> TriggeredRules { get; set; }
    }

    internal class Row
    {
        public List<Cell> Cells { get; set; }
    }

    internal class Table
    {
        public List<Row> Rows { get; set; }
    }


}
