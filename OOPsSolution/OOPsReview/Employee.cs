using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Employee
    {
        //data members (fields, variables)
        //typically data members are private and hold data for use
        //  within your class
        //usually associated with a property
        //a data member does not have an built-in validation

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
        //constructors

        //methods (aka behaviours)
    }
}
