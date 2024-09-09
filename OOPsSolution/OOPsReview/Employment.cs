//this refers to a namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Employment
    {
        //data members (fields, variables)
        //typically data members are private and hold data for use
        //  within your class
        //usually associated with a property
        //a data member does not have an built-in validation
        private string _Title;
        private double _Years;
        private SupervisoryLevel _Level;

        //properties
        //are associated with a single piece of data.
        //Properties can be implemented by:
        //  a) fully implemented property
        //  c) auto implmented property

        //A property does not need to store data
        //  this type of property is referred to as a read-only
        //  this property typically uses existing data values
        //      within the instance to return a computed value

        //fully implemented properties usually has additional logic
        //  to execute for control over the data: such as validation
        //fully implemented properties will have a declared data
        //  member to store the data into
        
        //auto implemented properties do not have additional logic
        //Auto implemented properties do not have a declared
        //  data member instead the o/s will create on the property's
        //  behave a storage that is accessable ONLY by the property

        ///<summary>
        ///Property: Title
        ///validation: there must be a character in the string
        ///a property will always have a getter (accessor)
        ///a property may or maynot have a setter (mutator)
        /// no mutator the property is consider "read-only" and is
        ///         usually returning a compound field
        /// has a mutator, the property will a some point save the data
        ///     to storage
        /// the mutator may be public (default) or private
        ///     public: accessable by outside users of the class
        ///     private: accessable ONLY within the class, usually
        ///                 via the constructor or a method
        /// !!!!! a property DOES NOT have ANY declared incoming parameters !!!!!!
        /// </summary>
        
        public string Title
        {
            //accessor (getter)
            //returns the string associated with this property
            get { return _Title; }

            //mutator (setter)
            //it is within the set that the validation of the data
            //  is done to determine if the data is acceptable
            //if all processing of the string is done via the property
            //  it will ensure that good data is within the associated string
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Title", "Title is a required field");
                _Title = value;
            }
        }

        ///<summary>
        ///Property: Years
        ///validation: the value must be 0 to greater
        ///</summary>
        public double Years
        {
            get
            {
                return _Years;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Years", value,
                            "Years must be 0 to greater (ie 3.75)");
                _Years = value;
            }
        }

        ///<summary>
        ///Property: StarDdate
        ///validation: none
        ///access: private
        ///</summary>
        //since the access to this property for the mutator is private ANY validation
        //  for this data will need to be done elsewhere
        //possible locations for the validation could be in
        //  a) a constructor
        //  b) any method that will alter the data
        //a private mutator will NOT allow alteration of the data via a property for the
        //  outside user, however, methods within the class will still be able to
        //  use the property

        //this property can be coded as an auto-implemented property
        public DateTime StartDate { get; private set; }

        ///<summary>
        ///Property: Level
        ///validation: none
        ///note: this is an enum using SupervisoryLevel
        ///</summary>
        
        //can an auto-implemented be coded as a fully implemented
        public SupervisoryLevel Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        //constructors

        //methods (aka behaviours)
    }
}
