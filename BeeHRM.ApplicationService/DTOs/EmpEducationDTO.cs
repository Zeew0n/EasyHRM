using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
   public class EmpEducationDTO
    {
        public int EduId { get; set; }
        public int EmpCode { get; set; }
        [Display(Name ="Education Level")]
        public int EmpEduLevelId { get; set; }
        [Display(Name ="Degree name (*)")]
        [Required(ErrorMessage ="Degree Name is required")]
        public string DegreeName { get; set; }
        [Display(Name = "Faculty name (*)")]
        [Required(ErrorMessage ="Faculty name is required")]
        public string FacultyName { get; set; }
        [Display(Name = "Passed Date(*)")]
        //[Required(ErrorMessage = "Passed date is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> PassedDate { get; set; }
        public string PassedDateNP { get; set; }
        public int CountryId { get; set; }
        [Display(Name = "University name (*)")]
        [Required(ErrorMessage = "University name is required")]
        public string UniversityName { get; set; }
        public string MarkingSystem { get; set; }
        public string ObtainedMark { get; set; }
        public string ScanDocument { get; set; }
        public string CountryName { get; set; }
        public string EducationLevelName { get; set; }
        public byte EducationStatus { get; set; }
        public string Division { get; set; }
        public HttpPostedFileBase File { get; set; }

        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> EducationLevelList { get; set; }
        public virtual CountryDTO Country { get; set; }
        public virtual EducationLevelDTO EducationLevel { get; set; }
    }
}

