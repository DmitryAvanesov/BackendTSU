using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend5.Models.ViewModels
{
    public class PlacementViewModel
    {
        public Int32 WardId { get; set; }

        public Ward Ward { get; set; }

        public Int32 PatientId { get; set; }

        public Patient Patient { get; set; }

        public Int32 Bed { get; set; }
    }
}
