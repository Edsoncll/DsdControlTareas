using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using DIST.SUIST.BE;
using System.Text;

namespace DIST.SUIST.Services.Test
{
    [TestClass]
    public class ActividadTest
    {
        [TestMethod]
        public void TestListarEventos()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:57566/ServicesWcf/wsActividad.svc/Evento");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string tramaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            EventoBE respuesta = js.Deserialize<EventoBE>(tramaJson);

            Console.Write(respuesta.description);

            Assert.AreEqual(1, respuesta.id);
        }

        [TestMethod]
        public void TestGuardarActividad()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            string json = js.Serialize(new ActividadBE()
            {
                IdUsuario = 1,
                IdCliente = 19,
                IdProyecto = 0,
                IdTipoActividad = 2,
                IdContacto = 0,
                Glosa = "asfasd",
                StrFechaInicio = "",
                StrFechaFin = "",
                TotalHoras = 5,
                TotalMinutos = 0,
                Facturable = true
            });
            byte[] data = Encoding.UTF8.GetBytes(json);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:57566/ServicesWcf/wsActividad.svc/Evento");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";

            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string repuesta = reader.ReadToEnd();

            //Assert.AreEqual("\"OK\"", repuesta);
        }
    }
}
