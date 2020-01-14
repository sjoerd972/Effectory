using Data.Model.People;
using System;

namespace Data.Model
{
    public class BaseModel
    {
        public int Id { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public int UpdatedByUserId { get; set; }
        public User UpdatedByUser { get; set; }
        public int DeletedByUserId { get; set; }
        public User DeletedByUser { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
