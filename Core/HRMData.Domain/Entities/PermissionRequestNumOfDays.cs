using HRMData.Domain.Entities.Common;

namespace HRMData.Domain.Entities
{
    public class PermissionRequestNumOfDays : BaseEntity
    {
        public int AntenatalLeaveDay { get; set; } = 56; //Doğum Öncesi
        public int PostnatalLeaveDay { get; set; } = 56; //Doğum Sonrası
        public int PaternityLeaveDay { get; set; } = 5; // Babalık çocuk doğduktan sonra
        public int BereavementLeaveDay { get; set; } = 3; //Vefat
        public int MarriageLeaveDay { get; set; } = 3; // Evlendiği 1 yıl içerisinde kullanabilir.       

    }
}
