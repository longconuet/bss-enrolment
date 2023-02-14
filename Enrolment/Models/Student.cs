namespace Enrolment.Models
{
    public class Student : BaseModel
    {
        public string FirstName { get; set; } = "";
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = "";
        public string? PreferredName { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public int Gender { get; set; }


        public int YearOfEntry { get; set; }
        public int TermOfEntry { get; set; }
        public int CalendarYearOfEntry { get; set; }


        public int? CurrentSchool { get; set; }
        public int? CurrentYearLevel { get; set; }


        public int Nationality { get; set; }
        public int CountryOfBirth { get; set; }
        public int ResidencyStatus { get; set; }

        // something


        public string StreetAddress { get; set; } = "";
        public string? UnitApartment { get; set; }
        public string Suburb { get; set; } = "";
        public int State { get; set; }
        public int PostCode { get; set; }
        public int Country { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? StudentLivesWith { get; set; }
        public List<int>? ParentsRelationship { get; set; }
        public string? ParentalResponsisbility { get; set; }
        public string? CourtOrdersOrParentingPlans { get; set; }
    }
}
