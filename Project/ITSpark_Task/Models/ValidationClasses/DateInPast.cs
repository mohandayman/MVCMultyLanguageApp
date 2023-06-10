using System.ComponentModel.DataAnnotations;

namespace ITSpark_Task.Models;

public class DateInPast : ValidationAttribute
{

    public override bool IsValid(object value)
    {
        if (value is DateTime dateValue)
        {
            return dateValue < DateTime.Now;
        }

        return false;
    }


}
