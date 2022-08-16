using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Appointment.Models
{
    //public class Users
    //{
    //public enum UserType : byte
    //{
    //    None = 0,
    //    Patient = 1,
    //    Doctor = 2,
    //    Admin = 3
    //}
    //public class User
    //{
    //[Key]
    //public int Id { get; set; }
    //public UserType UserType { get; set; }
    //public string Firstname { get; set; }
    //public string Lastname { get; set; }
    //public string Username { get; set; }
    //public string Email { get; set; }
    //public string Password { get; set; }
    //public string MobileNo { get; set; }
    //public DateTime Birthday { get; set; }
    //public virtual List<Appointement> Appointements { get; set; }

    //}
    public enum Status
    {
        Manager,
        Staff
    }

    public class Doctor
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Specialization { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        //public string ImageURL { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase File { get; set; }
        public byte[] UserPhoto { get; set; }
        public int DepId { get; set; }
        [ForeignKey("DepId")]
        public virtual Department Departament { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }

    }

}