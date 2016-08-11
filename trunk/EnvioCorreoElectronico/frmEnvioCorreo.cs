using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.IO;

namespace EnvioCorreoElectronico
{
    public partial class frmEnvioCorreo : Form
    {
        public frmEnvioCorreo()
        {
            InitializeComponent();
        }

        private void frmEnvioCorreo_Load(object sender, EventArgs e)
        {
        }

        private bool EnviarNotificacionesDeResoluciones()
        {
            // Acá enviamos los e-mail
            MailAddress _Desde = new MailAddress("msp.notificaciones@gmail.com", "Dirección Administrativa");
            this.Update();
            try
            {
                efGeDocEntities _Datos = new efGeDocEntities();

                var _Notificaciones = from r in _Datos.proResolucion.ToList()
                                      where r.resEsImportante == true && r.resNotificacionVencimiento != null ? r.resNotificacionVencimiento.Value.Date >= DateTime.Now.Date : false
                                      select r;
                pgbProgreso.Value = 0;
                pgbProgreso.Maximum = _Notificaciones.Count();
                foreach (var _Email in _Notificaciones)
                {
                    DateTime _FechaInicial = _Email.resNotificacionVencimiento.Value.AddDays((double)((_Email.resNotificacionDiaInicio != null ? _Email.resNotificacionDiaInicio : 0) * -1));

                    pgbProgreso.Value++;

                    if (_Email.resNotificacionVencimiento == null)
                        continue;
                    if (_FechaInicial.Date > DateTime.Now.Date)
                        continue;

                    pbProgreso2.Value = 0;
                    pbProgreso2.Maximum = _Email.proResolucionNotificacionEmpleado.Count();
                    foreach (var _Destinatario in _Email.proResolucionNotificacionEmpleado)
                    {
                        MailMessage _Correo = new MailMessage();
                        SmtpClient _SMTP = new SmtpClient();
                        sisLogEnvioEmail _LogEmail = new sisLogEnvioEmail();
                        string _Nombre = _Destinatario.catPersona.perApellidoyNombre;
                        string _Expedientes = _Email.proResolucionExpedientes.FirstOrDefault().catZona.zonCodigo.ToString();
                        string _Html = "";

                        pbProgreso2.Value++;
                        _Expedientes += "-" + _Email.proResolucionExpedientes.FirstOrDefault().reseExpedienteNumero.ToString();
                        _Expedientes += "/" + _Email.proResolucionExpedientes.FirstOrDefault().reseExpedienteAnio.ToString();

                        _LogEmail.leFecha = DateTime.Now;

                        _Correo.From = _Desde;
                        _Correo.To.Add(new MailAddress(_Destinatario.catPersona.perEmail, _Nombre));
                        _Correo.Subject = "Recordatorio...";
                        _Correo.IsBodyHtml = true;

                        _Html = "<html>";
                        _Html += "<head>";
                        _Html += "<title></title>";
                        _Html += "</head>";
                        _Html += "<body>";
                        _Html += "<table frame=\"border\" style=\"border: 1px solid #336699\">";
                        _Html += "<tr>";
                        _Html += "<td style=\"height: 120px; width: 80px;\"><img src='cid:imagen2' width=\"100%\" /></td>";
                        _Html += "<td></td>";
                        _Html += "<td></td>";
                        _Html += "<td style=\"height: 120px; width: 380px; text-align:center; font-family: Calibri; font-size: x-large; color: #336699; font-weight: bold;\">";
                        _Html += "<p>Ministerio de Salud Pública</p>";
                        _Html += "<p style=\"font-size: large\">Dirección Administrativa</p>";
                        _Html += "</td>";
                        _Html += "<td></td>";
                        _Html += "<td style=\"width: 50px;\"></td>";
                        _Html += "<td style=\"height: 120px; width: 113px;\"><img src='cid:imagen3' width=\"100%\" /></td>";
                        _Html += "</tr>";
                        _Html += "<tr><td></td></tr>";
                        _Html += "<tr>";
                        _Html += "<td></td>";
                        _Html += "<td></td>";
                        _Html += "<td style=\"font-family: Arial, Helvetica, sans-serif; font-size: xx-small; text-align:left; vertical-align:middle\">";
                        _Html += "<img src='cid:imagen' align=\"middle\" width=\"50px\" />";
                        _Html += "</td>";
                        _Html += "<td style=\"font-family: Arial, Helvetica, sans-serif; font-size: xx-small; text-align:left; vertical-align:middle; white-space: nowrap; word-spacing: normal; line-height: 1px;\">";
                        _Html += "<p>Debe iniciar el expediente número " + _Expedientes + "</p>";
                        _Html += "<p>De acuerdo a lo establecido a la resolución número " + _Email.resNumero.ToString() + "/" + _Email.resFecha.Value.Year.ToString() + "(ver archivo adjunto)</p>";
                        _Html += "</td>";
                        _Html += "<td></td>";
                        _Html += "</tr>";
                        _Html += "</table>";
                        _Html += "</body>";
                        _Html += "</html>";

                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_Html, Encoding.UTF8, MediaTypeNames.Text.Html);

                        string _Path = Path.Combine(Application.StartupPath.ToString().Replace("\\bin\\Debug", "") + "\\Resources\\General\\Notificacion", "Attention.jpg");
                        LinkedResource img = new LinkedResource(_Path, MediaTypeNames.Image.Jpeg);
                        img.ContentId = "imagen";
                        htmlView.LinkedResources.Add(img);

                        _Path = Path.Combine(Application.StartupPath.ToString().Replace("\\bin\\Debug", "") + "\\Resources\\General", "Escudo.jpg");
                        img = new LinkedResource(_Path, MediaTypeNames.Image.Jpeg);
                        img.ContentId = "imagen2";
                        htmlView.LinkedResources.Add(img);

                        _Path = Path.Combine(Application.StartupPath.ToString().Replace("\\bin\\Debug", "") + "\\Resources\\General", "Logo_Gobierno.jpg");
                        img = new LinkedResource(_Path, MediaTypeNames.Image.Jpeg);
                        img.ContentId = "imagen3";
                        htmlView.LinkedResources.Add(img);

                        _Correo.AlternateViews.Add(htmlView);

                        _Path = Path.Combine("E:\\inetpub\\wwwroot\\GeDoc\\Content\\Archivos", _Email.resLinkArchivo);
                        Attachment _PDF = new Attachment(_Path, MediaTypeNames.Application.Pdf);
                        _Correo.Attachments.Add(_PDF);

                        _Correo.Priority = MailPriority.High;
                        _SMTP.Host = "smtp.gmail.com";
                        _SMTP.Port = 587;
                        _SMTP.Credentials = new NetworkCredential("msp.notificaciones@gmail.com", "wr5VXpI13Fm0EFGyWYO0");
                        _SMTP.EnableSsl = true;

                        //Guardamos el Log del Email.
                        _LogEmail.leDestinatario = _Nombre;
                        _LogEmail.leDestinatarioEmail = _Destinatario.catPersona.perEmail;

                        try
                        {
                            _SMTP.Send(_Correo);
                            _Correo.To.Clear();
                            _LogEmail.leEnviado = true;
                            //_LogEmailDetalle.ledMensajeError = "Envío correcto.";
                        }
                        catch (Exception ex)
                        {
                            _LogEmail.leEnviado = false;
                            _LogEmail.leMensajeError = ex.Message.ToString();
                        }
                        _Datos.sisLogEnvioEmail.AddObject(_LogEmail);
                    }
                }
                //Grabamos el Log
                _Datos.SaveChanges();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return false;
            }

            return true;
        }

        private bool EnviarNotificacionesDeRequerimientos()
        {
            // Acá enviamos los e-mail
            MailAddress _Desde = new MailAddress("msp.notificaciones@gmail.com", "Dirección Administrativa");
            this.Update();
            try
            {
                efGeDocEntities _Datos = new efGeDocEntities();

                var _Notificaciones = from r in _Datos.catRequerimientoNotificaciones.Where(w => !w.reqnEnviado).ToList()
                                      select r;
                pgbProgreso.Value = 0;
                pgbProgreso.Maximum = _Notificaciones.Count();
                foreach (var _Email in _Notificaciones)
                {
                    pgbProgreso.Value++;

                    MailMessage _Correo = new MailMessage();
                    SmtpClient _SMTP = new SmtpClient();
                    string _Html = "";

                    _Correo.From = _Desde;
                    _Correo.To.Add(new MailAddress(_Email.reqnDestinatarioEmail, _Email.reqnDestinatario));
                    _Correo.Subject = "Notificaciones...";
                    _Correo.IsBodyHtml = true;

                    _Html = "<html>";
                    _Html += "<head>";
                    _Html += "<title>MSP - Módulo de Requerimientos</title>";
                    _Html += "</head>";
                    _Html += "<body>";
                    _Html += "<table frame=\"border\" style=\"border: 1px solid #336699\">";
                    _Html += "<tr>";
                    _Html += "<td style=\"height: 120px; width: 80px;\"><img src='cid:imagen2' width=\"100%\" /></td>";
                    _Html += "<td></td>";
                    _Html += "<td></td>";
                    _Html += "<td style=\"height: 120px; width: 380px; text-align:center; font-family: Calibri; font-size: x-large; color: #336699; font-weight: bold;\">";
                    _Html += "<p>Ministerio de Salud Pública</p>";
                    _Html += "<p style=\"font-size: large\">Dirección Administrativa</p>";
                    _Html += "</td>";
                    _Html += "<td></td>";
                    _Html += "<td style=\"width: 50px;\"></td>";
                    _Html += "<td style=\"height: 120px; width: 113px;\"><img src='cid:imagen3' width=\"100%\" /></td>";
                    _Html += "</tr>";
                    _Html += "<tr><td></td></tr>";
                    _Html += "<tr>";
                    _Html += "<td></td>";
                    _Html += "<td></td>";
                    _Html += "<td style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px; text-align:left; vertical-align:middle\">";
                    _Html += "<img src='cid:imagen' align=\"middle\" width=\"50px\" />";
                    _Html += "</td>";
                    _Html += "<td style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold; text-align:left; vertical-align:middle; white-space: nowrap; word-spacing: normal; line-height: 1px;\">";
                    _Html += "<p>" + _Email.reqnMensaje + "</p>";
                    _Html += "</td>";
                    _Html += "<td></td>";
                    _Html += "</tr>";
                    _Html += "</table>";
                    _Html += "</body>";
                    _Html += "</html>";

                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_Html, Encoding.UTF8, MediaTypeNames.Text.Html);

                    string _Path = Path.Combine(Application.StartupPath.ToString().Replace("\\bin\\Debug", "") + "\\Resources\\General\\Notificacion", "Attention.jpg");
                    LinkedResource img = new LinkedResource(_Path, MediaTypeNames.Image.Jpeg);
                    img.ContentId = "imagen";
                    htmlView.LinkedResources.Add(img);

                    _Path = Path.Combine(Application.StartupPath.ToString().Replace("\\bin\\Debug", "") + "\\Resources\\General", "Escudo.jpg");
                    img = new LinkedResource(_Path, MediaTypeNames.Image.Jpeg);
                    img.ContentId = "imagen2";
                    htmlView.LinkedResources.Add(img);

                    _Path = Path.Combine(Application.StartupPath.ToString().Replace("\\bin\\Debug", "") + "\\Resources\\General", "Logo_Gobierno.jpg");
                    img = new LinkedResource(_Path, MediaTypeNames.Image.Jpeg);
                    img.ContentId = "imagen3";
                    htmlView.LinkedResources.Add(img);

                    _Correo.AlternateViews.Add(htmlView);

                    if (_Email.reqnArchivoAdjunto != null && _Email.reqnArchivoAdjunto != "")
                    {
                        _Path = Path.Combine("E:\\inetpub\\wwwroot\\GeDoc\\Content\\Archivos\\Requerimientos", _Email.reqnArchivoAdjunto);
                        Attachment _PDF = new Attachment(_Path, MediaTypeNames.Application.Octet);
                        _Correo.Attachments.Add(_PDF);
                    }

                    _Correo.Priority = MailPriority.High;
                    _SMTP.Host = "smtp.gmail.com";
                    _SMTP.Port = 587;
                    _SMTP.Credentials = new NetworkCredential("msp.notificaciones@gmail.com", "wr5VXpI13Fm0EFGyWYO0");
                    _SMTP.EnableSsl = true;

                    try
                    {
                        _SMTP.Send(_Correo);
                        _Correo.To.Clear();
                        _Email.reqnEnviado = true;
                    }
                    catch (SmtpException ex)
                    {
                        _Email.reqnEnviado = false;
                        _Email.reqnError = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    }
                }
                //Grabamos el Log
                _Datos.SaveChanges();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
                return false;
            }

            return true;
        }

        private void frmEnvioCorreo_Shown(object sender, EventArgs e)
        {
            //EnviarNotificacionesDeResoluciones();

            EnviarNotificacionesDeRequerimientos();

            this.Close();
        }
    }
}
