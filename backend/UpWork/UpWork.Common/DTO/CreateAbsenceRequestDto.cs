<<<<<<< HEAD
﻿namespace UpWork.Common.DTO
=======
﻿using UpWork.Common.Enums;

namespace UpWork.Common.DTO
>>>>>>> dev
{
    public class CreateAbsenceRequestDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
<<<<<<< HEAD
        public Guid AbsenceTypeId { get; set; }
=======
        public AbsenceTypes AbsenceType { get; set; }
>>>>>>> dev
    }

}
