using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RunningActivity
    {
        [Key]
        [JsonIgnore]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserProfileId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserProfileId")]
        public UserProfile? UserProfile { get; set; }
        public string? Location { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        public double Distance { get; set; } //km
        public TimeSpan Duration => EndTime - StartTime;
        public double AveragePace => Duration.TotalMinutes / Distance;

    }
}
