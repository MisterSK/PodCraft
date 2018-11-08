using System;

namespace PodCraft.Models
{
    public class PodCraftUser
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime  DateOfBirth { get; set; }
    }


    public class PodCraftProduct
    {
        public int Id { get; set; }

        public string Lender { get; set;  }

        public int InterestRate { get; set; }

        public string RateType { get; set; }

        public int LTVRatio { get; set; }
    }

    public class PodCraftSearch
    {
        public int Id { get; set; }

        public string Lender { get; set; }

        public int InterestRate { get; set; }

        public string RateType { get; set; }

        public int LTVRatio { get; set; }
    }

}