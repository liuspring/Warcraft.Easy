namespace EventCloud
{
    public class DataTablesResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered {
            get { return recordsTotal; }
        }

        public object data;
    }
}
