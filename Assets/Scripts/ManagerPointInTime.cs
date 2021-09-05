using System.Collections.Generic;

namespace DefaultNamespace
{
    public class ManagerPointInTime
    {
        public List<TimeBody> timeBodiesDisabled;

        public ManagerPointInTime(List<TimeBody> timeBodiesDisabled)
        {
            this.timeBodiesDisabled = timeBodiesDisabled;
        }
    }
}