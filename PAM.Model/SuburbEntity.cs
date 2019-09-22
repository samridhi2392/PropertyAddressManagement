using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PAM.Model
{
   public class SuburbEntity:IDisposable
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

        public int SuburbId { get; set; }

        [Required(ErrorMessage = "You must enter an Suburb Name.")]
        [StringLength(150)]
        public string SuburbName { get; set; }

        public int StateId { get; set; }
        public string  StateName { get; set; }
     
        #endregion
    }
}
