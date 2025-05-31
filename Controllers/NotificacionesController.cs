using Microsoft.AspNetCore.Mvc;
using FarmaciaApi.Data;
using FarmaciaApi.Entities;
using FarmaciaApi.Services;
using System;
using System.Linq;

namespace FarmaciaApi.Controllers
{
    [ApiController]
    [Route("notificaciones")]
    public class NotificacionesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EmailService _email;
        private readonly RabbitService _mq;

        public NotificacionesController(AppDbContext context, EmailService email, RabbitService mq)
        {
            _context = context;
            _email = email;
            _mq = mq;
        }

        // ✔️ Envía a RabbitMQ y guarda en la BD
        [HttpPost("enviar-rabbit")]
        public IActionResult EnviarRabbit([FromBody] string mensaje)
        {
            _mq.EnviarMensaje(mensaje);

            var alerta = new HistorialAlerta
            {
                Mensaje = mensaje,
                Fecha = DateTime.Now,
                EnviadoRabbit = true,
                EnviadoCorreo = false
            };

            _context.HistorialAlertas.Add(alerta);
            _context.SaveChanges();

            return Ok("Mensaje enviado directamente a RabbitMQ y guardado en la BD.");
        }

        // ✔️ Envía por correo y RabbitMQ, y registra ambos
        [HttpPost]
        public IActionResult EnviarAlertaCompleta([FromBody] string mensaje)
        {
            _email.EnviarCorreo("admin@farmacia.com", "Alerta del sistema", mensaje);
            _mq.EnviarMensaje(mensaje);

            var alerta = new HistorialAlerta
            {
                Mensaje = mensaje,
                Fecha = DateTime.Now,
                EnviadoRabbit = true,
                EnviadoCorreo = true
            };

            _context.HistorialAlertas.Add(alerta);
            _context.SaveChanges();

            return Ok("Alerta enviada por correo y RabbitMQ, registrada.");
        }

        // ✔️ Muestra TODO el historial
        [HttpGet]
        public IActionResult GetHistorial()
        {
            var historial = _context.HistorialAlertas
                .OrderByDescending(a => a.Fecha)
                .ToList();

            return Ok(historial);
        }

        // ✔️ Muestra solo los mensajes enviados a RabbitMQ
        [HttpGet("historial-rabbit")]
        public IActionResult GetSoloRabbit()
        {
            var historialRabbit = _context.HistorialAlertas
                .Where(h => h.EnviadoRabbit)
                .OrderByDescending(h => h.Fecha)
                .ToList();

            return Ok(historialRabbit);
        }

        // ✔️ Muestra solo los mensajes enviados por correo
        [HttpGet("historial-correo")]
        public IActionResult GetSoloCorreo()
        {
            var historialCorreo = _context.HistorialAlertas
                .Where(h => h.EnviadoCorreo)
                .OrderByDescending(h => h.Fecha)
                .ToList();

            return Ok(historialCorreo);
        }
    }
}
