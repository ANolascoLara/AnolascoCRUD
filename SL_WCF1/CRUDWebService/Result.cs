using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SL_WCF1.CRUDWebService
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public bool Correct { get; set; }//TRUE = Proceso hecho correctamente
        [DataMember]                        // FALSE = Hubo un error
        public string ErrorMessage { get; set; }//Cual es el error especifico
        
        [DataMember]
        public object Object { get; set; }
        [DataMember]
        public List<object> Objects { get; set; }

    }
}