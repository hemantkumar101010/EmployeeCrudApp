using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMVC.Models
{
    public class EmployeeViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public int Id { get; set; }
        [DisplayName("First Name"),StringLength(50),Required(ErrorMessage = "The name cann't be empty")]

        [Column(TypeName = "Varchar(20)")]
        public string? FirstName { get; set; }

        [DisplayName("Last Name"), StringLength(50),Required(ErrorMessage = "The name cann't be empty")] 
        [Column(TypeName = "Varchar(20)")]
        public string? LastName { get; set; }

        [StringLength(20),Required(ErrorMessage = "Email cann't be empty"),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(10, MinimumLength = 10), Required(ErrorMessage = "Phone cann't be empty"),DataType(DataType.PhoneNumber)]
        [Column(TypeName = "char(10)")]
        public string Phone { get; set; }

        [StringLength(100),Required(ErrorMessage = "Address cann't be empty")]
        [Column(TypeName = "char(100)")]
        public string? Address { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? City { get; set; }


        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Dob { get; set; }

    }
}
