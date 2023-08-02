namespace DoctorsOffice.Models
{
  public class PatientTag
    {       
        public int PatientTagId { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}