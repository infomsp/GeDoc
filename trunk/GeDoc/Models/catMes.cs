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

    public class catMes
    {
        public int mesId
        {
            get;
            set;
        }

        public string mesNombre
        {
            get;
            set;
        }
    }

    public class catMeses : INotifyPropertyChanged
    {
        private List<catMes> _Mes;
        public List<catMes> Mes
        {
            get
            {
                return _Mes;
            }
            set
            {
                _Mes = value;
            }
        }

        public catMeses()
        {
            Mes = new List<catMes>();
            Mes.Add(new catMes() { mesId = 1, mesNombre = "Enero" });
            Mes.Add(new catMes() { mesId = 2, mesNombre = "Febrero" });
            Mes.Add(new catMes() { mesId = 3, mesNombre = "Marzo" });
            Mes.Add(new catMes() { mesId = 4, mesNombre = "Abril" });
            Mes.Add(new catMes() { mesId = 5, mesNombre = "Mayo" });
            Mes.Add(new catMes() { mesId = 6, mesNombre = "Junio" });
            Mes.Add(new catMes() { mesId = 7, mesNombre = "Julio" });
            Mes.Add(new catMes() { mesId = 8, mesNombre = "Agosto" });
            Mes.Add(new catMes() { mesId = 9, mesNombre = "Septiembre" });
            Mes.Add(new catMes() { mesId = 10, mesNombre = "Octubre" });
            Mes.Add(new catMes() { mesId = 11, mesNombre = "Noviembre" });
            Mes.Add(new catMes() { mesId = 12, mesNombre = "Diciembre" });
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}