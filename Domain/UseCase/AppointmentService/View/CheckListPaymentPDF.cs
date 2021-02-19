using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.UseCase.AppointmentService.View
{
    public class CheckListPaymentPDF
    {
        public static string Writer(Appointment appointment)
        {
            var body = "<hr>";
            body += "<h1>CheckList Veiculo</h1>";
            body += "</hr>";
            body += "<hr>";
            body += "<h4>Locador</h4>";
            body += $"Cliente: {appointment.Client.Name} ";
            body += $"CPF: {appointment.Client.Cpf} ";
            body += "</hr>";            
            body += "<hr>";
            body += $"Data da reserva: {appointment.Schedule:dd/mm/yyyy HH:MM}<br>";
            body += $"Quantidade de horas alugadas: {appointment.HourLocation}<br>";
            body += $"Data da coleta prevista: {appointment.DateTimeExpectedCollected:dd/mm/yyyy HH:MM}<br>";
            body += $"Data de entrega prevista: {appointment.DateTimeExpectedDelivery:dd/mm/yyyy HH:MM}<br>";
            body += $"Valor da hora: R${appointment.HourPrice}";
            body += "<hr>";
            body += "<h3>Reserva do veículo</h3>";
            body += "<hr>";
            body += "<hr>";
            body += $"Marca: {appointment.Car.Brand.Name}<br>";
            body += $"Modelo: {appointment.Car.Model.Name}<br>";
            body += $"Categoria: {appointment.Car.Category.Name}<br>";
            body += $"Capacidade do tanque: {appointment.Car.TankCapacity}<br>";
            body += $"Capacidade do Porta Malas: {appointment.Car.LuggageCapacity}";
            body += "<hr>";
            body += $"<h2>Valor Estimado: R${appointment.Subtotal}</h2>";
            body += "<hr>";
            body += "<hr>";
            body += "<h3>Checklist</h3>";
            body += "<hr>";
            body += $"Carro limpo: {(appointment.CheckList.CleanCar ? "Sim" : "Não")}<br>";
            body += $"Tanque cheio: {(appointment.CheckList.FullTank ? "Sim" : "Não")}<br>";
            body += $"Quantidade de litros pendentes: {appointment.CheckList.FullTank} litros<br>";
            body += $"Amassado: {(appointment.CheckList.Scratches ? "Sim" : "Não")}<br>";
            body += $"Arranhões: {(appointment.CheckList.Crumpled ? "Sim" : "Não")}";
            body += "<hr>";
            body += "<h3>Reserva do veículo</h3>";
            body += "<hr>";
            body += $"Marca: {appointment.Car.Brand.Name}<br>";
            body += $"Modelo: {appointment.Car.Model.Name}<br>";
            body += $"Categoria: {appointment.Car.Category.Name}<br>";
            body += $"Capacidade do tanque: {appointment.Car.TankCapacity}<br>";
            body += $"Capacidade do Porta Malas: {appointment.Car.LuggageCapacity}";
            body += "<hr>";
            body += $"<h3>Valor Estimado: R${appointment.Subtotal}</h3>";
            body += $"<h2>Valor Total: R${appointment.Amount}</h2>";
            body += "<hr>";


            return body;
        }
    }
}
