using ITSpark_Task.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ITSpark_Task;

public partial class Student
{
    
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = "Student Name Must Be In Range 3 & 50 ")] 
    [MinLength(3, ErrorMessage = "Student Name Must Be In Range 3 & 50 ")] 
    [Required(ErrorMessage ="Student Name Is Required Please Enter The Name ")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only characters are allowed Enter Valid Name ")]
    public string? Name { get; set; }

    [RegularExpression("^(Male|Female)$", ErrorMessage = "Invalid gender value.")]
 
    public string? Gender { get; set; }
    [Required]
   public string? City { get; set; }

    [DateInPast]
    [DataType(DataType.Date)]

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

    public DateTime BirthDate { get; set; }
}
