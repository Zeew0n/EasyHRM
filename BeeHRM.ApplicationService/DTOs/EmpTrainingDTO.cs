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
    public partial class EmpTrainingDTO
    {
        public int Id { get; set; }
        public int EmpCode { get; set; }
        public int Designation { get; set; }
        public int Rank { get; set; }
        public int Office { get; set; }
        [StringLength(255)]
        [DisplayName("Title")]
        [Required]
        public string TrainingTitle { get; set; }
        [DisplayName("Training focus")]
        [Required]
        [StringLength(255)]
        public string TrainingFocus { get; set; }
        [DisplayName("Training start date")]
        //[Required]
        public Nullable<DateTime> TrainingStartDate { get; set; }
        public string TrainingStartDateNP { get; set; }
        [DisplayName("Letter Date")]
        //[Required]
        public Nullable<DateTime> LetterDate { get; set; }
        public string LetterDateNP { get; set; }
        [DisplayName("Training end date")]
        //[Required]
        public Nullable<DateTime> TrainingEndDate { get; set; }
        public string TrainingEndDateNP { get; set; }
        [DisplayName("Training visit start date")]
        //[Required]
        public Nullable<DateTime> TrainingVisitStartDate { get; set; }
        public string TrainingVisitStartDateNP { get; set; }
        [DisplayName("Training visit end date")]
        //[Required]
        public Nullable<DateTime> TrainingVisitEndDate { get; set; }
        public string TrainingVisitEndDateNP { get; set; }
        [DisplayName("Training organization")]
        [Required]
        public string TrainingOrganization { get; set; }
        [Required]
        [DisplayName("National/International")]
        public string NationalInternational { get; set; }
        [Required]
        [DisplayName("Training country")]
        public int TrainingCountry { get; set; }
        [DisplayName("Venue")]
        [Required]
        public string TrainingVenue { get; set; }
        [Required]
        public string Sponsorship { get; set; }
        public string SponsorerOrganizationName { get; set; }
        [DisplayName("Approver")]
        [Required]
        public int Approver { get; set; }
        public DesignationDTO Designation1 { get; set; }
        public EmployeeDTO Employee { get; set; }
        public OfficeDTOs Office1 { get; set; }
        public RankDTO Rank1 { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> AssignedByList { get; set; }
        public List<SelectListItem> SponsorshipList { get; set; }
        public List<SelectListItem> NationalInternationalList { get; set; }

        public List<SelectListItem> DesignationList { get; set; }
        public List<SelectListItem> OfficeList { get; set; }
        public List<SelectListItem> RankList { get; set; }

        public int AssignedBy { get; set; }

        public string CountryName { get; set; }
    }
}
