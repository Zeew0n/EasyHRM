using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
   public class EmployeeTrainingDTO
    {
        public int TrainingId { get; set; }
        public Nullable<int> EmpCode { get; set; }
        [Display(Name ="Name")]
        public string TrainingName { get; set; }
        [Display(Name ="Training Year")]
        public Nullable<int> TrainingYear { get; set; }
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> TrainingStartDate { get; set; }
        public string TrainingStartDateNP { get; set; }
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> TrainingEndDate { get; set; }
        public string TrainingEndDateNP { get; set; }
        public Nullable<int> TrainingDays { get; set; }
        [Display(Name = "Subject")]
        public string TrainingSubject { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string TrainingDescription { get; set; }
        [Display(Name = "Country")]
        public Nullable<int> CountryId { get; set; }
        [Display(Name = "Venue")]
        public string Venue { get; set; }
        [Display(Name = "Training Provided By")]
        public string TrainingProvidedBy { get; set; }
        public string CountryName { get; set; }
        [DisplayName("Current designation")]
        public int CurrentDesignation { get; set; }
        [DisplayName("Current rank")]
        public int CurrentRank { get; set; }
        [DisplayName("Current office")]
        public int CurrentOffice { get; set; }
        [DisplayName("Assigned by")]
        public int AssignedBy { get; set; }
        public string Sponsorship { get; set; }
        [DisplayName("National/International")]
        public string NationalInternational { get; set; }
        public List<SelectListItem> DesignationList { get; set; }
        public List<SelectListItem> RankList { get; set; }
        public List<SelectListItem> OfficeList { get; set; }
        public List<SelectListItem> AssignedByList { get; set; }
        public List<SelectListItem> SponsorshipList { get; set; }
        public List<SelectListItem> NationalInternationalList { get; set; }
    }
}
