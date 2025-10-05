using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class MemberSession : BaseEntity
    {

        public bool IsAttended { get; set; }

        #region Navigation Properties

        #region Member - MemberSesssion

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        #endregion

        #region Session - MemberSession

        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;

        #endregion


        #endregion

    }
}
