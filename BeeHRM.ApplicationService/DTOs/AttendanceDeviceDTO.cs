namespace BeeHRM.ApplicationService.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttendaceDevice")]
    public partial class AttendanceDeviceDTO
    {
        [Key]
        public int DeviceId { get; set; }

        [StringLength(50)]
        public string DeviceName { get; set; }

        [StringLength(50)]
        public string DeviceIP { get; set; }

        public int? DevicePort { get; set; }

        [StringLength(50)]
        public string DevicePassword { get; set; }

        public int? DeviceMachineNo { get; set; }

        public TimeSpan? DeviceFetchStartTime { get; set; }

        public TimeSpan? DeviceFetchEndTime { get; set; }

        public DateTime? DeviceLastImportDate { get; set; }

        public bool? DeviceStatus { get; set; }

        public bool? DeviceDataDelete { get; set; }

        public int DeviceType { get; set; }
    }
}
