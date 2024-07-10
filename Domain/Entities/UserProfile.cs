using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserProfile
    {
        [Key]
        [JsonIgnore]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;

        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public double BMI => Math.Round(Weight / Math.Pow(Height / 100, 2), 2);

        [JsonIgnore]
        public ICollection<RunningActivity>? RunningActivities { get; set; }
    }
}
