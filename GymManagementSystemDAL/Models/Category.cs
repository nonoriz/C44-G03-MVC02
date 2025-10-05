using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class Category : BaseEntity
    {
        #region Properties
        public string CategoryName { get; set; } = null!;
        #endregion

        #region Navigation Property

        public ICollection<Session> CategorySessions { get; set; } = null!;

        #endregion
    }
}
