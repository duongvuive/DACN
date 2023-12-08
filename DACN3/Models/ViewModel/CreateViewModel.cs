namespace DACN3.Models.ViewModel
{
    public class CreateViewModel
    {
        public int IdDevice { get; set; }
        public int IdClassroom { get; set; }
        public int Quantify { get; set; }

        public int DeviceClassroomId { get; set; }
        public string UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public bool? Status { get; set; }
        public string Borrower { get; set; }
    }
}
