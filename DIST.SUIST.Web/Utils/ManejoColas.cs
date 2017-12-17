using DIST.SUIST.BE;
using IronSharp.Core;
using IronSharp.IronMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIST.SUIST.Web
{
    public class ManejoColas
    {
        public static void AgregarCola(string Cola, string Mensaje)
        {
            IronMqRestClient iromMq = IronSharp.IronMQ.Client.New(new IronClientConfig
            {
                ProjectId = ParametrosConfiguracionBE.ColasProjectId,
                Token = ParametrosConfiguracionBE.ColasToken,
                Host = ParametrosConfiguracionBE.ColasHost,
                Scheme = "http",
                Port = 80
            });

            var queue = iromMq.Queue(Cola);
            queue.Post(Mensaje);
        }

        public static int ObtenerCola(string Cola)
        {
            IronMqRestClient iromMq = IronSharp.IronMQ.Client.New(new IronClientConfig
            {
                ProjectId = ParametrosConfiguracionBE.ColasProjectId,
                Token = ParametrosConfiguracionBE.ColasToken,
                Host = ParametrosConfiguracionBE.ColasHost,
                Scheme = "http",
                Port = 80
            });

            var queue = iromMq.Queue(Cola);

            QueueInfo info = queue.Info();
            
            //MessageCollection mensajes = queue.Reserve(info.Size);
            
            return (int)info.Size;
        }

        public static void LimpiearCola(string Cola)
        {
            IronMqRestClient iromMq = IronSharp.IronMQ.Client.New(new IronClientConfig
            {
                ProjectId = ParametrosConfiguracionBE.ColasProjectId,
                Token = ParametrosConfiguracionBE.ColasToken,
                Host = ParametrosConfiguracionBE.ColasHost,
                Scheme = "http",
                Port = 80
            });

            var queue = iromMq.Queue(Cola);
            queue.Clear();
            queue.Delete();
        }
    }
}