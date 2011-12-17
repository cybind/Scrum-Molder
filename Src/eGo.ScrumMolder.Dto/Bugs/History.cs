using System;
using System.ComponentModel.DataAnnotations;

namespace eGo.ScrumMolder.Dto.Bugs
{
    public class History
    {
        [Key]
        public Guid Id { get; set; }

        public int? Priority { get; set; }

        public virtual Category Category { get; set; }

        public Guid ModifiedById { get; set; }
        public virtual User ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}