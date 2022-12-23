using System;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvHelpers
{
    public class Data
    {
        protected DataTable DataTable;
        protected Row Row;

        public Data(Row row, DataTable dataTable)
        {
            Row = row;
            DataTable = dataTable;
        }

        public void LoadData(Data data, Type type, Row row)
        {
            Row = row;
            Row.LoadData(data);
        }
        public string GetName()
        {
            return Row.GetName();
        }
    }
}