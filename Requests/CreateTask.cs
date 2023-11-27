namespace SimonP_amital.Requests
{
    public class CreateTask
    {
        public string Subject { get; set; }
        public int UserId { get; set; }
        public DateTime TargetDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
