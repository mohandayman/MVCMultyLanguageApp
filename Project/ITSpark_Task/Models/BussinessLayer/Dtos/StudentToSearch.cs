using ITSpark_Task.Models.BussinessLayer.HelperClasses;
using ITSpark_Task.Models.ValidationClasses;
using System.ComponentModel.DataAnnotations;

namespace ITSpark_Task.Models.BussinessLayer;

public class StudentToSearch
{


    [MaxLength(50, ErrorMessage = "Student Name Must Be In Range 3 & 50 ")]
    [MinLength(3, ErrorMessage = "Student Name Must Be In Range 3 & 50 ")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only characters are allowed Enter Valid Name ")]
    public string? Name { get; set; } = string.Empty;

    //[RegularExpression("^(Male|Female|All)$", ErrorMessage = "Invalid gender value.")]

    public string Gender { get; set; } 
    public string City { get; set; }


    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDateFrom { get; set; } = DateTime.Now;

    [DateFromToAttribute("BirthDateFrom", "BirthDateTo")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDateTo { get; set; } = DateTime.Now;


    public StudentToSearch()
    {
        Name = string.Empty;
        Gender = "All";
        City = "All";
        BirthDateFrom = DateTime.Now;
        BirthDateTo = DateTime.Now;
    }


}
