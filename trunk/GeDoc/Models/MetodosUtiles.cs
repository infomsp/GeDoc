namespace GeDoc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using Microsoft.Web.Mvc;

    public class MetodosUtiles
    {
        public MetodosUtiles()
        {
        }

        public int Edad(DateTime? pFecha)
        {
            if(pFecha == null)
            {
                return 0;
            }
            
            if (DateTime.Now.Month <= pFecha.Value.Month)
            {
                if (DateTime.Now.Month == pFecha.Value.Month)
                {
                    if (DateTime.Now.Day < pFecha.Value.Day)
                    {
                        return DateTime.Now.Year - pFecha.Value.Year - 1;
                    }
                }
                else
                {
                    return DateTime.Now.Year - pFecha.Value.Year - 1;
                }
            }

            return DateTime.Now.Year - pFecha.Value.Year;
        }
    }
}