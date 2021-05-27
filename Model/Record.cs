namespace EmbeddedRecordCreator.Model
{
    public class Record
    {
        public Evnt Evnt { get; set; } = new();
        public long Time { get; set; }
    }
}