using System.Collections.Generic;

namespace DoctorsOffice.Models
{
  public class Patient
  {
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public string Name { get; set; }
    public Doctor Doctor { get; set; } //code doctor entity/navigation property 
    public List<PatientTag> JoinEntities { get;}
  }
}