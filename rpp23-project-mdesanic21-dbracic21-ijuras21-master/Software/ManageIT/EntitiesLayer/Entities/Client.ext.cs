using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities {
    /// <remarks>
    /// Ivan Juras
    /// </remarks>
    public partial class Client {
        /// <remarks>
        /// Ivan Juras
        /// </remarks>
        public override string ToString() {
            if(ID_type == 1) {
                return FirstName + " " + LastName;
            } else {
                return CompanyName;
            }
        }
    }
}
