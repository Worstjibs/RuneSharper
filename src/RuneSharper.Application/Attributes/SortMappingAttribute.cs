namespace RuneSharper.Application.Attributes;

public class SortMappingAttribute : Attribute
{
    public SortMappingAttribute(string tableName, string columnName)
    {
        TableName = tableName;
        ColumnName = columnName;
    }

    public string TableName { get; }
    public string ColumnName { get; }
}
