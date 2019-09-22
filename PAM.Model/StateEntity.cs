using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAM.Model
{
    public class StateEntity:IDisposable
    {

        #region Class Public Methods

        /// <summary>
        /// Purpose: Implements the IDispose interface.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Class Property Declarations

        public int StateId { get; set; }

        [Required(ErrorMessage = "You must enter an State Name.")]
        [StringLength(150)]
        public string StateName { get; set; }

       
        #endregion
    }
}
