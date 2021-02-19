using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.AppointmentService.View
{
    public class LeaseSimulateAgreementToPDF
    {
        public static string Writer(Appointment appointment)
        {

            var body = "<hr>";
            body += "<h1>SIMULÇÃO CONTRATO DE LOCAÇÃO DE AUTOMÓVEL POR PRAZO DETERMINADO</h1>";
            body += "<h3>IDENTIFICAÇÃO DAS PARTES CONTRATANTES</h3>";
            body += "</hr>";
            body += "<hr>";
            body += "<h4>Locador</h4>";
            body += $"Cliente: {appointment.Client.Name} ";
            body += $"CPF: {appointment.Client.Cpf} ";
            body += $" Residente e domiciliado em endereço {appointment.Client.Logradouro}, ";
            body += $"número {appointment.Client.Number}, CEP: {appointment.Client.CEP} - Cidade: {appointment.Client.City}, {appointment.Client.State}";
            body += "</hr>";
            body += "<hr>";
            body += "<h4>Locatário</h4>";
            body += $"Cliente: Localiza ";
            body += $"CNPJ: XX.XXX.XXX/XXXX-XX ";
            body += $" Residente e domiciliado em endereço Rua Castelo da Beira, ";
            body += $"número 224, CEP: 30840-460 - Cidade: Belo Horizonte, MG";
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

            return body;

        }
    }
}
