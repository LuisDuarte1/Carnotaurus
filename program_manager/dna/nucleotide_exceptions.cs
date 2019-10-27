using System;

namespace CarnotaurusV2{
    namespace Program_Manager{
        namespace NDNA{

            class InvalidGeneticInformationException : Exception{
                public InvalidGeneticInformationException() : base(String.Format("This nucleotide is not valid.")){

                }
                
            }
        
        }
    
    }

}