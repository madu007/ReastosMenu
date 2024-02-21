using System.ComponentModel.DataAnnotations;

namespace ReastosMenu.Models
{
    public enum NumberOfPerson
    {
        [Display(Name = "One Person")] One_Person,
        [Display(Name = "Two Persons")] Two_Persons,
        [Display(Name = "Three Persons")] Three_Person,
        [Display(Name = "Four Persons")] Four_Persons,
        [Display(Name = "Five Persons")] Five_Persons
    }
}
