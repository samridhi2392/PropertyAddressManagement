using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAM.Model
{
   public class AddressEntity : IDisposable

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

     

       
        [StringLength(150)]
        public string StateName { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter an Street Number.")]
        public string StreetNumber { get; set; }

        [Required(ErrorMessage = "You must enter an Street.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "You must Select Suburb")]
        public int SuburbId { get; set; }
        [Required(ErrorMessage = "You must enter an PostCode")]
        public int PostCode { get; set; }
        [Required(ErrorMessage = "You must Select State")]
        public int StateId { get; set; }
        public Nullable<int> UnitNumber { get; set; }
        public string PropertyType { get; set; }
        public string StreetType { get; set; }

        public string SuburbName { get; set; }
        public string FullAddress { get; set; }

       #endregion
    }
}
